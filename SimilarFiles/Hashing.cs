using NLog;

namespace SimilarFiles
{
    internal class Hashing
    {
        Logger logger = Globals.LOGGER;
        private Form _form;
        private Label _label;
        private ProgressBar _progressBar;
        private DataGridView _dataGridView;

        public Hashing(Form form, Label label, DataGridView dataGridView)
        {
            _form = form;
            _label = label;
            _dataGridView = dataGridView;
        }
                async private void hashing()
        {
            _label.Invoke(new Action (() => _label.Text = "フォルダ一覧取得中..."));
            //folder_list_tableのデータを取り出す
            int line = _dataGridView.Rows.Count;
            var folder_list = new List<string>();
            for (int i = 0; i < line; i++)
            {
                folder_list.Add(_dataGridView.Rows[i].Cells[0].Value.ToString());
            }

            //指定されたフォルダー以下のフォルダーとファイルの一覧を取り出す
            var files = new List<string>();
            var folders = new List<string>();
            var getHashFiles = new List<string>();
            try
            {
                var flist = await Task.Run(() => GetAllDir(folder_list));
                folders.AddRange(flist);
            }
            catch (Exception err)
            {
                logger.Error(err);
            }

            _label.Invoke(new Action(() => _label.Text = "ファイル調査中..."));
            //ファイルをDBへ追加
            //var sqlCSB = new SQLiteConnectionStringBuilder { DataSource = "./data.db" };
            //using var cn = new SQLiteConnection(sqlCSB.ToString());
            //cn.Open();
            //using (var cmd = new SQLiteCommand(cn))
            //{
            //cmd.CommandText = @"
            //        CREATE TABLE IF NOT EXISTS file_hash(
            //            path TEXT PRIMARY KEY,
            //            name TEXT NOT NULL,
            //            hash TEXT NOT NULL
            //        )
            //    ";
            //cmd.ExecuteNonQuery();

            string[] fileList;
            foreach (string folder in folder_list)
            {
                try
                {
                    fileList = Directory.GetFiles(folder);
                    files.AddRange(fileList);
                }
                catch (Exception err)
                {
                    logger.Error(err);
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
                    logger.Debug(folder);
                    logger.Debug(err);
                    continue;
                }
                foreach (string st in getHashFileList)
                {
                    getHashFiles.Add(st);
                }
            }
            _progressBar.Invoke(new Action(() =>
            {
                _progressBar.Value = 0;
                _progressBar.Maximum = getHashFiles.Count;
                _progressBar.Visible = true;
                _progressBar.Style = ProgressBarStyle.Blocks;
            }));
            //ファイルハッシュ化し、listに追加
            var j = 0;
            var data = new List<string[]>();
            foreach (var file in getHashFiles)
            {
                _progressBar.Invoke(() => _progressBar.Value += 1);
                data.Add(await Task.Run(() =>
                {
                    var data = new string[3];
                    if (PSKILL == true)
                    {
                        return data;
                    }

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
                        FileStream fs = new(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        //using MD5 ha = MD5.Create();
                        byte[] ha = XxHash128.Hash(BitConverter.GetBytes(fs.ReadByte()));
                        var hash = ha;
                        string hash_text = "";


                        hash_text = Convert.ToBase64String(hash);
                        data[0] = file;
                        data[1] = file_name;
                        data[2] = hash_text;
                        //ha.Clear();
                        fs.Close();
                        return data;
                    }
                    catch (Exception err)
                    {
                        logger.Error(err);
                        return data;
                        //TODO: return dataはするが、データベース格納時にNull？が存在した場合はスキップする処理にして格納しないようにする
                        //ディスクへの書き込みが多く、メモリ使用料が上がらないので、dbをメモリに保持して最後にディスクに書き込むように修正する
                        //停止ボタンをクリックした際にもう少し即時にすべての動作を停止できないか検討する
                    }
                }));

                //ProgressBarの値を増加させる
                j++;
                if (PSKILL == true)
                {
                    break;
                }

                //try
                //{
                //    cmd.CommandText = $@"
                //            INSERT INTO file_hash
                //            VALUES ('{data[0]}', '{data[1]}', '{data[2]}')
                //    ";

                //    cmd.ExecuteNonQuery();
                //}
                //catch (Exception err)
                //{
                //    logger.Error(err);
                //    logger.Error(string.Join(", ", data));
                //    continue;
                //}
                //    }
                //    cmd.Dispose();
                //    cn.Close();
            }

            //ReadDBData(sqlCSB);
            await Task.Run(() => Insertdb(data));
            var sqlCSB = new SQLiteConnectionStringBuilder { DataSource = "./data.db" };
            _progressBar.Invoke(new Action(() => {
                _progressBar.Visible = true;
                _progressBar.Style = ProgressBarStyle.Marquee;
            }));
            _label.Invoke(new Action(() => { _label.Text = "データ一致ファイル一覧作成中..."; }));

            await Task.Run(() =>
            {
                
                ReadDBData(sqlCSB);
                progressBar1.Invoke(new Action(() => { progressBar1.Visible = false; }));
                label1.Invoke(new Action(() => { label1.Visible = false; }));
                start_button.Invoke(new Action(() => { start_button.Enabled = true; }));
                add_button.Invoke(new Action(() => { add_button.Enabled = true; }));
                remove_button.Invoke(new Action(() => { remove_button.Enabled = true; }));
                reset_button.Invoke(new Action (() => { reset_button.Enabled = true; }));
            });
            
        }


    }
}
