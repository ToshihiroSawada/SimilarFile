using System.Diagnostics;
using System.Security.Cryptography;
using System.Data.SQLite;
using System;
using System.Threading.Tasks;

namespace SimilarFiles
{
    public partial class search_list : Form
    {
        public bool PSKILL = false;

        public search_list()
        {
            InitializeComponent();
        }

        private string select_folder()
        {
            using (var fbd = new FolderBrowserDialog()
            {
                Description = "フォルダを選択してください",
                SelectedPath = @""
            })
            {
                if (fbd.ShowDialog() != DialogResult.OK)
                {
                    return null;
                }

                return fbd.SelectedPath;
            }
        }

        private void add_button_Click()
        {
            var sp = select_folder();
            if (sp != null)
            {
                folder_list_table.Rows.Add(sp);
            }
        }
        private void add_button_Click(object sender, EventArgs e)
        {
            add_button_Click();
        }

        private void remove_button_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in folder_list_table.SelectedRows)
            {
                if (!r.IsNewRow)
                {
                    folder_list_table.Rows.Remove(r);
                }
            }
        }

        private void reset_button_Click(object sender, EventArgs e)
        {
            folder_list_table.Rows.Clear();
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            start_button_Click(sender, e, match_list);
        }

        private async void start_button_Click(object sender, EventArgs e, DataGridView match_list)
        {
            match_list.Rows.Clear();
            string db_path = "./data.db";
            if (File.Exists(db_path))
            {
                File.Delete(db_path);
            }

            //stop_buttonクリックできないようにする
            start_button.Enabled = false;
            add_button.Enabled = false;
            remove_button.Enabled = false;
            reset_button.Enabled = false;

            Hashing();

            start_button.Enabled = true;
            add_button.Enabled = true;
            remove_button.Enabled = true;
            reset_button.Enabled = true;
        }

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            PSKILL = true;
        }

        async private void Hashing()
        {
            //folder_list_tableのデータを取り出す
            int line = folder_list_table.Rows.Count - 1;
            string[] folder_list = new string[line];
            for (int i = 0; i < line; i++)
            {
                folder_list[i] = folder_list_table.Rows[i].Cells[0].Value.ToString();
            }

            //指定されたフォルダー以下のフォルダーとファイルの一覧を取り出す
            List<string> files = new List<string>();
            //Stack<string> folders = new Stack<string>();
            List<string> folders = new List<string>();
            foreach (string folder in folder_list)
            {
                try
                {
                    //folders.Add(folder);
                    folders.AddRange(Directory.GetDirectories(
                        folder, "*", SearchOption.TopDirectoryOnly
                    ));
                }
                catch (Exception err)
                {
                    Debug.Print(err.ToString());
                    continue;
                }

            }
            Debug.Print(string.Join("\n  ", folders));
            string[] fileList;
            foreach (string folder in folder_list)
            {
                fileList = Directory.GetFiles(folder);
                foreach (string st in fileList)
                {
                    files.Add(st);
                }

            }
            foreach (string folder in folders)
            {
                try
                {
                    //パス内に$が含まれている場合、システム領域のためスキップ
                    if (folder.IndexOf("$") > 0)
                    {
                        continue;
                    }
                    fileList = Directory.GetFiles(
                        folder, "*", SearchOption.AllDirectories
                    );
                }
                catch
                {
                    continue;
                }
                foreach (string st in fileList)
                {
                    files.Add(st);
                }
            }
            progressBar1.Value = 0;
            progressBar1.Maximum = files.Count;
            progressBar1.Visible = true;
            //ファイルハッシュ化し、listに追加
            var j = 0;
            List<List<string>> data_list = new List<List<string>>();
            foreach (var file in files)
            {
                try
                {
                    var data = await Task<List<string>>.Factory.StartNew(() =>
                    {
                        List<string> data = new List<string>();

                        try
                        {
                            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            using MD5 ha = MD5.Create();
                            var hash = ha.ComputeHash(fs);
                            string hash_text = "";
                            string file_name = Path.GetFileName(file);

                            hash_text = Convert.ToBase64String(hash);
                            data.Add(file);
                            data.Add(file_name);
                            data.Add(hash_text);
                            ha.Clear();
                            return data;
                        }
                        catch (Exception err)
                        {
                            Debug.Print(err.ToString());
                            return data;
                        }
                    });
                    data_list.Add(data);

                    //ProgressBarの値を増加させる
                    progressBar1.Value += 1;
                    j++;
                    if (PSKILL == true)
                    {
                        break;
                    }
                }
                catch (Exception err)
                {
                    Debug.Print(err.ToString());
                    continue;
                }
            }


            //ファイルをDBへ追加
            var sqlCSB = new SQLiteConnectionStringBuilder { DataSource = "data.db" };
            using (var cn = new SQLiteConnection(sqlCSB.ToString()))
            {
                cn.Open();

                using (var cmd = new SQLiteCommand(cn))
                {
                    cmd.CommandText = @"
                        CREATE TABLE IF NOT EXISTS file_hash(
                            path TEXT PRIMARY KEY,
                            name TEXT NOT NULL,
                            hash TEXT NOT NULL
                        )
                    ";
                    cmd.ExecuteNonQuery();

                    foreach (var list in data_list)
                    {
                        try
                        {
                            cmd.CommandText = $@"
                                INSERT INTO file_hash
                                VALUES ('{list[0]}', '{list[1]}', '{list[2]}')
                        ";

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception err)
                        {
                            Debug.Print(err.ToString());
                            continue;
                        }
                    }
                    cmd.Dispose();
                }

                cn.Close();
            }

            ReadDBData(sqlCSB);
            progressBar1.Visible = false;
            PSKILL = false;
        }

        private void ReadDBData(SQLiteConnectionStringBuilder sqlCSB)
        {
            //DBからハッシュが一致したファイルを取り出す
            using (var cn = new SQLiteConnection(sqlCSB.ToString()))
            {
                cn.Open();

                using (var cmd = new SQLiteCommand(cn))
                {
                    cmd.CommandText = $@"
                        SELECT A.path, A.name, B.path, B.name 
                        FROM file_hash AS A 
                        LEFT OUTER JOIN file_hash AS B 
                        ON A.hash = B.hash 
                        AND A.path != B.path 
                        WHERE B.name IS NOT NULL;
                    ";
                    SQLiteDataReader dr = cmd.ExecuteReader();

                    List<string[]> list = new List<string[]>();
                    try
                    {
                        for (int i = 0; dr.Read(); i++)
                        {
                            string[] column = new string[dr.FieldCount];
                            for (int j = 0; j < dr.FieldCount; j++)
                            {
                                column[j] = dr[j].ToString();
                            }
                            list.Add(column);
                        }

                        foreach (string[] mlist in list)
                        {
                            match_list.Rows.Add(mlist);
                        }
                    }
                    catch (Exception err)
                    {
                        Debug.Print(err.ToString());
                    }
                    cmd.Dispose();
                }
                cn.Close();
            }
        }

        private void match_list_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewCell onePreviousCell;
            int even_col = (e.ColumnIndex + 1) % 2;
            if (even_col % 2 == 0)
            {
                try
                {
                    onePreviousCell = match_list.Rows[e.RowIndex].Cells[e.ColumnIndex - 1];
                }
                catch (Exception err)
                {
                    Debug.Print(err.ToString());
                    return;
                }
            }
            else
            {
                onePreviousCell = match_list.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
            try
            {
                string path = onePreviousCell.Value.ToString();
                Process.Start("EXPLORER.EXE", @$"/select,""{path}");
            }
            catch (Exception err)
            {
                Debug.Print(err.ToString());
            }
        }

        private void DBファイルToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string path;
            using (var ofd = new OpenFileDialog()
            {
                Title = "フォルダを選択してください",
                InitialDirectory = @""
            })
            {
                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            var sqlCSB = new SQLiteConnectionStringBuilder { DataSource = "data.db" };
            ReadDBData(sqlCSB);
        }

        private void フォルダーToolStripMenuItem_Click(object sender, EventArgs e)
        {
            add_button_Click();
        }

        private void match_list_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                match_list.CurrentCell = match_list[e.ColumnIndex, e.RowIndex];
                Point p = Cursor.Position;
                CellRightClickMenu.Show(p);
            }
        }
    }
}
