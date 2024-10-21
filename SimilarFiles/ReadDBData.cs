using NLog;
using System.Data.SQLite;

namespace SimilarFiles
{
    internal class ReadDBData
    {
        bool _PSKILL = Globals.PSKILL;
        readonly Logger _logger = Globals.LOGGER;
        private ReadDBData(SQLiteConnectionStringBuilder sqlCSB)
        {
            //progressBar1.Visible = true;
            //progressBar1.Style = ProgressBarStyle.Marquee;
            //label1.Text = "データ一致ファイル一覧作成中...";
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

                List<string[]> list = new();
                try
                {
                    while (dr.Read())
                    {
                        string[] column = new string[dr.FieldCount];
                        for (int j = 0; j < dr.FieldCount; j++)
                        {
                            column[j] = dr[j].ToString();
                        }
                        match_list.Rows.Add(column);
                    }
                }
                catch (Exception err)
                {
                    _logger.Error(err);
                }
                cmd.Dispose();
            }
            cn.Close();
            _progressBar.Invoke(new Action(() => { _progressBar.Visible = false; }));
        }

    }
}
