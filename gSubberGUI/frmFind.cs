using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gSubberGUI
{
    public partial class frmFind : gSubberGUI.Controls.BaseForm
    {
        private frmMain _frmMain;

        public frmFind(frmMain argFrmMain) : base()
        {
            InitializeComponent();

            this.TopMost = true;

            _frmMain = argFrmMain;
        }
    }
}
