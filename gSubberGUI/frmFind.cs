using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace gSubberGUI
{
    public partial class frmFind : gSubberGUI.Controls.BaseForm
    {
        private class FindSettings
        {
            public Queue<string> LastSearches { get; set; }


            public bool MatchCase { get; set; }

            public bool MatchWholeWord { get; set; }

            public bool WrapAround { get; set; }


            public bool NormalMode { get; set; }

            public bool ExtendedMode { get; set; }

            public bool RegularExpressionMode { get; set; }


            public bool UseTransparency { get; set; }

            public bool UseTransparencyAlways { get; set; }

            public bool UseTransparencyOnLostFocus { get; set; }

            public int TransparencyValue { get; set; }
        }

        private frmMain _frmMain;

        private bool _IgnoreEvents = false;


        public frmFind(frmMain argFrmMain) : base()
        {
            InitializeComponent();

            Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);

            this.TopMost = true;

            _frmMain = argFrmMain;

            FindSettings settings = LoadSettings();

            _IgnoreEvents = true;
            SetSettings(settings);
            _IgnoreEvents = false;

            SetTransparency();
        }

        private void frmFind_Activated(object sender, EventArgs e)
        {
            if (_IgnoreEvents) return;

            SetTransparency();
        }

        private void frmFind_Deactivate(object sender, EventArgs e)
        {
            if (_IgnoreEvents) return;

            try
            {
                if (chkUseTransparency.Checked && rbtnTransparencyOnLostFocus.Checked)
                {
                    this.Opacity = trkTransparency.Value / 100.0;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void SetTransparency()
        {
            if (!chkUseTransparency.Checked)
            {
                this.Opacity = 1.0;
            }
            else if (chkUseTransparency.Checked && rbtnTransaparencyAlways.Checked)
            {
                this.Opacity = trkTransparency.Value / 100.0;
            }
            else if (chkUseTransparency.Checked && rbtnTransparencyOnLostFocus.Checked)
            {
                this.Opacity = 1.0;
            }
        }

        private FindSettings LoadSettings()
        {
            if (!File.Exists("findSettings.json"))
            {
                SaveSettings();
            }
            using (StreamReader sr = new StreamReader("findSettings.json"))
            {
                return JsonConvert.DeserializeObject<FindSettings>(sr.ReadToEnd());
            }
        }

        private void SaveSettings()
        {
            FindSettings settings = new FindSettings()
            {
                MatchCase = chkMatchCase.Checked,
                MatchWholeWord = chkWholeWord.Checked,
                WrapAround = chkWrapAround.Checked,

                NormalMode = rbtnNormal.Checked,
                ExtendedMode = rbtnExtended.Checked,
                RegularExpressionMode = rbtnRegularExpression.Checked,

                UseTransparency = chkUseTransparency.Checked,
                UseTransparencyAlways = rbtnTransaparencyAlways.Checked,
                UseTransparencyOnLostFocus = rbtnTransparencyOnLostFocus.Checked,
                TransparencyValue = trkTransparency.Value
            };

            using (StreamWriter sw = new StreamWriter("findSettings.json"))
            {
                sw.Write(JsonConvert.SerializeObject(settings));
            }
        }

        private void SetSettings(FindSettings argSettings)
        {
            chkMatchCase.Checked = argSettings.MatchCase;
            chkWholeWord.Checked = argSettings.MatchWholeWord;
            chkWrapAround.Checked = argSettings.WrapAround;

            rbtnNormal.Checked = argSettings.NormalMode;
            rbtnExtended.Checked = argSettings.ExtendedMode;
            rbtnRegularExpression.Checked = argSettings.RegularExpressionMode;

            chkUseTransparency.Checked = argSettings.UseTransparency;
            rbtnTransaparencyAlways.Checked = argSettings.UseTransparencyAlways;
            rbtnTransparencyOnLostFocus.Checked = argSettings.UseTransparencyOnLostFocus;
            trkTransparency.Value = argSettings.TransparencyValue;
        }

        private void chkUseTransparency_CheckedChanged(object sender, EventArgs e)
        {
            if (_IgnoreEvents) return;

            SaveSettings();
            SetTransparency();
        }

        private void rbtnTransparency_CheckedChanged(object sender, EventArgs e)
        {
            if (_IgnoreEvents) return;

            SaveSettings();
            SetTransparency();
        }

        private void trkTransparency_ValueChanged(object sender, EventArgs e)
        {
            if (_IgnoreEvents) return;

            SaveSettings();
            SetTransparency();
        }
    }
}
