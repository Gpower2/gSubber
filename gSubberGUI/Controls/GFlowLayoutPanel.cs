﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gSubberGUI.Controls
{
    public class GFlowLayoutPanel : FlowLayoutPanel
    {
        public GFlowLayoutPanel()
            : base()
        {
            this.DoubleBuffered = true;
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
    }
}
