using System.Diagnostics;
using System.Security.Cryptography;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace SimilarFiles
{
    public partial class search_list : Form
    {
        public bool PSKILL = false;

        public search_list()
        {
            InitializeComponent();
            match_list.AllowUserToAddRows = false;
            folder_list_table.AllowUserToAddRows = false;
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

        private void start_button_Click(object sender, EventArgs e, DataGridView match_list)
        {
            if (folder_list_table.RowCount < 1)
            {
                return;
            }
            match_list.Rows.Clear();
            string db_path = "./data.db";
            DialogResult dr = MessageBox.Show("前回の結果を削除しますか？", "確認", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                if (File.Exists(db_path))
                {
                    File.Delete(db_path);
                }
            }

            //stop_buttonクリックできないようにする
            start_button.Enabled = false;
            add_button.Enabled = false;
            remove_button.Enabled = false;
            reset_button.Enabled = false;
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;

            Hashing();
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
            int line = folder_list_table.Rows.Count;
            var folder_list = new List<string>();
            for (int i = 0; i < line; i++)
            {
                folder_list.Add(folder_list_table.Rows[i].Cells[0].Value.ToString());
            }

            //指定されたフォルダー以下のフォルダーとファイルの一覧を取り出す
            var files = new List<string>();
            var folders = new List<string>();
            var getHashFiles = new List<string>();
            try
            {
                var data = await Task.Run(() => GetAllDirectories(folder_list));
                folders.AddRange(data);
            }
            catch (Exception err)
            {
                Debug.Print(err.ToString());
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

                    string[] fileList;
                    foreach (string folder in folder_list)
                    {
                        try
                        {
                            fileList = Directory.GetFiles(folder);
                            files.AddRange(fileList);
                        }
                        catch
                        {
                            continue;
                        }

                    }
                    string[] getHashFileList;
                    foreach (string folder in folders)
                    {
                        try
                        {
                            getHashFileList = Directory.GetFiles(
                                folder, "*", SearchOption.TopDirectoryOnly
                            );
                        }
                        catch (Exception err)
                        {
                            Debug.Print("3 continue!! : " + folder);
                            Debug.Print(err.ToString());
                            continue;
                        }
                        foreach (string st in getHashFileList)
                        {
                            getHashFiles.Add(st);
                        }
                    }
                    progressBar1.Value = 0;
                    progressBar1.Maximum = getHashFiles.Count;
                    progressBar1.Visible = true;
                    progressBar1.Style = ProgressBarStyle.Continuous;
                    //ファイルハッシュ化し、listに追加
                    var j = 0;
                    foreach (var file in getHashFiles)
                    {
                        progressBar1.Value += 1;
                        var data = await Task.Run(() =>
                        {
                            var data = new string[3];
                            try
                            {
                                string file_name = Path.GetFileName(file);
                                if (file_name == "desktop.ini")
                                {
                                    return data;
                                }
                                var setFile = new FileInfo(file);
                                long fileSize = setFile.Length;
                                if (fileSize == 0)
                                {
                                    return data;
                                }
                                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                                using MD5 ha = MD5.Create();
                                var hash = ha.ComputeHash(fs);
                                string hash_text = "";


                                hash_text = Convert.ToBase64String(hash);
                                data[0] = file;
                                data[1] = file_name;
                                data[2] = hash_text;
                                ha.Clear();
                                fs.Close();
                                return data;
                            }
                            catch (Exception err)
                            {
                                Debug.Print(err.ToString());
                                return data;
                            }
                        });

                        //ProgressBarの値を増加させる
                        j++;
                        if (PSKILL == true)
                        {
                            break;
                        }

                        try
                        {
                            cmd.CommandText = $@"
                                INSERT INTO file_hash
                                VALUES ('{data[0]}', '{data[1]}', '{data[2]}')
                        ";

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception err)
                        {
                            Debug.Print(err.ToString());
                            Debug.Print("5 continue!! : " + string.Join(", ", data));
                            continue;
                        }
                    }
                    cmd.Dispose();
                    cn.Close();
                }

                ReadDBData(sqlCSB);
                progressBar1.Visible = false;
                PSKILL = false;
                start_button.Enabled = true;
                add_button.Enabled = true;
                remove_button.Enabled = true;
                reset_button.Enabled = true;
            }
        }

        private void ReadDBData(SQLiteConnectionStringBuilder sqlCSB)
        {
            //DBからハッシュが一致したファイルを取り出す
            using var cn = new SQLiteConnection(sqlCSB.ToString());
            cn.Open();

            using (var cmd = new SQLiteCommand(cn))
            {
                cmd.CommandText = @"
                        SELECT A.path, B.path 
                        FROM file_hash AS A 
                        LEFT OUTER JOIN file_hash AS B 
                        ON A.hash = B.hash 
                        AND A.path > B.path
                        WHERE B.name IS NOT NULL
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

        private void match_list_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewCell onePreviousCell = null;
            try
            {
                onePreviousCell = match_list.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
            catch (Exception err)
            {
                Debug.Print(err.ToString());
                return;
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
                Title = "DBファイルを選択してください",
                Filter = "DBファイル(*.db)|*.db",
                InitialDirectory = @"",
            })
            {
                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                path = ofd.FileName;
            }

            var sqlCSB = new SQLiteConnectionStringBuilder { DataSource = path };
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

        private async Task<List<string>> GetAllDirectories(List<string> argList)
        {
            //ファイルハッシュを取得しないスキップリスト
            var skipList = new List<string> { @"C:\Users\Public", @"C:\Users\Default", @"C:\Users\All Users", @"C:\Windows", @"C:\Power_On_and_WOL", @"C:\Intel", @"C:\Driver", @"C:\Program Files", @"C:\Program Files (x86)", @"C:\ProgramData", @"C:\Recovery", "System Volume Information" };
            var pattern = @".*AppData.*|.*\$.*";
            var list = new List<string>();
            var regex = new Regex(pattern);
            var directories = new List<string>();
            try
            {
                foreach (var path in argList)
                {
                    var matchFlug = false;

                    if (regex.IsMatch(path))
                    {
                        continue;
                    }

                    foreach (var i in skipList)
                    {
                        if (path.Contains(i))
                        {
                            matchFlug = true;
                        }
                    }
                    if (matchFlug)
                    {
                        continue;
                    }

                    list.Add(path);
                }

                foreach (string path in list)
                {
                    try
                    {
                        directories.AddRange(Directory.GetDirectories(
                            path, "*", SearchOption.TopDirectoryOnly
                        ));
                    }
                    catch
                    {
                        Debug.Print("100 conrinue!! : " + path);
                        continue;
                    }
                }
            }
            catch (Exception err)
            {
                Debug.Print(err.ToString());
            }
            if (directories.Count > 0)
            {
                var data = await Task.Run(() => GetAllDirectories(directories));
                list.AddRange(data);
            }
            return list;
        }
    }
}
