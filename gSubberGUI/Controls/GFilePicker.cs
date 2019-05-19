using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace gSubberGUI.Controls
{
    public partial class GFilePicker : UserControl
    {
        [Browsable(true)]
        public new Font Font
        {
            get
            {
                return txtFile.Font;
            }
            set
            {
                base.Font = value;                
                txtFile.Font = value;
                btnBrowse.Font = value;
                SetSize();
            }
        }

        protected String _OpenFileDialogTitle = "Select an input file...";

        [Browsable(true)]
        public String OpenFileDialogTitle
        {
            get
            {
                return _OpenFileDialogTitle;
            }
            set
            {
                _OpenFileDialogTitle = value;
            }
        }

        protected String _OpenFileDialogFilter = "";

        [Browsable(true)]
        public String OpenFileDialogFilter
        {
            get
            {
                return _OpenFileDialogFilter;
            }
            set
            {
                _OpenFileDialogFilter = value;
            }
        }

        protected bool _AllowManualInput = true;

        [Browsable(true)]
        public bool AllowManualInput
        {
            get
            {
                return _AllowManualInput;
            }
            set
            {
                _AllowManualInput = value;
                txtFile.ReadOnly = !_AllowManualInput;
            }
        }

        public override String Text
        {
            get { return txtFile.Text; }
            set { txtFile.Text = Text; }
        }

        public new delegate void OnTextChanged(object sender, EventArgs e);
        public new event OnTextChanged TextChanged;

        private void txtFile_TextChanged(object sender, EventArgs e)
        {
            // Make sure someone is listening to event
            if (TextChanged == null) return;

            TextChanged(sender, e);
        }

        public GFilePicker()
        {
            this.DoubleBuffered = true;
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            InitializeComponent();

            SetSize();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (!String.IsNullOrWhiteSpace(txtFile.Text))
                {
                    if (Directory.Exists(Path.GetDirectoryName(txtFile.Text.Trim())))
                    {
                        ofd.InitialDirectory = Path.GetDirectoryName(txtFile.Text.Trim());
                    }
                }
                ofd.Title = _OpenFileDialogTitle;
                ofd.Filter = _OpenFileDialogFilter;
                ofd.Multiselect = false;
                ofd.AutoUpgradeEnabled = true;
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtFile.Text = ofd.FileName;
                }
            }
            catch (Exception ex)
            {
                ex.ShowException();
            }
        }

        private void GFilePicker_Resize(object sender, EventArgs e)
        {
            SetSize();
        }

        private void SetSize()
        {
            if (Height != btnBrowse.Height)
            {
                this.Height = btnBrowse.Height;
            }
            txtFile.Width = this.Width - btnBrowse.Width - 3;
            btnBrowse.Left = txtFile.Width + 3;

            txtFile.Top = Convert.ToInt32(Convert.ToDouble(this.Height - txtFile.Height) / 2.0);
        }

        private void txtFile_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                // check if the drop data is actually a file or folder
                if (e != null && e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    // Get the Dropped Data
                    String[] s = (String[])e.Data.GetData(DataFormats.FileDrop, false);
                    // Check if we have valid Data and that the specified File Data actually exists
                    if (s != null && s.Length > 0 && File.Exists(s[0]))
                    {
                        e.Effect = DragDropEffects.All;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                ex.ShowException();
            }
        }

        private void txtFile_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                // check if the drop data is actually a file or folder
                if (e != null && e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    String[] s = (String[])e.Data.GetData(DataFormats.FileDrop, false);
                    // Check if we have valid Data and that the specified File Data actually exists
                    if (s != null && s.Length > 0 && File.Exists(s[0]))
                    {
                        txtFile.Text = s[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                ex.ShowException();
            }
        }
    }
}
