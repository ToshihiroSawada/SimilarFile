namespace SimilarFiles
{
    partial class SearchList
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
            components = new System.ComponentModel.Container();
            match_list = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            folder_list_table = new DataGridView();
            search = new DataGridViewTextBoxColumn();
            reset_button = new Button();
            remove_button = new Button();
            add_button = new Button();
            start_button = new Button();
            stop_button = new Button();
            menuStrip1 = new MenuStrip();
            ファイルToolStripMenuItem = new ToolStripMenuItem();
            OpenToolStripMenuItem = new ToolStripMenuItem();
            DBFileToolStripMenuItem1 = new ToolStripMenuItem();
            FolderToolStripMenuItem = new ToolStripMenuItem();
            CloseToolStripMenuItem = new ToolStripMenuItem();
            EndToolStripMenuItem = new ToolStripMenuItem();
            progressBar1 = new ProgressBar();
            CellRightClickMenu = new ContextMenuStrip(components);
            削除ToolStripMenuItem = new ToolStripMenuItem();
            開くToolStripMenuItem1 = new ToolStripMenuItem();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)match_list).BeginInit();
            ((System.ComponentModel.ISupportInitialize)folder_list_table).BeginInit();
            menuStrip1.SuspendLayout();
            CellRightClickMenu.SuspendLayout();
            SuspendLayout();
            // 
            // match_list
            // 
            match_list.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            match_list.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            match_list.ColumnHeadersHeight = 29;
            match_list.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2 });
            match_list.Location = new Point(0, 151);
            match_list.Margin = new Padding(3, 2, 3, 2);
            match_list.Name = "match_list";
            match_list.ReadOnly = true;
            match_list.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            match_list.RowTemplate.Height = 29;
            match_list.RowTemplate.Resizable = DataGridViewTriState.True;
            match_list.SelectionMode = DataGridViewSelectionMode.CellSelect;
            match_list.Size = new Size(1104, 355);
            match_list.TabIndex = 1;
            match_list.CellMouseDoubleClick += MatchList_CellMouseDoubleClick;
            match_list.CellMouseDown += MatchList_CellMouseDown;
            // 
            // Column1
            // 
            Column1.HeaderText = "パス1";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // Column2
            // 
            Column2.HeaderText = "パス2";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // folder_list_table
            // 
            folder_list_table.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            folder_list_table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            folder_list_table.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            folder_list_table.Columns.AddRange(new DataGridViewColumn[] { search });
            folder_list_table.Location = new Point(0, 26);
            folder_list_table.Margin = new Padding(3, 2, 3, 2);
            folder_list_table.Name = "folder_list_table";
            folder_list_table.RowHeadersWidth = 51;
            folder_list_table.RowTemplate.Height = 29;
            folder_list_table.Size = new Size(1104, 94);
            folder_list_table.TabIndex = 2;
            // 
            // search
            // 
            search.HeaderText = "検索リスト";
            search.MinimumWidth = 6;
            search.Name = "search";
            // 
            // reset_button
            // 
            reset_button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            reset_button.Location = new Point(1012, 124);
            reset_button.Margin = new Padding(3, 2, 3, 2);
            reset_button.Name = "reset_button";
            reset_button.Size = new Size(82, 22);
            reset_button.TabIndex = 3;
            reset_button.Text = "リセット";
            reset_button.UseVisualStyleBackColor = true;
            reset_button.Click += ResetButton_Click;
            // 
            // remove_button
            // 
            remove_button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            remove_button.Location = new Point(924, 124);
            remove_button.Margin = new Padding(3, 2, 3, 2);
            remove_button.Name = "remove_button";
            remove_button.Size = new Size(82, 22);
            remove_button.TabIndex = 4;
            remove_button.Text = "削除";
            remove_button.UseVisualStyleBackColor = true;
            remove_button.Click += RemoveButton_Click;
            // 
            // add_button
            // 
            add_button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            add_button.Location = new Point(836, 124);
            add_button.Margin = new Padding(3, 2, 3, 2);
            add_button.Name = "add_button";
            add_button.Size = new Size(82, 22);
            add_button.TabIndex = 5;
            add_button.Text = "追加";
            add_button.UseVisualStyleBackColor = true;
            add_button.Click += AddButton_Click;
            // 
            // start_button
            // 
            start_button.Location = new Point(10, 124);
            start_button.Margin = new Padding(3, 2, 3, 2);
            start_button.Name = "start_button";
            start_button.Size = new Size(82, 22);
            start_button.TabIndex = 6;
            start_button.Text = "開始";
            start_button.UseVisualStyleBackColor = true;
            start_button.Click += StartButton_Click;
            // 
            // stop_button
            // 
            stop_button.Location = new Point(98, 124);
            stop_button.Margin = new Padding(3, 2, 3, 2);
            stop_button.Name = "stop_button";
            stop_button.Size = new Size(82, 22);
            stop_button.TabIndex = 7;
            stop_button.Text = "停止";
            stop_button.UseVisualStyleBackColor = true;
            stop_button.Click += StopButton_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { ファイルToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(1104, 24);
            menuStrip1.TabIndex = 8;
            menuStrip1.Text = "menuStrip1";
            // 
            // ファイルToolStripMenuItem
            // 
            ファイルToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { OpenToolStripMenuItem, CloseToolStripMenuItem, EndToolStripMenuItem });
            ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
            ファイルToolStripMenuItem.Size = new Size(53, 20);
            ファイルToolStripMenuItem.Text = "ファイル";
            // 
            // OpenToolStripMenuItem
            // 
            OpenToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { DBFileToolStripMenuItem1, FolderToolStripMenuItem });
            OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            OpenToolStripMenuItem.Size = new Size(104, 22);
            OpenToolStripMenuItem.Text = "開く";
            // 
            // DBFileToolStripMenuItem1
            // 
            DBFileToolStripMenuItem1.Name = "DBFileToolStripMenuItem1";
            DBFileToolStripMenuItem1.Size = new Size(123, 22);
            DBFileToolStripMenuItem1.Text = "DBファイル";
            DBFileToolStripMenuItem1.Click += DBFileToolStripMenuItem1_Click;
            // 
            // FolderToolStripMenuItem
            // 
            FolderToolStripMenuItem.Name = "FolderToolStripMenuItem";
            FolderToolStripMenuItem.Size = new Size(123, 22);
            FolderToolStripMenuItem.Text = "フォルダー";
            FolderToolStripMenuItem.Click += FolderToolStripMenuItem_Click;
            // 
            // CloseToolStripMenuItem
            // 
            CloseToolStripMenuItem.Name = "CloseToolStripMenuItem";
            CloseToolStripMenuItem.Size = new Size(104, 22);
            CloseToolStripMenuItem.Text = "閉じる";
            // 
            // EndToolStripMenuItem
            // 
            EndToolStripMenuItem.Name = "EndToolStripMenuItem";
            EndToolStripMenuItem.Size = new Size(104, 22);
            EndToolStripMenuItem.Text = "終了";
            EndToolStripMenuItem.Click += EndToolStripMenuItem_Click;
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.Location = new Point(0, 416);
            progressBar1.Margin = new Padding(3, 2, 3, 2);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(1104, 22);
            progressBar1.TabIndex = 9;
            progressBar1.Visible = false;
            // 
            // CellRightClickMenu
            // 
            CellRightClickMenu.ImageScalingSize = new Size(20, 20);
            CellRightClickMenu.Items.AddRange(new ToolStripItem[] { 削除ToolStripMenuItem, 開くToolStripMenuItem1 });
            CellRightClickMenu.Name = "contextMenuStrip1";
            CellRightClickMenu.Size = new Size(99, 48);
            // 
            // 削除ToolStripMenuItem
            // 
            削除ToolStripMenuItem.Name = "削除ToolStripMenuItem";
            削除ToolStripMenuItem.Size = new Size(98, 22);
            削除ToolStripMenuItem.Text = "削除";
            // 
            // 開くToolStripMenuItem1
            // 
            開くToolStripMenuItem1.Name = "開くToolStripMenuItem1";
            開くToolStripMenuItem1.Size = new Size(98, 22);
            開くToolStripMenuItem1.Text = "開く";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 19.8F);
            label1.Location = new Point(10, 380);
            label1.Name = "label1";
            label1.Size = new Size(146, 37);
            label1.TabIndex = 10;
            label1.Text = "読み込み中";
            label1.Visible = false;
            // 
            // SearchList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1104, 505);
            Controls.Add(label1);
            Controls.Add(progressBar1);
            Controls.Add(stop_button);
            Controls.Add(start_button);
            Controls.Add(add_button);
            Controls.Add(remove_button);
            Controls.Add(reset_button);
            Controls.Add(folder_list_table);
            Controls.Add(match_list);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            Name = "SearchList";
            Text = "SimilarFiles";
            FormClosing += SearchList_FormClosing;
            ((System.ComponentModel.ISupportInitialize)match_list).EndInit();
            ((System.ComponentModel.ISupportInitialize)folder_list_table).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            CellRightClickMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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
        private ToolStripMenuItem OpenToolStripMenuItem;
        private ToolStripMenuItem CloseToolStripMenuItem;
        private ToolStripMenuItem EndToolStripMenuItem;
        private ProgressBar progressBar1;
        private ToolStripMenuItem DBFileToolStripMenuItem1;
        private ToolStripMenuItem FolderToolStripMenuItem;
        private ContextMenuStrip CellRightClickMenu;
        private ToolStripMenuItem 削除ToolStripMenuItem;
        private ToolStripMenuItem 開くToolStripMenuItem1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private Label label1;
    }
}