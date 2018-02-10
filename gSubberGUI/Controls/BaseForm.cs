﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace gSubberGUI.Controls
{
    public class BaseForm : Form
    {
        public BaseForm() : base()
        {
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // gForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "gForm";
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Returns the full path and filename of the executing assembly
        /// </summary>
        /// <returns></returns>
        protected String GetExecutingAssemblyLocation()
        {
            return Assembly.GetExecutingAssembly().Location;
        }

        /// <summary>
        /// Returns the current directory of the executing assembly
        /// </summary>
        /// <returns></returns>
        protected String GetCurrentDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// Returns the version of the executing assembly
        /// </summary>
        /// <returns></returns>
        protected Version GetCurrentVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }

        protected void ShowErrorMessage(String argMessage)
        {
            MessageBox.Show("An error has occured!" + Environment.NewLine + Environment.NewLine + argMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected void ShowSuccessMessage(String argMessage)
        {
            MessageBox.Show(argMessage, "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected DialogResult ShowQuestion(String argQuestion, String argTitle, bool argShowCancel = true)
        {
            MessageBoxButtons msgBoxBtns = MessageBoxButtons.YesNoCancel;
            if (!argShowCancel)
            {
                msgBoxBtns = MessageBoxButtons.YesNo;
            }
            return MessageBox.Show(argQuestion, argTitle, msgBoxBtns, MessageBoxIcon.Question);
        }

        protected void ToggleControls(Control argRootControl, Boolean argStatus)
        {
            foreach (Control ctrl in argRootControl.Controls)
            {
                if (ctrl is IContainer)
                {
                    ToggleControls(ctrl, argStatus);
                }
                else
                {
                    ctrl.Enabled = argStatus;
                }
            }
        }

        #region "DPI Handling"

        public static short LOWORD(int number)
        {
            return (short)number;
        }

        protected const int WM_DPICHANGED = 0x02E0;
        protected const float DESIGN_TIME_DPI = 96F;

        protected float oldDpi;
        protected float currentDpi;

        protected bool isMoving = false;
        protected bool shouldScale = false;

        protected void InitDPI()
        {
            oldDpi = currentDpi;
            float dx;
            using (Graphics g = this.CreateGraphics())
            {
                dx = g.DpiX;
            }
            currentDpi = dx;

            HandleDpiChanged();
            OnDPIChanged();
        }

        protected override void OnResizeBegin(EventArgs e)
        {
            base.OnResizeBegin(e);

            this.isMoving = true;
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);

            this.isMoving = false;
            if (shouldScale)
            {
                shouldScale = false;
                HandleDpiChanged();
            }
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);

            if (this.shouldScale && CanPerformScaling())
            {
                this.shouldScale = false;
                HandleDpiChanged();
            }
        }

        protected bool CanPerformScaling()
        {
            return (Screen.FromControl(this).Bounds.Contains(this.Bounds));
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                // This message is sent when the form is dragged to a different monitor i.e. when
                // the bigger part of its are is on the new monitor. Note that handling the message immediately
                // might change the size of the form so that it no longer overlaps the new monitor in its bigger part
                // which in turn will send again the WM_DPICHANGED message and this might cause misbehavior.
                // Therefore we delay the scaling if the form is being moved and we use the CanPerformScaling method to 
                // check if it is safe to perform the scaling.
                case WM_DPICHANGED:
                    oldDpi = currentDpi;
                    currentDpi = LOWORD((int)m.WParam);

                    if (oldDpi != currentDpi)
                    {
                        if (this.isMoving)
                        {
                            shouldScale = true;
                        }
                        else
                        {
                            HandleDpiChanged();
                        }

                        OnDPIChanged();
                    }

                    break;
            }

            base.WndProc(ref m);
        }

        protected void HandleDpiChanged()
        {
            if (oldDpi != 0F)
            {
                float scaleFactor = currentDpi / oldDpi;

                // The default scaling method of the framework
                this.Scale(new SizeF(scaleFactor, scaleFactor));

                // Fonts are not scaled automatically so we need to handle this manually
                this.ScaleFonts(scaleFactor);

                // Perform any other scaling different than font or size (e.g. ItemHeight)
                this.PerformSpecialScaling(scaleFactor);
            }
            else
            {
                // The special scaling also needs to be done initially
                this.PerformSpecialScaling(currentDpi / DESIGN_TIME_DPI);
            }
        }

        protected virtual void ScaleFonts(float scaleFactor)
        {
            // Go through all controls in the control tree and set their Font property
            ScaleFontForControl(this, scaleFactor);
        }

        protected static void ScaleFontForControl(Control control, float scaleFactor)
        {
            control.Font = new Font(control.Font.FontFamily, control.Font.Size * scaleFactor, control.Font.Style);

            foreach (Control child in control.Controls)
            {
                ScaleFontForControl(child, scaleFactor);
            }
        }

        protected virtual void PerformSpecialScaling(float scaleFactor)
        {
        }

        protected virtual void OnDPIChanged()
        {
        }

        #endregion
    }
}
