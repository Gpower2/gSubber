using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gSubberGUI.Controls
{
    public class GLabel : Label
    {
        protected ContextMenuStrip _ContextMenu = new ContextMenuStrip();
        protected ToolStripMenuItem _CopyMenu = new ToolStripMenuItem("Copy");

        public GLabel()
            : base()
        {
            this.DoubleBuffered = true;
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            SetUpContextMenu();
        }

        protected void SetUpContextMenu()
        {
            // Set the ContextMenu Items
            _ContextMenu.Items.Clear();
            _ContextMenu.Items.Add(_CopyMenu);

            // Add the EventHandlers
            _CopyMenu.Click += (object sender, EventArgs e) =>
            {
                try
                {
                    if (!String.IsNullOrWhiteSpace(this.Text))
                    {
                        Clipboard.SetText(this.Text);
                    }
                }
                catch (Exception ex)
                {
                    ex.ShowException();
                }
            };

            // Set the ContextMenu to this control
            this.ContextMenuStrip = _ContextMenu;
        }
    }
}
