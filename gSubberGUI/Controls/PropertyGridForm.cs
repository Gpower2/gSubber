using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubberGUI.Controls
{
    public class PropertyGridForm : BaseForm
    {

        #region " Windows Form Designer generated code "

        public PropertyGridForm()
            : base()
        {
            InitializeComponent();
        }

        //Form overrides dispose to clean up the component list.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if ((components != null))
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private System.ComponentModel.IContainer components = null;

        public System.Windows.Forms.PropertyGrid PropertyGrid;

        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // PropertyGrid
            // 
            this.PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.PropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.PropertyGrid.Name = "PropertyGrid";
            this.PropertyGrid.Size = new System.Drawing.Size(484, 561);
            this.PropertyGrid.TabIndex = 0;
            // 
            // PropertyGridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(484, 561);
            this.Controls.Add(this.PropertyGrid);
            this.Name = "PropertyGridForm";
            this.Text = "Ιδιότητες";
            this.ResumeLayout(false);

        }

        #endregion
    }
}
