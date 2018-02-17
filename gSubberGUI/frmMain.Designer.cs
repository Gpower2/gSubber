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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.TxtInputFile = new System.Windows.Forms.TextBox();
            this.gComboBox1 = new gSubberGUI.Controls.GComboBox();
            this.gTextBox1 = new gSubberGUI.Controls.GTextBox();
            this.gFilePicker1 = new gSubberGUI.Controls.GFilePicker();
            this.gDataGridView1 = new gSubberGUI.Controls.GDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(104, 159);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(591, 118);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(104, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 49);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TxtInputFile
            // 
            this.TxtInputFile.AllowDrop = true;
            this.TxtInputFile.Location = new System.Drawing.Point(298, 78);
            this.TxtInputFile.Name = "TxtInputFile";
            this.TxtInputFile.Size = new System.Drawing.Size(460, 20);
            this.TxtInputFile.TabIndex = 2;
            this.TxtInputFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.TxtInputFile_DragDrop);
            this.TxtInputFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.TxtInputFile_DragEnter);
            // 
            // gComboBox1
            // 
            this.gComboBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.gComboBox1.FormattingEnabled = true;
            this.gComboBox1.Location = new System.Drawing.Point(316, 109);
            this.gComboBox1.Name = "gComboBox1";
            this.gComboBox1.Size = new System.Drawing.Size(192, 23);
            this.gComboBox1.TabIndex = 3;
            // 
            // gTextBox1
            // 
            this.gTextBox1.DataObject = null;
            this.gTextBox1.Decimals = 2;
            this.gTextBox1.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.gTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.gTextBox1.Int32Value = 0;
            this.gTextBox1.Int64Value = ((long)(0));
            this.gTextBox1.Location = new System.Drawing.Point(566, 104);
            this.gTextBox1.Name = "gTextBox1";
            this.gTextBox1.Size = new System.Drawing.Size(146, 23);
            this.gTextBox1.TabIndex = 4;
            this.gTextBox1.TextBoxType = gSubberGUI.Controls.GTextBox.GTextBoxType.Text;
            // 
            // gFilePicker1
            // 
            this.gFilePicker1.AllowManualInput = false;
            this.gFilePicker1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.gFilePicker1.Location = new System.Drawing.Point(398, 17);
            this.gFilePicker1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gFilePicker1.Name = "gFilePicker1";
            this.gFilePicker1.OpenFileDialogFilter = "";
            this.gFilePicker1.OpenFileDialogTitle = "Select an input file...";
            this.gFilePicker1.Size = new System.Drawing.Size(462, 35);
            this.gFilePicker1.TabIndex = 5;
            // 
            // gDataGridView1
            // 
            this.gDataGridView1.AllowUserToAddRows = false;
            this.gDataGridView1.AllowUserToDeleteRows = false;
            this.gDataGridView1.AllowUserToOrderColumns = true;
            this.gDataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MintCream;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.gDataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gDataGridView1.BackgroundColor = System.Drawing.Color.GhostWhite;
            this.gDataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gDataGridView1.CustomRowAlternativeBackgroundColor = System.Drawing.Color.MintCream;
            this.gDataGridView1.CustomRowAlternativeForeColor = System.Drawing.Color.Black;
            this.gDataGridView1.CustomRowBackgroundColor = System.Drawing.Color.White;
            this.gDataGridView1.CustomRowForeColor = System.Drawing.Color.Black;
            this.gDataGridView1.CustomRowSelectionBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.gDataGridView1.CustomRowSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gDataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.gDataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gDataGridView1.GridColor = System.Drawing.Color.Gainsboro;
            this.gDataGridView1.KeyColumnIndex = -1;
            this.gDataGridView1.LastClickedColumnIndex = -1;
            this.gDataGridView1.LastClickedRowIndex = -1;
            this.gDataGridView1.Location = new System.Drawing.Point(106, 294);
            this.gDataGridView1.MultiSelect = false;
            this.gDataGridView1.Name = "gDataGridView1";
            this.gDataGridView1.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gDataGridView1.RowHeadersVisible = false;
            this.gDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gDataGridView1.Size = new System.Drawing.Size(767, 250);
            this.gDataGridView1.TabIndex = 6;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(967, 571);
            this.Controls.Add(this.gDataGridView1);
            this.Controls.Add(this.gFilePicker1);
            this.Controls.Add(this.gTextBox1);
            this.Controls.Add(this.gComboBox1);
            this.Controls.Add(this.TxtInputFile);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "frmMain";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.gDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TxtInputFile;
        private Controls.GComboBox gComboBox1;
        private Controls.GTextBox gTextBox1;
        private Controls.GFilePicker gFilePicker1;
        private Controls.GDataGridView gDataGridView1;
    }
}

