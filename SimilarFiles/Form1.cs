using System.Data.SQLite;
using System.Diagnostics;
using static SimilarFiles.GlobalVar;

namespace SimilarFiles
{
    public partial class SearchList : Form
    {
        private readonly UIOperation uio;
        private readonly Hashing hashing;
        private readonly DBOperation dbo = new();

        public SearchList()
        {
            InitializeComponent();
            match_list.AllowUserToAddRows = false;
            folder_list_table.AllowUserToAddRows = false;
            uio = new UIOperation
            {
                Form1 = this,
                Label1 = label1,
                Add_button = add_button,
                ProgressBar1 = progressBar1,
                Remove_button = remove_button,
                Reset_button = reset_button,
                Start_button = start_button,
            };
            hashing = new Hashing(folder_list_table);
        }

        private static string SelectFolder()
        {
            using var fbd = new FolderBrowserDialog()
            {
                Description = "フォルダを選択してください",
                SelectedPath = @""
            };
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return "";
            }

            return fbd.SelectedPath;
        }

        private void AddButton_Click()
        {
            var sp = SelectFolder();
            if (sp != "")
            {
                folder_list_table.Rows.Add(sp);
            }
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            AddButton_Click();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in folder_list_table.SelectedRows)
            {
                if (!r.IsNewRow)
                {
                    folder_list_table.Rows.Remove(r);
                }
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            folder_list_table.Rows.Clear();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartButton_Click(sender, e, match_list);
        }

        private void StartButton_Click(object sender, EventArgs e, DataGridView match_list)
        {
            if (sender is null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            PSKILL = false;
            if (folder_list_table.RowCount < 1)
            {
                return;
            }
            match_list.Rows.Clear();

            uio.Start();

            hashing.Hash();
        }

        private void EndToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {

            uio.PSKILL();
        }

        private void MatchList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewCell onePreviousCell;
            try
            {
                onePreviousCell = match_list.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
            catch (Exception err)
            {
                logger.Error(err);
                return;
            }
            try
            {
                string path = onePreviousCell.Value.ToString();
                Process.Start("EXPLORER.EXE", @$"/select,""{path}");
            }
            catch (Exception err)
            {
                logger.Error(err);
            }
        }

        private void DBFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                string opendb_path;
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
                    else
                    {
                        opendb_path = ofd.FileName;
                        Console.Write(opendb_path);
                    }
                }

                var sqlCSB = new SQLiteConnectionStringBuilder { DataSource = "./data.db" };
                var margeDB = new SQLiteConnectionStringBuilder { DataSource = opendb_path };
                using var cn = new SQLiteConnection(sqlCSB.ToString());
                cn.Open();
                using (var cmd = new SQLiteCommand(cn))
                {
                    cmd.CommandText = @$"
                        CREATE TABLE IF NOT EXISTS file_hash(
                            path TEXT PRIMARY KEY,
                            name TEXT NOT NULL,
                            hash TEXT NOT NULL
                        );
                        ATTACH DATABASE '{margeDB}' as db1;
                        INSERT INTO main.file_hash (path, name, hash)
                            SELECT path, name, hash 
                            FROM db1.file_hash
                        ;
                        DETTACH DATABASE db1;
                    ";
                    cmd.ExecuteNonQuery();

                }
                cn.Close();
                dbo.ReadDBData(sqlCSB);
            }
            catch (Exception err)
            {
                logger.Error(err);
            }
        }

        private void FolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddButton_Click();
        }

        private void MatchList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                match_list.CurrentCell = match_list[e.ColumnIndex, e.RowIndex];
                Point p = Cursor.Position;
                CellRightClickMenu.Show(p);
            }
        }

        private void SearchList_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("調査結果を保存しますか？", "確認", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                using var sfd = new SaveFileDialog()
                {
                    Title = "保存先を選択してください",
                    FileName = "data.db",
                    OverwritePrompt = true,
                    CheckPathExists = true,
                    Filter = "DBファイル(*.db)|*.db",
                    InitialDirectory = @"",
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.Move(@"./data.db", sfd.FileName);
                }
                else
                {
                    File.Delete(@"./data.db");
                }
                logger.Debug(sfd.FileName);
            }
            else
            {
                File.Delete(@"./data.db");
            }
        }
    }
}
