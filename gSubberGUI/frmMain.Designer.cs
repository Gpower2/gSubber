namespace gSubberGUI
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnLoad = new System.Windows.Forms.Button();
            this.gFilePicker1 = new gSubberGUI.Controls.GFilePicker();
            this.grdSubtitles = new gSubberGUI.Controls.GDataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixIssuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpSubtitles = new gSubberGUI.Controls.GGroupBox();
            this.grpInputFile = new gSubberGUI.Controls.GGroupBox();
            this.tlpInputFile = new gSubberGUI.Controls.GTableLayoutPanel();
            this.grpProperties = new gSubberGUI.Controls.GGroupBox();
            this.lstProperties = new gSubberGUI.Controls.GListBox();
            this.grpInformation = new gSubberGUI.Controls.GGroupBox();
            this.lstInfo = new gSubberGUI.Controls.GListBox();
            this.grpStyles = new gSubberGUI.Controls.GGroupBox();
            this.lstStyles = new gSubberGUI.Controls.GListBox();
            this.grpAttachments = new gSubberGUI.Controls.GGroupBox();
            this.lstAttachments = new gSubberGUI.Controls.GListBox();
            this.gSubtitleItem = new gSubberGUI.Controls.GGroupBox();
            this.tlpSubtitleItem = new gSubberGUI.Controls.GTableLayoutPanel();
            this.txtSubtitleItem = new gSubberGUI.Controls.GTextBox();
            this.tlpFileExtras = new gSubberGUI.Controls.GTableLayoutPanel();
            this.tlpSubtitles = new gSubberGUI.Controls.GTableLayoutPanel();
            this.tlpMain = new gSubberGUI.Controls.GTableLayoutPanel();
            this.spltMain = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubtitles)).BeginInit();
            this.menuStripMain.SuspendLayout();
            this.grpSubtitles.SuspendLayout();
            this.grpInputFile.SuspendLayout();
            this.tlpInputFile.SuspendLayout();
            this.grpProperties.SuspendLayout();
            this.grpInformation.SuspendLayout();
            this.grpStyles.SuspendLayout();
            this.grpAttachments.SuspendLayout();
            this.gSubtitleItem.SuspendLayout();
            this.tlpSubtitleItem.SuspendLayout();
            this.tlpFileExtras.SuspendLayout();
            this.tlpSubtitles.SuspendLayout();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltMain)).BeginInit();
            this.spltMain.Panel1.SuspendLayout();
            this.spltMain.Panel2.SuspendLayout();
            this.spltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLoad.Location = new System.Drawing.Point(1066, 3);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(70, 28);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load...";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // gFilePicker1
            // 
            this.gFilePicker1.AllowManualInput = false;
            this.gFilePicker1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gFilePicker1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.gFilePicker1.Location = new System.Drawing.Point(4, 5);
            this.gFilePicker1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gFilePicker1.Name = "gFilePicker1";
            this.gFilePicker1.OpenFileDialogFilter = "";
            this.gFilePicker1.OpenFileDialogTitle = "Select an input file...";
            this.gFilePicker1.Size = new System.Drawing.Size(1055, 30);
            this.gFilePicker1.TabIndex = 5;
            // 
            // grdSubtitles
            // 
            this.grdSubtitles.AllowUserToAddRows = false;
            this.grdSubtitles.AllowUserToDeleteRows = false;
            this.grdSubtitles.AllowUserToOrderColumns = true;
            this.grdSubtitles.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MintCream;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdSubtitles.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdSubtitles.BackgroundColor = System.Drawing.Color.GhostWhite;
            this.grdSubtitles.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grdSubtitles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSubtitles.CustomColumnFormats = "";
            this.grdSubtitles.CustomRowAlternativeBackgroundColor = System.Drawing.Color.MintCream;
            this.grdSubtitles.CustomRowAlternativeForeColor = System.Drawing.Color.Black;
            this.grdSubtitles.CustomRowBackgroundColor = System.Drawing.Color.White;
            this.grdSubtitles.CustomRowForeColor = System.Drawing.Color.Black;
            this.grdSubtitles.CustomRowSelectionBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.grdSubtitles.CustomRowSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdSubtitles.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdSubtitles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSubtitles.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdSubtitles.GridColor = System.Drawing.Color.Gainsboro;
            this.grdSubtitles.KeyColumnIndex = -1;
            this.grdSubtitles.LastClickedColumnIndex = -1;
            this.grdSubtitles.LastClickedRowIndex = -1;
            this.grdSubtitles.Location = new System.Drawing.Point(3, 19);
            this.grdSubtitles.MultiSelect = false;
            this.grdSubtitles.Name = "grdSubtitles";
            this.grdSubtitles.ReadOnly = true;
            this.grdSubtitles.RowHeadersVisible = false;
            this.grdSubtitles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdSubtitles.Size = new System.Drawing.Size(962, 341);
            this.grdSubtitles.TabIndex = 6;
            this.grdSubtitles.SelectionChanged += new System.EventHandler(this.grdSubtitles_SelectionChanged);
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(1142, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 28);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save...";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1227, 24);
            this.menuStripMain.TabIndex = 8;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator1,
            this.findToolStripMenuItem,
            this.replaceToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Enabled = false;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Enabled = false;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.findToolStripMenuItem.Text = "Find...";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.replaceToolStripMenuItem.Text = "Replace...";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fixIssuesToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // fixIssuesToolStripMenuItem
            // 
            this.fixIssuesToolStripMenuItem.Name = "fixIssuesToolStripMenuItem";
            this.fixIssuesToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.fixIssuesToolStripMenuItem.Text = "Fix issues...";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // grpSubtitles
            // 
            this.grpSubtitles.Controls.Add(this.grdSubtitles);
            this.grpSubtitles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSubtitles.Location = new System.Drawing.Point(3, 3);
            this.grpSubtitles.Name = "grpSubtitles";
            this.grpSubtitles.Size = new System.Drawing.Size(968, 363);
            this.grpSubtitles.TabIndex = 9;
            this.grpSubtitles.TabStop = false;
            this.grpSubtitles.Text = "Subtitles";
            // 
            // grpInputFile
            // 
            this.grpInputFile.Controls.Add(this.tlpInputFile);
            this.grpInputFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInputFile.Location = new System.Drawing.Point(3, 3);
            this.grpInputFile.Name = "grpInputFile";
            this.grpInputFile.Size = new System.Drawing.Size(1221, 56);
            this.grpInputFile.TabIndex = 10;
            this.grpInputFile.TabStop = false;
            this.grpInputFile.Text = "Input File";
            // 
            // tlpInputFile
            // 
            this.tlpInputFile.ColumnCount = 3;
            this.tlpInputFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInputFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tlpInputFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tlpInputFile.Controls.Add(this.gFilePicker1, 0, 0);
            this.tlpInputFile.Controls.Add(this.btnLoad, 1, 0);
            this.tlpInputFile.Controls.Add(this.btnSave, 2, 0);
            this.tlpInputFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInputFile.Location = new System.Drawing.Point(3, 19);
            this.tlpInputFile.Name = "tlpInputFile";
            this.tlpInputFile.RowCount = 1;
            this.tlpInputFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInputFile.Size = new System.Drawing.Size(1215, 34);
            this.tlpInputFile.TabIndex = 0;
            // 
            // grpProperties
            // 
            this.grpProperties.Controls.Add(this.lstProperties);
            this.grpProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpProperties.Location = new System.Drawing.Point(3, 125);
            this.grpProperties.Name = "grpProperties";
            this.grpProperties.Size = new System.Drawing.Size(237, 116);
            this.grpProperties.TabIndex = 11;
            this.grpProperties.TabStop = false;
            this.grpProperties.Text = "Properties";
            // 
            // lstProperties
            // 
            this.lstProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstProperties.FormattingEnabled = true;
            this.lstProperties.ItemHeight = 15;
            this.lstProperties.Location = new System.Drawing.Point(3, 19);
            this.lstProperties.Name = "lstProperties";
            this.lstProperties.Size = new System.Drawing.Size(231, 94);
            this.lstProperties.TabIndex = 0;
            // 
            // grpInformation
            // 
            this.grpInformation.Controls.Add(this.lstInfo);
            this.grpInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInformation.Location = new System.Drawing.Point(3, 3);
            this.grpInformation.Name = "grpInformation";
            this.grpInformation.Size = new System.Drawing.Size(237, 116);
            this.grpInformation.TabIndex = 12;
            this.grpInformation.TabStop = false;
            this.grpInformation.Text = "Information";
            // 
            // lstInfo
            // 
            this.lstInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstInfo.FormattingEnabled = true;
            this.lstInfo.ItemHeight = 15;
            this.lstInfo.Location = new System.Drawing.Point(3, 19);
            this.lstInfo.Name = "lstInfo";
            this.lstInfo.Size = new System.Drawing.Size(231, 94);
            this.lstInfo.TabIndex = 0;
            // 
            // grpStyles
            // 
            this.grpStyles.Controls.Add(this.lstStyles);
            this.grpStyles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpStyles.Location = new System.Drawing.Point(3, 247);
            this.grpStyles.Name = "grpStyles";
            this.grpStyles.Size = new System.Drawing.Size(237, 116);
            this.grpStyles.TabIndex = 13;
            this.grpStyles.TabStop = false;
            this.grpStyles.Text = "Styles";
            // 
            // lstStyles
            // 
            this.lstStyles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstStyles.FormattingEnabled = true;
            this.lstStyles.ItemHeight = 15;
            this.lstStyles.Location = new System.Drawing.Point(3, 19);
            this.lstStyles.Name = "lstStyles";
            this.lstStyles.Size = new System.Drawing.Size(231, 94);
            this.lstStyles.TabIndex = 0;
            // 
            // grpAttachments
            // 
            this.grpAttachments.Controls.Add(this.lstAttachments);
            this.grpAttachments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAttachments.Location = new System.Drawing.Point(3, 369);
            this.grpAttachments.Name = "grpAttachments";
            this.grpAttachments.Size = new System.Drawing.Size(237, 117);
            this.grpAttachments.TabIndex = 14;
            this.grpAttachments.TabStop = false;
            this.grpAttachments.Text = "Attachments";
            // 
            // lstAttachments
            // 
            this.lstAttachments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAttachments.FormattingEnabled = true;
            this.lstAttachments.ItemHeight = 15;
            this.lstAttachments.Location = new System.Drawing.Point(3, 19);
            this.lstAttachments.Name = "lstAttachments";
            this.lstAttachments.Size = new System.Drawing.Size(231, 95);
            this.lstAttachments.TabIndex = 0;
            // 
            // gSubtitleItem
            // 
            this.gSubtitleItem.Controls.Add(this.tlpSubtitleItem);
            this.gSubtitleItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gSubtitleItem.Location = new System.Drawing.Point(3, 372);
            this.gSubtitleItem.Name = "gSubtitleItem";
            this.gSubtitleItem.Size = new System.Drawing.Size(968, 114);
            this.gSubtitleItem.TabIndex = 15;
            this.gSubtitleItem.TabStop = false;
            this.gSubtitleItem.Text = "Subtitle Item";
            // 
            // tlpSubtitleItem
            // 
            this.tlpSubtitleItem.ColumnCount = 2;
            this.tlpSubtitleItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSubtitleItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpSubtitleItem.Controls.Add(this.txtSubtitleItem, 0, 1);
            this.tlpSubtitleItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSubtitleItem.Location = new System.Drawing.Point(3, 19);
            this.tlpSubtitleItem.Name = "tlpSubtitleItem";
            this.tlpSubtitleItem.RowCount = 2;
            this.tlpSubtitleItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpSubtitleItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSubtitleItem.Size = new System.Drawing.Size(962, 92);
            this.tlpSubtitleItem.TabIndex = 0;
            // 
            // txtSubtitleItem
            // 
            this.txtSubtitleItem.DataObject = null;
            this.txtSubtitleItem.Decimals = 2;
            this.txtSubtitleItem.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSubtitleItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSubtitleItem.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.txtSubtitleItem.HideSelection = false;
            this.txtSubtitleItem.Int32Value = 0;
            this.txtSubtitleItem.Int64Value = ((long)(0));
            this.txtSubtitleItem.Location = new System.Drawing.Point(3, 33);
            this.txtSubtitleItem.Multiline = true;
            this.txtSubtitleItem.Name = "txtSubtitleItem";
            this.txtSubtitleItem.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSubtitleItem.Size = new System.Drawing.Size(926, 56);
            this.txtSubtitleItem.TabIndex = 0;
            this.txtSubtitleItem.TextBoxType = gSubberGUI.Controls.GTextBox.GTextBoxType.Text;
            // 
            // tlpFileExtras
            // 
            this.tlpFileExtras.ColumnCount = 1;
            this.tlpFileExtras.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFileExtras.Controls.Add(this.grpInformation, 0, 0);
            this.tlpFileExtras.Controls.Add(this.grpProperties, 0, 1);
            this.tlpFileExtras.Controls.Add(this.grpAttachments, 0, 3);
            this.tlpFileExtras.Controls.Add(this.grpStyles, 0, 2);
            this.tlpFileExtras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFileExtras.Location = new System.Drawing.Point(0, 0);
            this.tlpFileExtras.Name = "tlpFileExtras";
            this.tlpFileExtras.RowCount = 4;
            this.tlpFileExtras.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpFileExtras.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpFileExtras.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpFileExtras.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpFileExtras.Size = new System.Drawing.Size(243, 489);
            this.tlpFileExtras.TabIndex = 16;
            // 
            // tlpSubtitles
            // 
            this.tlpSubtitles.ColumnCount = 1;
            this.tlpSubtitles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSubtitles.Controls.Add(this.grpSubtitles, 0, 0);
            this.tlpSubtitles.Controls.Add(this.gSubtitleItem, 0, 1);
            this.tlpSubtitles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSubtitles.Location = new System.Drawing.Point(0, 0);
            this.tlpSubtitles.Name = "tlpSubtitles";
            this.tlpSubtitles.RowCount = 2;
            this.tlpSubtitles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSubtitles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpSubtitles.Size = new System.Drawing.Size(974, 489);
            this.tlpSubtitles.TabIndex = 17;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Controls.Add(this.grpInputFile, 0, 0);
            this.tlpMain.Controls.Add(this.spltMain, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 24);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1227, 557);
            this.tlpMain.TabIndex = 18;
            // 
            // spltMain
            // 
            this.spltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltMain.Location = new System.Drawing.Point(3, 65);
            this.spltMain.Name = "spltMain";
            // 
            // spltMain.Panel1
            // 
            this.spltMain.Panel1.Controls.Add(this.tlpFileExtras);
            // 
            // spltMain.Panel2
            // 
            this.spltMain.Panel2.Controls.Add(this.tlpSubtitles);
            this.spltMain.Size = new System.Drawing.Size(1221, 489);
            this.spltMain.SplitterDistance = 243;
            this.spltMain.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1227, 581);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.menuStripMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "frmMain";
            this.Text = "gSubber";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.grdSubtitles)).EndInit();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.grpSubtitles.ResumeLayout(false);
            this.grpInputFile.ResumeLayout(false);
            this.tlpInputFile.ResumeLayout(false);
            this.grpProperties.ResumeLayout(false);
            this.grpInformation.ResumeLayout(false);
            this.grpStyles.ResumeLayout(false);
            this.grpAttachments.ResumeLayout(false);
            this.gSubtitleItem.ResumeLayout(false);
            this.tlpSubtitleItem.ResumeLayout(false);
            this.tlpSubtitleItem.PerformLayout();
            this.tlpFileExtras.ResumeLayout(false);
            this.tlpSubtitles.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.spltMain.Panel1.ResumeLayout(false);
            this.spltMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltMain)).EndInit();
            this.spltMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnLoad;
        private Controls.GFilePicker gFilePicker1;
        private Controls.GDataGridView grdSubtitles;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private Controls.GGroupBox grpSubtitles;
        private Controls.GGroupBox grpInputFile;
        private Controls.GTableLayoutPanel tlpInputFile;
        private Controls.GGroupBox grpProperties;
        private Controls.GGroupBox grpInformation;
        private Controls.GGroupBox grpStyles;
        private Controls.GGroupBox grpAttachments;
        private Controls.GTableLayoutPanel tlpMain;
        private Controls.GTableLayoutPanel tlpFileExtras;
        private Controls.GTableLayoutPanel tlpSubtitles;
        private Controls.GGroupBox gSubtitleItem;
        private Controls.GTableLayoutPanel tlpSubtitleItem;
        private Controls.GTextBox txtSubtitleItem;
        private Controls.GListBox lstInfo;
        private Controls.GListBox lstProperties;
        private Controls.GListBox lstAttachments;
        private Controls.GListBox lstStyles;
        private System.Windows.Forms.SplitContainer spltMain;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fixIssuesToolStripMenuItem;
    }
}

