namespace gSubberGUI
{
    partial class frmFind
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
            this.cmbTextToFind = new gSubberGUI.Controls.GComboBox();
            this.chkMatchCase = new gSubberGUI.Controls.GCheckBox();
            this.rbtnNormal = new gSubberGUI.Controls.GRadioButton();
            this.rbtnExtended = new gSubberGUI.Controls.GRadioButton();
            this.rbtnRegularExpression = new gSubberGUI.Controls.GRadioButton();
            this.grpSearchMode = new gSubberGUI.Controls.GGroupBox();
            this.chkWholeWord = new gSubberGUI.Controls.GCheckBox();
            this.btnFindNext = new gSubberGUI.Controls.GButton();
            this.btnFindPrevious = new gSubberGUI.Controls.GButton();
            this.btnCount = new gSubberGUI.Controls.GButton();
            this.chkWrapAround = new gSubberGUI.Controls.GCheckBox();
            this.btnClose = new gSubberGUI.Controls.GButton();
            this.grpTransparency = new gSubberGUI.Controls.GGroupBox();
            this.trkTransparency = new System.Windows.Forms.TrackBar();
            this.rbtnTransaparencyAlways = new gSubberGUI.Controls.GRadioButton();
            this.rbtnTransparencyOnLostFocus = new gSubberGUI.Controls.GRadioButton();
            this.chkUseTransparency = new gSubberGUI.Controls.GCheckBox();
            this.grpActions = new gSubberGUI.Controls.GGroupBox();
            this.btnReplaceAll = new gSubberGUI.Controls.GButton();
            this.btnReplacePrevious = new gSubberGUI.Controls.GButton();
            this.btnReplaceNext = new gSubberGUI.Controls.GButton();
            this.grpFindInput = new gSubberGUI.Controls.GGroupBox();
            this.lblReplace = new gSubberGUI.Controls.GLabel();
            this.lblFind = new gSubberGUI.Controls.GLabel();
            this.cmbTextForReplace = new gSubberGUI.Controls.GComboBox();
            this.tlpMain = new gSubberGUI.Controls.GTableLayoutPanel();
            this.tlpUp = new gSubberGUI.Controls.GTableLayoutPanel();
            this.tlpDown = new gSubberGUI.Controls.GTableLayoutPanel();
            this.grpSearchMode.SuspendLayout();
            this.grpTransparency.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkTransparency)).BeginInit();
            this.grpActions.SuspendLayout();
            this.grpFindInput.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tlpUp.SuspendLayout();
            this.tlpDown.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbTextToFind
            // 
            this.cmbTextToFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTextToFind.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.cmbTextToFind.FormattingEnabled = true;
            this.cmbTextToFind.Location = new System.Drawing.Point(66, 21);
            this.cmbTextToFind.Name = "cmbTextToFind";
            this.cmbTextToFind.Size = new System.Drawing.Size(265, 23);
            this.cmbTextToFind.TabIndex = 0;
            // 
            // chkMatchCase
            // 
            this.chkMatchCase.AutoSize = true;
            this.chkMatchCase.Location = new System.Drawing.Point(14, 94);
            this.chkMatchCase.Name = "chkMatchCase";
            this.chkMatchCase.Size = new System.Drawing.Size(88, 19);
            this.chkMatchCase.TabIndex = 1;
            this.chkMatchCase.Text = "Match Case";
            this.chkMatchCase.UseVisualStyleBackColor = true;
            this.chkMatchCase.CheckedChanged += new System.EventHandler(this.chkMatchCase_CheckedChanged);
            // 
            // rbtnNormal
            // 
            this.rbtnNormal.AutoSize = true;
            this.rbtnNormal.Checked = true;
            this.rbtnNormal.Location = new System.Drawing.Point(12, 22);
            this.rbtnNormal.Name = "rbtnNormal";
            this.rbtnNormal.Size = new System.Drawing.Size(65, 19);
            this.rbtnNormal.TabIndex = 2;
            this.rbtnNormal.TabStop = true;
            this.rbtnNormal.Text = "Normal";
            this.rbtnNormal.UseVisualStyleBackColor = true;
            this.rbtnNormal.CheckedChanged += new System.EventHandler(this.rbtnNormal_CheckedChanged);
            // 
            // rbtnExtended
            // 
            this.rbtnExtended.AutoSize = true;
            this.rbtnExtended.Location = new System.Drawing.Point(12, 47);
            this.rbtnExtended.Name = "rbtnExtended";
            this.rbtnExtended.Size = new System.Drawing.Size(142, 19);
            this.rbtnExtended.TabIndex = 3;
            this.rbtnExtended.Text = "Extended (\\n, \\r, \\t, ...)";
            this.rbtnExtended.UseVisualStyleBackColor = true;
            this.rbtnExtended.CheckedChanged += new System.EventHandler(this.rbtnExtended_CheckedChanged);
            // 
            // rbtnRegularExpression
            // 
            this.rbtnRegularExpression.AutoSize = true;
            this.rbtnRegularExpression.Location = new System.Drawing.Point(12, 72);
            this.rbtnRegularExpression.Name = "rbtnRegularExpression";
            this.rbtnRegularExpression.Size = new System.Drawing.Size(124, 19);
            this.rbtnRegularExpression.TabIndex = 4;
            this.rbtnRegularExpression.Text = "Regular Expression";
            this.rbtnRegularExpression.UseVisualStyleBackColor = true;
            this.rbtnRegularExpression.CheckedChanged += new System.EventHandler(this.rbtnRegularExpression_CheckedChanged);
            // 
            // grpSearchMode
            // 
            this.grpSearchMode.Controls.Add(this.rbtnNormal);
            this.grpSearchMode.Controls.Add(this.rbtnRegularExpression);
            this.grpSearchMode.Controls.Add(this.rbtnExtended);
            this.grpSearchMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSearchMode.Location = new System.Drawing.Point(3, 3);
            this.grpSearchMode.Name = "grpSearchMode";
            this.grpSearchMode.Size = new System.Drawing.Size(184, 104);
            this.grpSearchMode.TabIndex = 5;
            this.grpSearchMode.TabStop = false;
            this.grpSearchMode.Text = "Search Mode";
            // 
            // chkWholeWord
            // 
            this.chkWholeWord.AutoSize = true;
            this.chkWholeWord.Location = new System.Drawing.Point(14, 119);
            this.chkWholeWord.Name = "chkWholeWord";
            this.chkWholeWord.Size = new System.Drawing.Size(157, 19);
            this.chkWholeWord.TabIndex = 6;
            this.chkWholeWord.Text = "Match Whole Word Only";
            this.chkWholeWord.UseVisualStyleBackColor = true;
            this.chkWholeWord.CheckedChanged += new System.EventHandler(this.chkWholeWord_CheckedChanged);
            // 
            // btnFindNext
            // 
            this.btnFindNext.Location = new System.Drawing.Point(11, 21);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(105, 30);
            this.btnFindNext.TabIndex = 7;
            this.btnFindNext.Text = "Find Next";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // btnFindPrevious
            // 
            this.btnFindPrevious.Location = new System.Drawing.Point(11, 56);
            this.btnFindPrevious.Name = "btnFindPrevious";
            this.btnFindPrevious.Size = new System.Drawing.Size(105, 30);
            this.btnFindPrevious.TabIndex = 8;
            this.btnFindPrevious.Text = "Find Previous";
            this.btnFindPrevious.UseVisualStyleBackColor = true;
            this.btnFindPrevious.Click += new System.EventHandler(this.btnFindPrevious_Click);
            // 
            // btnCount
            // 
            this.btnCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCount.Location = new System.Drawing.Point(11, 207);
            this.btnCount.Name = "btnCount";
            this.btnCount.Size = new System.Drawing.Size(105, 30);
            this.btnCount.TabIndex = 9;
            this.btnCount.Text = "Count";
            this.btnCount.UseVisualStyleBackColor = true;
            this.btnCount.Click += new System.EventHandler(this.btnCount_Click);
            // 
            // chkWrapAround
            // 
            this.chkWrapAround.AutoSize = true;
            this.chkWrapAround.Location = new System.Drawing.Point(14, 144);
            this.chkWrapAround.Name = "chkWrapAround";
            this.chkWrapAround.Size = new System.Drawing.Size(97, 19);
            this.chkWrapAround.TabIndex = 10;
            this.chkWrapAround.Text = "Wrap Around";
            this.chkWrapAround.UseVisualStyleBackColor = true;
            this.chkWrapAround.CheckedChanged += new System.EventHandler(this.chkWrapAround_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(11, 243);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(105, 30);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grpTransparency
            // 
            this.grpTransparency.Controls.Add(this.trkTransparency);
            this.grpTransparency.Controls.Add(this.rbtnTransaparencyAlways);
            this.grpTransparency.Controls.Add(this.rbtnTransparencyOnLostFocus);
            this.grpTransparency.Controls.Add(this.chkUseTransparency);
            this.grpTransparency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTransparency.Location = new System.Drawing.Point(193, 3);
            this.grpTransparency.Name = "grpTransparency";
            this.grpTransparency.Size = new System.Drawing.Size(288, 104);
            this.grpTransparency.TabIndex = 12;
            this.grpTransparency.TabStop = false;
            this.grpTransparency.Text = "Transparency";
            // 
            // trkTransparency
            // 
            this.trkTransparency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trkTransparency.AutoSize = false;
            this.trkTransparency.Location = new System.Drawing.Point(18, 72);
            this.trkTransparency.Maximum = 100;
            this.trkTransparency.Minimum = 10;
            this.trkTransparency.Name = "trkTransparency";
            this.trkTransparency.Size = new System.Drawing.Size(243, 25);
            this.trkTransparency.TabIndex = 3;
            this.trkTransparency.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkTransparency.Value = 100;
            this.trkTransparency.ValueChanged += new System.EventHandler(this.trkTransparency_ValueChanged);
            // 
            // rbtnTransaparencyAlways
            // 
            this.rbtnTransaparencyAlways.AutoSize = true;
            this.rbtnTransaparencyAlways.Checked = true;
            this.rbtnTransaparencyAlways.Location = new System.Drawing.Point(128, 47);
            this.rbtnTransaparencyAlways.Name = "rbtnTransaparencyAlways";
            this.rbtnTransaparencyAlways.Size = new System.Drawing.Size(62, 19);
            this.rbtnTransaparencyAlways.TabIndex = 2;
            this.rbtnTransaparencyAlways.TabStop = true;
            this.rbtnTransaparencyAlways.Text = "Always";
            this.rbtnTransaparencyAlways.UseVisualStyleBackColor = true;
            this.rbtnTransaparencyAlways.CheckedChanged += new System.EventHandler(this.rbtnTransparency_CheckedChanged);
            // 
            // rbtnTransparencyOnLostFocus
            // 
            this.rbtnTransparencyOnLostFocus.AutoSize = true;
            this.rbtnTransparencyOnLostFocus.Location = new System.Drawing.Point(18, 47);
            this.rbtnTransparencyOnLostFocus.Name = "rbtnTransparencyOnLostFocus";
            this.rbtnTransparencyOnLostFocus.Size = new System.Drawing.Size(100, 19);
            this.rbtnTransparencyOnLostFocus.TabIndex = 1;
            this.rbtnTransparencyOnLostFocus.Text = "On Lost Focus";
            this.rbtnTransparencyOnLostFocus.UseVisualStyleBackColor = true;
            this.rbtnTransparencyOnLostFocus.CheckedChanged += new System.EventHandler(this.rbtnTransparency_CheckedChanged);
            // 
            // chkUseTransparency
            // 
            this.chkUseTransparency.AutoSize = true;
            this.chkUseTransparency.Location = new System.Drawing.Point(18, 22);
            this.chkUseTransparency.Name = "chkUseTransparency";
            this.chkUseTransparency.Size = new System.Drawing.Size(117, 19);
            this.chkUseTransparency.TabIndex = 0;
            this.chkUseTransparency.Text = "Use Transparency";
            this.chkUseTransparency.UseVisualStyleBackColor = true;
            this.chkUseTransparency.CheckedChanged += new System.EventHandler(this.chkUseTransparency_CheckedChanged);
            // 
            // grpActions
            // 
            this.grpActions.Controls.Add(this.btnReplaceAll);
            this.grpActions.Controls.Add(this.btnReplacePrevious);
            this.grpActions.Controls.Add(this.btnReplaceNext);
            this.grpActions.Controls.Add(this.btnClose);
            this.grpActions.Controls.Add(this.btnCount);
            this.grpActions.Controls.Add(this.btnFindPrevious);
            this.grpActions.Controls.Add(this.btnFindNext);
            this.grpActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpActions.Location = new System.Drawing.Point(357, 3);
            this.grpActions.Name = "grpActions";
            this.grpActions.Size = new System.Drawing.Size(124, 285);
            this.grpActions.TabIndex = 13;
            this.grpActions.TabStop = false;
            // 
            // btnReplaceAll
            // 
            this.btnReplaceAll.Location = new System.Drawing.Point(10, 166);
            this.btnReplaceAll.Name = "btnReplaceAll";
            this.btnReplaceAll.Size = new System.Drawing.Size(105, 30);
            this.btnReplaceAll.TabIndex = 14;
            this.btnReplaceAll.Text = "Replace All";
            this.btnReplaceAll.UseVisualStyleBackColor = true;
            this.btnReplaceAll.Click += new System.EventHandler(this.btnReplaceAll_Click);
            // 
            // btnReplacePrevious
            // 
            this.btnReplacePrevious.Location = new System.Drawing.Point(10, 131);
            this.btnReplacePrevious.Name = "btnReplacePrevious";
            this.btnReplacePrevious.Size = new System.Drawing.Size(105, 30);
            this.btnReplacePrevious.TabIndex = 13;
            this.btnReplacePrevious.Text = "Replace Previous";
            this.btnReplacePrevious.UseVisualStyleBackColor = true;
            this.btnReplacePrevious.Click += new System.EventHandler(this.btnReplacePrevious_Click);
            // 
            // btnReplaceNext
            // 
            this.btnReplaceNext.Location = new System.Drawing.Point(10, 96);
            this.btnReplaceNext.Name = "btnReplaceNext";
            this.btnReplaceNext.Size = new System.Drawing.Size(105, 30);
            this.btnReplaceNext.TabIndex = 12;
            this.btnReplaceNext.Text = "Replace Next";
            this.btnReplaceNext.UseVisualStyleBackColor = true;
            this.btnReplaceNext.Click += new System.EventHandler(this.btnReplaceNext_Click);
            // 
            // grpFindInput
            // 
            this.grpFindInput.Controls.Add(this.lblReplace);
            this.grpFindInput.Controls.Add(this.lblFind);
            this.grpFindInput.Controls.Add(this.cmbTextForReplace);
            this.grpFindInput.Controls.Add(this.chkWrapAround);
            this.grpFindInput.Controls.Add(this.chkWholeWord);
            this.grpFindInput.Controls.Add(this.chkMatchCase);
            this.grpFindInput.Controls.Add(this.cmbTextToFind);
            this.grpFindInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpFindInput.Location = new System.Drawing.Point(3, 3);
            this.grpFindInput.Name = "grpFindInput";
            this.grpFindInput.Size = new System.Drawing.Size(348, 285);
            this.grpFindInput.TabIndex = 14;
            this.grpFindInput.TabStop = false;
            // 
            // lblReplace
            // 
            this.lblReplace.AutoSize = true;
            this.lblReplace.Location = new System.Drawing.Point(13, 59);
            this.lblReplace.Name = "lblReplace";
            this.lblReplace.Size = new System.Drawing.Size(48, 15);
            this.lblReplace.TabIndex = 13;
            this.lblReplace.Text = "Replace";
            // 
            // lblFind
            // 
            this.lblFind.AutoSize = true;
            this.lblFind.Location = new System.Drawing.Point(31, 25);
            this.lblFind.Name = "lblFind";
            this.lblFind.Size = new System.Drawing.Size(30, 15);
            this.lblFind.TabIndex = 12;
            this.lblFind.Text = "Find";
            // 
            // cmbTextForReplace
            // 
            this.cmbTextForReplace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTextForReplace.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.cmbTextForReplace.FormattingEnabled = true;
            this.cmbTextForReplace.Location = new System.Drawing.Point(66, 55);
            this.cmbTextForReplace.Name = "cmbTextForReplace";
            this.cmbTextForReplace.Size = new System.Drawing.Size(265, 23);
            this.cmbTextForReplace.TabIndex = 11;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tlpUp, 0, 0);
            this.tlpMain.Controls.Add(this.tlpDown, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpMain.Size = new System.Drawing.Size(484, 401);
            this.tlpMain.TabIndex = 15;
            // 
            // tlpUp
            // 
            this.tlpUp.ColumnCount = 2;
            this.tlpUp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpUp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tlpUp.Controls.Add(this.grpFindInput, 0, 0);
            this.tlpUp.Controls.Add(this.grpActions, 1, 0);
            this.tlpUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpUp.Location = new System.Drawing.Point(0, 0);
            this.tlpUp.Margin = new System.Windows.Forms.Padding(0);
            this.tlpUp.Name = "tlpUp";
            this.tlpUp.RowCount = 1;
            this.tlpUp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpUp.Size = new System.Drawing.Size(484, 291);
            this.tlpUp.TabIndex = 0;
            // 
            // tlpDown
            // 
            this.tlpDown.ColumnCount = 2;
            this.tlpDown.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.tlpDown.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDown.Controls.Add(this.grpSearchMode, 0, 0);
            this.tlpDown.Controls.Add(this.grpTransparency, 1, 0);
            this.tlpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDown.Location = new System.Drawing.Point(0, 291);
            this.tlpDown.Margin = new System.Windows.Forms.Padding(0);
            this.tlpDown.Name = "tlpDown";
            this.tlpDown.RowCount = 1;
            this.tlpDown.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDown.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpDown.Size = new System.Drawing.Size(484, 110);
            this.tlpDown.TabIndex = 1;
            // 
            // frmFind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(484, 401);
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.MaximumSize = new System.Drawing.Size(1200, 440);
            this.MinimumSize = new System.Drawing.Size(500, 440);
            this.Name = "frmFind";
            this.Text = "Find text";
            this.Activated += new System.EventHandler(this.frmFind_Activated);
            this.Deactivate += new System.EventHandler(this.frmFind_Deactivate);
            this.grpSearchMode.ResumeLayout(false);
            this.grpSearchMode.PerformLayout();
            this.grpTransparency.ResumeLayout(false);
            this.grpTransparency.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkTransparency)).EndInit();
            this.grpActions.ResumeLayout(false);
            this.grpFindInput.ResumeLayout(false);
            this.grpFindInput.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.tlpUp.ResumeLayout(false);
            this.tlpDown.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.GComboBox cmbTextToFind;
        private Controls.GCheckBox chkMatchCase;
        private Controls.GRadioButton rbtnNormal;
        private Controls.GRadioButton rbtnExtended;
        private Controls.GRadioButton rbtnRegularExpression;
        private Controls.GGroupBox grpSearchMode;
        private Controls.GCheckBox chkWholeWord;
        private Controls.GButton btnFindNext;
        private Controls.GButton btnFindPrevious;
        private Controls.GButton btnCount;
        private Controls.GCheckBox chkWrapAround;
        private Controls.GButton btnClose;
        private Controls.GGroupBox grpTransparency;
        private System.Windows.Forms.TrackBar trkTransparency;
        private Controls.GRadioButton rbtnTransaparencyAlways;
        private Controls.GRadioButton rbtnTransparencyOnLostFocus;
        private Controls.GCheckBox chkUseTransparency;
        private Controls.GGroupBox grpActions;
        private Controls.GGroupBox grpFindInput;
        private Controls.GTableLayoutPanel tlpMain;
        private Controls.GTableLayoutPanel tlpUp;
        private Controls.GTableLayoutPanel tlpDown;
        private Controls.GComboBox cmbTextForReplace;
        private Controls.GButton btnReplacePrevious;
        private Controls.GButton btnReplaceNext;
        private Controls.GLabel lblReplace;
        private Controls.GLabel lblFind;
        private Controls.GButton btnReplaceAll;
    }
}
