using System.Data.SQLite;

namespace SimilarFiles
{
    internal class DBOperation
    {
        public Form? Form1 { get; set; }
        public Button? Start_button { get; set; }
        public Button? Add_button { get; set; }
        public Button? Remove_button { get; set; }
        public Button? Reset_button { get; set; }
        public ProgressBar? ProgressBar1 { get; set; }
        public Label? Label1 { get; set; }
        public DataGridView? Match_list { get; set; }

        private readonly UIOperation uio = new();

        public void ReadDBData(SQLiteConnectionStringBuilder sqlCSB)
        {
            uio.ReadDB();
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

                List<string[]> list = [];
                try
                {
                    if (Match_list != null)
                    {
                        while (dr.Read())
                        {
                            string[] column = new string[dr.FieldCount];
                            for (int j = 0; j < dr.FieldCount; j++)
                            {
                                column[j] = dr[j].ToString();
                            }
                            Match_list.Rows.Add(column);
                        }
                    }
                }
                catch (Exception err)
                {
                    GlobalVar.logger.Error(err);
                }
                cmd.Dispose();
            }
            cn.Close();
            uio.ALLProsessComplete();
            //progressBar1.Invoke(new Action(() => progressBar1.Visible = false));
        }
    }
}
