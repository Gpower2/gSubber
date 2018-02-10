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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.TxtInputFile = new System.Windows.Forms.TextBox();
            this.gComboBox1 = new gSubberGUI.Controls.GComboBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(104, 159);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(683, 266);
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
            this.TxtInputFile.Location = new System.Drawing.Point(292, 60);
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
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 571);
            this.Controls.Add(this.gComboBox1);
            this.Controls.Add(this.TxtInputFile);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TxtInputFile;
        private Controls.GComboBox gComboBox1;
    }
}

