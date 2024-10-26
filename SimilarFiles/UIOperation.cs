namespace SimilarFiles
{
    internal class UIOperation
    {
        /// <summary>
        /// UI操作をまとめたクラス
        /// </summary>
        public Form? Form1 { get; set; }
        public Button? Start_button { get; set; }
        public Button? Add_button { get; set; }
        public Button? Remove_button { get; set; }
        public Button? Reset_button { get; set; }
        public ProgressBar? ProgressBar1 { get; set; }
        public Label? Label1 { get; set; }

        public void Start()
        {
            /// <summary>
            /// stop_button以外クリックできないようにする
            /// </summary>
            if (Form1 != null && Start_button != null && Add_button != null && Remove_button != null && Reset_button != null && ProgressBar1 != null && Label1 != null)
            {
                Form1.Invoke(new Action(() =>
                {
                    Start_button.Enabled = false;
                    Add_button.Enabled = false;
                    Remove_button.Enabled = false;
                    Reset_button.Enabled = false;
                    ProgressBar1.Visible = true;
                    ProgressBar1.Style = ProgressBarStyle.Marquee;
                    Label1.Visible = true;
                }));
            };
        }

        public void PSKILL()
        {
            /// <summary>
            /// 全ての処理を強制的に打ち切り、ユーザーがUIを操作できるようにする
            /// </summary>
            GlobalVar.PSKILL = true;
            if (Form1 != null && Label1 != null)
            {
                Form1.Invoke(new Action(() =>
                {
                    Label1.Text = "処理停止中。しばらくお待ちください。";
                }));
            }
        }

        public void CreateFileList()
        {
            Label1?.Invoke(new Action(() => Label1.Text = "フォルダ一覧取得中..."));
        }

        public void StartHashing(int getHashFilesCount)
        {
            /// <summary>
            /// ファイルハッシュ化処理の開始時UI操作
            /// </summary>
            if (Form1 != null && ProgressBar1 != null && Label1 != null)
            {
                Form1.Invoke(new Action(() =>
                {
                    ProgressBar1.Value = 0;
                    ProgressBar1.Maximum = getHashFilesCount;
                    ProgressBar1.Visible = true;
                    ProgressBar1.Style = ProgressBarStyle.Blocks;
                    Label1.Text = "ファイル調査中...";

                }));
            }

        }

        public void GetHashFilesCount()
        {
            if (ProgressBar1 != null)
            {
                ProgressBar1.Value++;
            }
        }

        public void ALLProsessComplete()
        {
            /// <summary>
            /// 全ての処理を完了して、UIを最初の状態に戻す
            /// </summary>
            if (Form1 != null && ProgressBar1 != null && Label1 != null && Start_button != null && Add_button != null && Remove_button != null && Reset_button != null)
            {
                Form1.Invoke(new Action(() =>
                {
                    ProgressBar1.Visible = false;
                    Label1.Visible = false;
                    Start_button.Enabled = true;
                    Add_button.Enabled = true;
                    Remove_button.Enabled = true;
                    Reset_button.Enabled = true;
                }));
            }
        }

        public void ReadDB()
        {
            /// <summary>
            /// DBデータの読み込み開始
            /// </summary>
            if (Form1 != null && ProgressBar1 != null && Label1 != null)
            {
                Form1.Invoke(new Action(() =>
                {
                    ProgressBar1.Visible = true;
                    ProgressBar1.Style = ProgressBarStyle.Marquee;
                    Label1.Text = "データ一致ファイル一覧作成中...";
                }));
            }
        }
    }
}