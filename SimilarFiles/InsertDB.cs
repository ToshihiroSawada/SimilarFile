using NLog;
using System.Data.SQLite;

namespace SimilarFiles
{
    internal class InsertDB
    {
        //private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        readonly Logger _logger = Globals.LOGGER;
        private void Insertdb(List<string[]> list)
        {
            //ファイルをDBへ追加

            var sqlCSB = new SQLiteConnectionStringBuilder { DataSource = ":memory:" };
            using var cn = new SQLiteConnection(sqlCSB.ToString() + ";mode=memory;cache=shared");
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
                int counter = new();
                foreach (string[] l in list)
                {
                    if (l[0] is null || l[1] is null || l[2] is null) { continue; }
                    counter++;
                    insertData += $"""("{l[0]}", "{l[1]}", "{l[2]}")""";
                    if (counter >= 100000)
                    {
                        Globals.logger.Debug(insertData);
                        cmd.CommandText = $"""
                            INSERT INTO file_hash(path, name, hash)
                            VALUES {insertData}
                        """;
                        cmd.ExecuteNonQuery();
                        insertData = "";
                    }
                    else
                    {
                        insertData += ",";
                    }
                }
                if (insertData.Length > 0)
                {
                    insertData = insertData.Remove(insertData.Length - 1);
                    cmd.CommandText = @$"
                        INSERT INTO file_hash(path, name, hash)
                        VALUES 
                    {insertData}
                    ";
                    _logger.Debug(cmd.CommandText);
                    cmd.ExecuteNonQuery();
                }
                cmd.CommandText = "VACUUM INTO 'data.db'";
                cmd.ExecuteNonQuery();

            }
            var sqlCSB2 = new SQLiteConnectionStringBuilder { DataSource = "./data.db" };
            //using var cn2 = new SQLiteConnection(sqlCSB.ToString());
            ReadDBData(sqlCSB2);
        }

    }
}
