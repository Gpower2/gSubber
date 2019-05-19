using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gSubber;
using gSubber.Formats.Ass;
using System.Diagnostics;
using gSubber.Formats.Srt;
using System.IO;
using gSubber.Core;
using gSubber.Core.SubtitleFile;
using gSubber.Helpers;
using gSubberGUI.Controls;

namespace gSubberGUI
{
    public partial class frmMain : BaseForm
    {
        private SubFileParserResults _results = null;
        private ISubFileParser _parser = null;

        public frmMain()
        {
            InitializeComponent();

            this.gComboBox1.Items.Add("sdfgggggggggggggggggggggggggggggggg");
            this.gComboBox1.Items.Add("sfgsfg");
            this.gComboBox1.Items.Add("asdfhgdfgsd;lsadgmf;lasdfgn;lasdfgn;asdfgn;nalgag");

            this.gFilePicker1.TextChanged += GFilePicker1_TextChanged;

            SetUpDataGridView();
        }

        private void GFilePicker1_TextChanged(object sender, EventArgs e)
        {
            _parser = null;
            _results = null;

            SetUpDataGridView();
            gDataGridView1.DataSource = null;
        }

        private void SetUpDataGridView()
        {
            gDataGridView1.SuspendLayout();

            gDataGridView1.AutoGenerateColumns = false;
            gDataGridView1.Columns.Clear();
            gDataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "LineNumber",
                HeaderText = "#"
            });
            gDataGridView1.Columns[gDataGridView1.Columns.Count - 1].Width = 45;
            gDataGridView1.Columns[gDataGridView1.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gDataGridView1.Columns[gDataGridView1.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gDataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Zindex",
                HeaderText = "Z"
            });
            gDataGridView1.Columns[gDataGridView1.Columns.Count - 1].Width = 25;
            gDataGridView1.Columns[gDataGridView1.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gDataGridView1.Columns[gDataGridView1.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gDataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "StartTime",
                HeaderText = "Start Time"
            });
            gDataGridView1.Columns[gDataGridView1.Columns.Count - 1].Width = 85;
            gDataGridView1.Columns[gDataGridView1.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gDataGridView1.Columns[gDataGridView1.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gDataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "EndTime",
                HeaderText = "End Time"
            });
            gDataGridView1.Columns[gDataGridView1.Columns.Count - 1].Width = 85;
            gDataGridView1.Columns[gDataGridView1.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gDataGridView1.Columns[gDataGridView1.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gDataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Duration",
                HeaderText = "Duration",
            });
            gDataGridView1.Columns[gDataGridView1.Columns.Count - 1].Width = 55;
            gDataGridView1.Columns[gDataGridView1.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gDataGridView1.Columns[gDataGridView1.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            gDataGridView1.Columns[gDataGridView1.Columns.Count - 1].DefaultCellStyle.Format = "#,##0.000";
            gDataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "DisplayText",
                HeaderText = "Text"
            });
            gDataGridView1.Columns[gDataGridView1.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gDataGridView1.Columns[gDataGridView1.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gDataGridView1.Columns[gDataGridView1.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            gDataGridView1.ResumeLayout();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(gFilePicker1.Text))
                {
                    throw new Exception("No input file selected!");
                }

                if (!File.Exists(gFilePicker1.Text))
                {
                    throw new Exception(String.Format("The input file '{0}' was not found!", gFilePicker1.Text));
                }

                String inputFileExtension = Path.GetExtension(gFilePicker1.Text);
                if(inputFileExtension.Length > 1)
                {
                    inputFileExtension = inputFileExtension.Substring(1);
                }
                inputFileExtension = inputFileExtension.ToLower().Trim();

                _parser = null;
                if (inputFileExtension == "srt")
                {
                    _parser = new SrtFileParser();
                }
                else if (inputFileExtension == "ass")
                {
                    _parser = new AssFileParser();
                }
                if(_parser == null)
                {
                    throw new Exception(String.Format("Could not file parser for format {0}!", inputFileExtension));
                }

                Encoding enc = FileHelper.GetEncoding(gFilePicker1.Text);
                if (enc == Encoding.ASCII) enc = System.Text.Encoding.Default;

                _results = _parser.Load(gFilePicker1.Text, enc);

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
                gDataGridView1.DataSource = _results.SubFile.Subtitles;


                ShowSuccessMessage(String.Format("Success!{0}Subtitle lines:{1}", Environment.NewLine, _results.SubFile.Subtitles.Count));

                //SubFileParserResults r = p.Load(@"H:\AnimeClipse\FairyTail\Karaoke\Karaoke06 [061-072]\Karaoke_op6.ass", Encoding.UTF8);

                //SubFileParserResults sr = s.Load(@"M:\Videos_M\Movies\Assassins.Creed.2016.1080p.WEB-DL.H264.AC3-EVO_track3_eng.srt", Encoding.UTF8);
                //s.Save(r.SubFile, @"F:\Videos_Red_3\Series_Red_3\Scream\Season 2\Scream.S02E01.720p.WEB-DL.DD5.1.H.264-VietHD_track3_eng.new.srt");
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
    }
}
