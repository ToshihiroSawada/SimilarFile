using System.Data.SQLite;
using System.IO.Hashing;
using System.Text.RegularExpressions;
using static SimilarFiles.GlobalVar;

namespace SimilarFiles
{
    internal class Hashing
    {
        private readonly DataGridView _folder_list_table;
        private readonly UIOperation uio = new();
        private readonly DBOperation dbo = new();

        public Hashing(DataGridView folder_list_table)
        {
            _folder_list_table = folder_list_table;
        }
        public async void Hash()
        {
            uio.CreateFileList();
            //folder_list_tableのデータを取り出す
            int line = _folder_list_table.Rows.Count;
            var folder_list = new List<string>();
            if (_folder_list_table is not null)
            {
                for (int i = 0; i < line; i++)
                {
                    folder_list.Add(_folder_list_table.Rows[i].Cells[0].Value.ToString());
                }
            }

            //指定されたフォルダー以下のフォルダーとファイルの一覧を取り出す
            var files = new List<string>();
            var folders = new List<string>();
            var getHashFiles = new List<string>();
            try
            {
                var flist = await Task.Run(() => GetAllDirectories(folder_list));
                folders.AddRange(flist);
            }
            catch (Exception err)
            {
                logger.Error(err);
            }

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
            uio.StartHashing(files.Count);

            //ファイルハッシュ化し、listに追加
            var j = 0;
            var data = new List<string[]>();
            foreach (var file in getHashFiles)
            {
                uio.GetHashFilesCount();
                data.Add(await Task.Run(() =>
                {
                    var data = new string[3];
                    if (GlobalVar.PSKILL == true)
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
                        byte[] hashbytes = XxHash128.Hash(BitConverter.GetBytes(fs.ReadByte()), 256);
                        fs.Close();
                        //var hash = ha;
                        string hash_text = "";


                        hash_text = Convert.ToBase64String(hashbytes);
                        Console.WriteLine(hash_text);
                        data[0] = file;
                        data[1] = file_name;
                        data[2] = hash_text;
                        //ha.Clear();
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
                if (GlobalVar.PSKILL == true)
                {
                    break;
                }
            }

            //ReadDBData(sqlCSB);
            await Task.Run(() => Insertdb(data));
            uio.ALLProsessComplete();
        }
        private void Insertdb(List<string[]> list)
        {
            //ファイルをDBへ追加
            var sqlCSB = new SQLiteConnectionStringBuilder { DataSource = "./data.db" };
            using var cn = new SQLiteConnection(sqlCSB.ToString());
            cn.Open();
            string insertData = "";
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

                foreach (string[] l in list)
                {
                    if (l[0] is null || l[1] is null || l[2] is null) { continue; }
                    insertData += @$"('{l[0]}', '{l[1]}', '{l[2]}'),";
                }

                insertData = insertData.Remove(insertData.Length - 1);
                cmd.CommandText = @$"
                    INSERT INTO file_hash(path, name, hash)
                    VALUES {insertData}
                ";
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }


            }
            cn.Close();
            dbo.ReadDBData(sqlCSB);
        }

        public async Task<List<string>> GetAllDirectories(List<string> argList)
        {
            //ファイルハッシュを取得しないスキップリスト
            var skipList = new List<string> { @"C:\Users\Public", @"C:\Users\Default", @"C:\Users\All Users", @"C:\Windows", @"C:\Power_On_and_WOL", @"C:\Intel", @"C:\Driver", @"C:\Program Files", @"C:\Program Files (x86)", @"C:\ProgramData", @"C:\Recovery", "System Volume Information" };
            //var skipList = new List<string>();
            var pattern = @".*AppData.*|.*\$.*";
            var list = new List<string>();
            var regex = new Regex(pattern);
            var directories = new List<string>();

            foreach (var path in argList)
            {
                var matchFlug = false;
                try
                {
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
                }
                catch (Exception err)
                {
                    logger.Debug(err.ToString());
                    continue;
                }
                if (matchFlug)
                {
                    continue;
                }

                list.Add(path);

                try
                {
                    directories.AddRange(Directory.GetDirectories(
                        path, "*", SearchOption.TopDirectoryOnly
                    ));
                }
                catch (Exception err)
                {
                    logger.Error(err, path);
                    continue;
                }
                if (GlobalVar.PSKILL == true)
                {
                    return list;
                }
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