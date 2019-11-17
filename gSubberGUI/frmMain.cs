using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gSubber;
using gSubber.Formats;
using System.Diagnostics;
using System.IO;
using gSubber.Core;
using gSubber.Core.SubtitleFile;
using gSubber.Helpers;
using gSubberGUI.Controls;
using System.Reflection;

namespace gSubberGUI
{
    public partial class frmMain : BaseForm
    {
        private SubFileParserResults _results = null;
        private ISubFileParser _parser = null;
        private List<string> _cmdArguments = new List<string>();


        public frmMain()
        {
            InitializeComponent();

            Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);

            this.gFilePicker1.TextChanged += GFilePicker1_TextChanged;

            // Get the command line arguments
            GetCommandLineArguments();

            SetUpDataGridView();
        }

        private void GetCommandLineArguments()
        {
            // check if user provided with command line arguments when executing the application
            string[] cmdArgs = Environment.GetCommandLineArgs();
            if (cmdArgs.Length > 1)
            {
                // Copy the results to a list
                _cmdArguments = cmdArgs.ToList();
                // Remove the first argument (the executable)
                _cmdArguments.RemoveAt(0);
            }
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            try
            {
                // check if user provided with a filename when executing the application
                if (_cmdArguments.Any()
                    && _cmdArguments.Any(c => File.Exists(c)))
                {
                    // Get the file list
                    List<string> fileList = _cmdArguments.Where(c => File.Exists(c)).ToList();

                    // Check if any existing files were provided
                    if (!fileList.Any())
                    {
                        throw new Exception("No existing files were provided!");
                    }

                    // Open the first file provided
                    OpenFile(fileList.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                ex.ShowException();
            }
        }

        private void GFilePicker1_TextChanged(object sender, EventArgs e)
        {
            ClearData();
        }

        private void ClearData()
        {
            _parser = null;
            _results = null;

            SetUpDataGridView();
            grdSubtitles.DataSource = null;

            lstInfo.Items.Clear();
            lstProperties.Items.Clear();
            lstStyles.Items.Clear();
            lstAttachments.Items.Clear();
        }


        private void SetUpDataGridView()
        {
            grdSubtitles.SuspendLayout();

            grdSubtitles.AutoGenerateColumns = false;
            grdSubtitles.Columns.Clear();
            grdSubtitles.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "LineNumber",
                HeaderText = "#"
            });
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].Width = 45;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdSubtitles.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Zindex",
                HeaderText = "Z"
            });
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].Width = 25;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdSubtitles.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "StartTime",
                HeaderText = "Start Time"
            });
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].Width = 85;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdSubtitles.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "EndTime",
                HeaderText = "End Time"
            });
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].Width = 85;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdSubtitles.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Duration",
                HeaderText = "Duration",
            });
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].Width = 55;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].DefaultCellStyle.Format = "#,##0.000";
            grdSubtitles.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "DisplayText",
                HeaderText = "Text"
            });
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            grdSubtitles.ResumeLayout();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFile(gFilePicker1.Text);
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Save file...";
                if (_parser is SrtFileParser)
                {
                    sfd.DefaultExt = ".srt";
                    sfd.Filter = "SRT Files (*.srt)|*.srt";
                }
                else if (_parser is AssFileParser)
                {
                    sfd.DefaultExt = ".ass";
                    sfd.Filter = "ASS Files (*.ass)|*.ass";
                }
                sfd.AddExtension = true;

                sfd.InitialDirectory = Path.GetDirectoryName(gFilePicker1.Text);
                sfd.FileName = Path.GetFileName(gFilePicker1.Text);

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    _parser.Save(_results.SubFile, sfd.FileName, Encoding.UTF8);

                    ShowSuccessMessage(String.Format("The file '{0}' was saved!", sfd.FileName));
                }
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
        }

        private void OpenFile(string argFilename)
        {
            if (String.IsNullOrWhiteSpace(argFilename))
            {
                throw new Exception("No input file selected!");
            }

            if (!File.Exists(argFilename))
            {
                throw new Exception(String.Format($"The input file '{argFilename}' was not found!"));
            }

            if (!gFilePicker1.Text.ToLower().Equals(argFilename.ToLower()))
            {
                gFilePicker1.Text = argFilename;
            }

            ClearData();

            _parser = SubFileParserFactory.GetSubFileParser(argFilename);

            Encoding enc = FileHelper.GetEncoding(argFilename);
            if (enc == Encoding.ASCII) enc = System.Text.Encoding.Default;

            _results = _parser.Load(argFilename, enc);

            if (_parser is SrtFileParser)
            {
                while (_results.SubFile.Subtitles.GroupBy(x => x.StartTime, x => x.Text).Count() < _results.SubFile.Subtitles.Count)
                {
                    for (int i = 0; i < _results.SubFile.Subtitles.Count; i++)
                    {
                        if (i == 0) continue;
                        if (_results.SubFile.Subtitles[i - 1].StartTime.Equals(_results.SubFile.Subtitles[i].StartTime))
                        {
                            _results.SubFile.Subtitles[i - 1].Text = String.Format("{0}\r\n{1}", _results.SubFile.Subtitles[i - 1].Text, _results.SubFile.Subtitles[i].Text);
                            _results.SubFile.Subtitles[i].Text = "";
                        }
                    }
                    _results.SubFile.Subtitles.RemoveAll(x => String.IsNullOrWhiteSpace(x.Text));
                }
            }

            //bool test = results.SubFile.Subtitles.GroupBy(x => x.StartTime, x => x.Text).Count() < results.SubFile.Subtitles.Count;

            //foreach (var sub in results.SubFile.Subtitles)
            //{
            //    sub.Text = String.Join("\r\n", sub.TextLines.Reverse());
            //} 


            if (_results.Warnings.Any())
            {
                ShowWarningMessage(String.Join(Environment.NewLine, _results.Warnings));
            }

            if (_results.Errors.Any())
            {
                ShowErrorMessage(String.Join(Environment.NewLine, _results.Errors));
            }

            SetUpDataGridView();
            grdSubtitles.DataSource = _results.SubFile.Subtitles;

            _results.SubFile.Info.ForEach(i => lstInfo.Items.Add(i));

            _results.SubFile.Properties.ForEach(i => lstProperties.Items.Add(i));

            _results.SubFile.Styles.ForEach(i => lstStyles.Items.Add(i));

            _results.SubFile.Attachments.ForEach(i => lstAttachments.Items.Add(i));


            ShowSuccessMessage(String.Format("Success!{0}Subtitle lines:{1}", Environment.NewLine, _results.SubFile.Subtitles.Count));

            //SubFileParserResults r = p.Load(@"H:\AnimeClipse\FairyTail\Karaoke\Karaoke06 [061-072]\Karaoke_op6.ass", Encoding.UTF8);

            //SubFileParserResults sr = s.Load(@"M:\Videos_M\Movies\Assassins.Creed.2016.1080p.WEB-DL.H264.AC3-EVO_track3_eng.srt", Encoding.UTF8);
            //s.Save(r.SubFile, @"F:\Videos_Red_3\Series_Red_3\Scream\Season 2\Scream.S02E01.720p.WEB-DL.DD5.1.H.264-VietHD_track3_eng.new.srt");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ShowQuestion("Do you really want to exit?", "Exit?", false) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Select subtitle file...";
                ofd.Filter = "(*.srt) Subrip files|*.srt|(*.ass) ASS files|*.ass";
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    OpenFile(ofd.FileName);
                }
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
        }

        private void grdSubtitles_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdSubtitles.SelectedIndex == -1)
                {
                    txtSubtitleItem.Clear();
                }
                else
                {
                    var sub = grdSubtitles.SelectedItem as SubFileSubtitleItem;
                    txtSubtitleItem.Text = sub.Text;
                }
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
        }
    }
}
