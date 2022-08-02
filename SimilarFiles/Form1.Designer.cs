namespace SimilarFiles
{
    partial class search_list
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.match_list = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folder_list_table = new System.Windows.Forms.DataGridView();
            this.search = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reset_button = new System.Windows.Forms.Button();
            this.remove_button = new System.Windows.Forms.Button();
            this.add_button = new System.Windows.Forms.Button();
            this.start_button = new System.Windows.Forms.Button();
            this.stop_button = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DBファイルToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.フォルダーToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.閉じるToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.CellRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.開くToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.match_list)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.folder_list_table)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.CellRightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // match_list
            // 
            this.match_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.match_list.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.match_list.ColumnHeadersHeight = 29;
            this.match_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.match_list.Location = new System.Drawing.Point(0, 201);
            this.match_list.Name = "match_list";
            this.match_list.ReadOnly = true;
            this.match_list.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.match_list.RowTemplate.Height = 29;
            this.match_list.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.match_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.match_list.Size = new System.Drawing.Size(1262, 473);
            this.match_list.TabIndex = 1;
            this.match_list.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.MatchList_CellMouseDoubleClick);
            this.match_list.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.MatchList_CellMouseDown);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "パス1";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "パス2";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // folder_list_table
            // 
            this.folder_list_table.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.folder_list_table.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.folder_list_table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.folder_list_table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.search});
            this.folder_list_table.Location = new System.Drawing.Point(0, 35);
            this.folder_list_table.Name = "folder_list_table";
            this.folder_list_table.RowHeadersWidth = 51;
            this.folder_list_table.RowTemplate.Height = 29;
            this.folder_list_table.Size = new System.Drawing.Size(1262, 125);
            this.folder_list_table.TabIndex = 2;
            // 
            // search
            // 
            this.search.HeaderText = "検索リスト";
            this.search.MinimumWidth = 6;
            this.search.Name = "search";
            // 
            // reset_button
            // 
            this.reset_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reset_button.Location = new System.Drawing.Point(1156, 166);
            this.reset_button.Name = "reset_button";
            this.reset_button.Size = new System.Drawing.Size(94, 29);
            this.reset_button.TabIndex = 3;
            this.reset_button.Text = "リセット";
            this.reset_button.UseVisualStyleBackColor = true;
            this.reset_button.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // remove_button
            // 
            this.remove_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.remove_button.Location = new System.Drawing.Point(1056, 166);
            this.remove_button.Name = "remove_button";
            this.remove_button.Size = new System.Drawing.Size(94, 29);
            this.remove_button.TabIndex = 4;
            this.remove_button.Text = "削除";
            this.remove_button.UseVisualStyleBackColor = true;
            this.remove_button.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // add_button
            // 
            this.add_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.add_button.Location = new System.Drawing.Point(956, 166);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(94, 29);
            this.add_button.TabIndex = 5;
            this.add_button.Text = "追加";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(12, 166);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(94, 29);
            this.start_button.TabIndex = 6;
            this.start_button.Text = "開始";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // stop_button
            // 
            this.stop_button.Location = new System.Drawing.Point(112, 166);
            this.stop_button.Name = "stop_button";
            this.stop_button.Size = new System.Drawing.Size(94, 29);
            this.stop_button.TabIndex = 7;
            this.stop_button.Text = "停止";
            this.stop_button.UseVisualStyleBackColor = true;
            this.stop_button.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1262, 28);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルToolStripMenuItem
            // 
            this.ファイルToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開くToolStripMenuItem,
            this.閉じるToolStripMenuItem,
            this.終了ToolStripMenuItem});
            this.ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
            this.ファイルToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.ファイルToolStripMenuItem.Text = "ファイル";
            // 
            // 開くToolStripMenuItem
            // 
            this.開くToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DBファイルToolStripMenuItem1,
            this.フォルダーToolStripMenuItem});
            this.開くToolStripMenuItem.Name = "開くToolStripMenuItem";
            this.開くToolStripMenuItem.Size = new System.Drawing.Size(129, 26);
            this.開くToolStripMenuItem.Text = "開く";
            // 
            // DBファイルToolStripMenuItem1
            // 
            this.DBファイルToolStripMenuItem1.Name = "DBファイルToolStripMenuItem1";
            this.DBファイルToolStripMenuItem1.Size = new System.Drawing.Size(154, 26);
            this.DBファイルToolStripMenuItem1.Text = "DBファイル";
            this.DBファイルToolStripMenuItem1.Click += new System.EventHandler(this.DBファイルToolStripMenuItem1_Click);
            // 
            // フォルダーToolStripMenuItem
            // 
            this.フォルダーToolStripMenuItem.Name = "フォルダーToolStripMenuItem";
            this.フォルダーToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.フォルダーToolStripMenuItem.Text = "フォルダー";
            this.フォルダーToolStripMenuItem.Click += new System.EventHandler(this.フォルダーToolStripMenuItem_Click);
            // 
            // 閉じるToolStripMenuItem
            // 
            this.閉じるToolStripMenuItem.Name = "閉じるToolStripMenuItem";
            this.閉じるToolStripMenuItem.Size = new System.Drawing.Size(129, 26);
            this.閉じるToolStripMenuItem.Text = "閉じる";
            // 
            // 終了ToolStripMenuItem
            // 
            this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
            this.終了ToolStripMenuItem.Size = new System.Drawing.Size(129, 26);
            this.終了ToolStripMenuItem.Text = "終了";
            this.終了ToolStripMenuItem.Click += new System.EventHandler(this.終了ToolStripMenuItem_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(0, 555);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1262, 29);
            this.progressBar1.TabIndex = 9;
            this.progressBar1.Visible = false;
            // 
            // CellRightClickMenu
            // 
            this.CellRightClickMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.CellRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.削除ToolStripMenuItem,
            this.開くToolStripMenuItem1});
            this.CellRightClickMenu.Name = "contextMenuStrip1";
            this.CellRightClickMenu.Size = new System.Drawing.Size(109, 52);
            // 
            // 削除ToolStripMenuItem
            // 
            this.削除ToolStripMenuItem.Name = "削除ToolStripMenuItem";
            this.削除ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.削除ToolStripMenuItem.Text = "削除";
            // 
            // 開くToolStripMenuItem1
            // 
            this.開くToolStripMenuItem1.Name = "開くToolStripMenuItem1";
            this.開くToolStripMenuItem1.Size = new System.Drawing.Size(108, 24);
            this.開くToolStripMenuItem1.Text = "開く";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 506);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 46);
            this.label1.TabIndex = 10;
            this.label1.Text = "読み込み中";
            this.label1.Visible = false;
            // 
            // search_list
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.stop_button);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.add_button);
            this.Controls.Add(this.remove_button);
            this.Controls.Add(this.reset_button);
            this.Controls.Add(this.folder_list_table);
            this.Controls.Add(this.match_list);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "search_list";
            this.Text = "SimilarFiles";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchList_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.match_list)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.folder_list_table)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.CellRightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DataGridView folder_list_table;
        private Button reset_button;
        private Button remove_button;
        private Button add_button;
        private DataGridViewTextBoxColumn search;
        private Button start_button;
        private Button stop_button;
        public DataGridView match_list;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem ファイルToolStripMenuItem;
        private ToolStripMenuItem 開くToolStripMenuItem;
        private ToolStripMenuItem 閉じるToolStripMenuItem;
        private ToolStripMenuItem 終了ToolStripMenuItem;
        private ProgressBar progressBar1;
        private ToolStripMenuItem DBファイルToolStripMenuItem1;
        private ToolStripMenuItem フォルダーToolStripMenuItem;
        private ContextMenuStrip CellRightClickMenu;
        private ToolStripMenuItem 削除ToolStripMenuItem;
        private ToolStripMenuItem 開くToolStripMenuItem1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private Label label1;
    }
}