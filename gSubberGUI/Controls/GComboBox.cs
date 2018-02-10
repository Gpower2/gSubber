using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gSubberGUI.Controls
{
    public class GComboBox : ComboBox
    {
        protected ContextMenuStrip _ContextMenu = new ContextMenuStrip();
        protected ToolStripMenuItem _ClearMenu = new ToolStripMenuItem("Clear");
        protected ToolStripMenuItem _CopyMenu = new ToolStripMenuItem("Copy");

        public GComboBox()
        {
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            InitializeComponent();

            SetUpContextMenu();
        }

        protected void SetUpContextMenu()
        {
            // Set the ContextMenu Items
            _ContextMenu.Items.Clear();
            _ContextMenu.Items.Add(_CopyMenu);
            _ContextMenu.Items.Add(_ClearMenu);

            // Add the EventHandlers
            _CopyMenu.Click += (object sender, EventArgs e) => 
            {
                try
                {
                    if (this.SelectedIndex > -1)
                    {
                        Clipboard.SetText(this.Text);
                    }
                }
                catch (Exception ex)
                {
                    ex.ShowException();
                }
            };

            _ClearMenu.Click += (object sender, EventArgs e) =>
            {
                try
                {
                    this.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    ex.ShowException();
                }
            };

            // Set the ContextMenu to this control
            this.ContextMenuStrip = _ContextMenu;
        }

        protected void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // gComboBox
            // 
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Size = new System.Drawing.Size(121, 21);
            this.ResumeLayout(false);
        }

        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);

            try
            {
                AutosizeDropDownWidth();
            }
            catch (Exception ex)
            {
                ex.ShowException();
            }
        }

        protected void AutosizeDropDownWidth()
        {
            float longestItem = 0f;

            // Find the longest text from the items list, in order to define the width
            using (Graphics g = Graphics.FromHwnd(this.Handle))
            {
                foreach (var item in Items)
                {
                    float itemWidth = g.MeasureString(GetItemText(item), Font).Width;
                    if (itemWidth > longestItem)
                    {
                        longestItem = itemWidth;
                    }
                }
            }

            // If there is a ScrollBar, then increase the width by the VerticalScrollBarWidth
            if (Items.Count > MaxDropDownItems)
            {
                longestItem += SystemInformation.VerticalScrollBarWidth;
            }

            // Change the width of the items list, byt never make it smaller than the width of the control
            DropDownWidth = Convert.ToInt32(Math.Max(Math.Ceiling(longestItem), Width));
        }
    }
}
