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
        public frmMain()
        {
            InitializeComponent();

            this.gComboBox1.Items.Add("sdfgggggggggggggggggggggggggggggggg");
            this.gComboBox1.Items.Add("sfgsfg");
            this.gComboBox1.Items.Add("asdfhgdfgsd;lsadgmf;lasdfgn;lasdfgn;asdfgn;nalgag");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                Time t = new Time(1, 12, 35, 135);
                textBox1.AppendText(String.Format("Time: {0}\r\n", t));
                textBox1.AppendText(String.Format("Hours: {0}\r\n", t.Hours));
                textBox1.AppendText(String.Format("Minutes: {0}\r\n", t.Minutes));
                textBox1.AppendText(String.Format("Seconds: {0}\r\n", t.Seconds));
                textBox1.AppendText(String.Format("Milliseconds: {0}\r\n", t.Milliseconds));
                textBox1.AppendText(String.Format("Microseconds: {0}\r\n", t.Microseconds));
                textBox1.AppendText(String.Format("Nanoseconds: {0}\r\n", t.Nanoseconds));
                textBox1.AppendText(String.Format("Total Hours: {0}\r\n", t.TotalHours));
                textBox1.AppendText(String.Format("Total Minutes: {0}\r\n", t.TotalMinutes));
                textBox1.AppendText(String.Format("Total Seconds: {0}\r\n", t.TotalSeconds));
                textBox1.AppendText(String.Format("Total Milliseconds: {0}\r\n", t.TotalMilliseconds));
                textBox1.AppendText(String.Format("Total Microseconds: {0}\r\n", t.TotalMicroseconds));
                textBox1.AppendText(String.Format("Total Nanoseconds: {0}\r\n", t.TotalNanoseconds));
                textBox1.AppendText(String.Format("Time: {0}\r\n", t.ToString(Time.TimeFormat.WithSeconds)));
                textBox1.AppendText(String.Format("Time: {0}\r\n", t.ToString(Time.TimeFormat.WithMilliseconds)));
                textBox1.AppendText(String.Format("Time: {0}\r\n", t.ToString(Time.TimeFormat.WithMicroseconds)));
                textBox1.AppendText(String.Format("Time: {0}\r\n", t.ToString(Time.TimeFormat.WithNanoseconds)));

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

                ISubFileParser parser = null;
                if (inputFileExtension == "srt")
                {
                    parser = new SrtFileParser();
                }
                else if (inputFileExtension == "ass")
                {
                    parser = new AssFileParser();
                }
                if(parser == null)
                {
                    throw new Exception(String.Format("Could not file parser for format {0}!", inputFileExtension));
                }

                Encoding enc = FileHelper.GetEncoding(gFilePicker1.Text);
                if (enc == Encoding.ASCII) enc = System.Text.Encoding.Default;

                SubFileParserResults results = parser.Load(gFilePicker1.Text, enc);

                if (parser is SrtFileParser)
                {
                    while (results.SubFile.Subtitles.GroupBy(x => x.StartTime, x => x.Text).Count() < results.SubFile.Subtitles.Count)
                    {
                        for (int i = 0; i < results.SubFile.Subtitles.Count; i++)
                        {
                            if (i == 0) continue;
                            if (results.SubFile.Subtitles[i - 1].StartTime.Equals(results.SubFile.Subtitles[i].StartTime))
                            {
                                results.SubFile.Subtitles[i - 1].Text = String.Format("{0}\r\n{1}", results.SubFile.Subtitles[i - 1].Text, results.SubFile.Subtitles[i].Text);
                                results.SubFile.Subtitles[i].Text = "";
                            }
                        }
                        results.SubFile.Subtitles.RemoveAll(x => String.IsNullOrWhiteSpace(x.Text));
                    }
                }

                //bool test = results.SubFile.Subtitles.GroupBy(x => x.StartTime, x => x.Text).Count() < results.SubFile.Subtitles.Count;

                //foreach (var sub in results.SubFile.Subtitles)
                //{
                //    sub.Text = String.Join("\r\n", sub.TextLines.Reverse());
                //} 

                //parser.Save(results.SubFile, String.Format("{0}.{1}", TxtInputFile.Text, inputFileExtension), Encoding.UTF8);

                if (results.Warnings.Any())
                {
                    ShowWarningMessage(String.Join(Environment.NewLine, results.Warnings));
                }

                if (results.Errors.Any())
                {
                    ShowErrorMessage(String.Join(Environment.NewLine, results.Errors));
                }

                dataGridView1.DataSource = results.SubFile.Subtitles;

                ShowSuccessMessage(String.Format("Success!{0}Subtitle lines:{1}", Environment.NewLine, results.SubFile.Subtitles.Count));

                //SubFileParserResults r = p.Load(@"H:\AnimeClipse\FairyTail\Karaoke\Karaoke06 [061-072]\Karaoke_op6.ass", Encoding.UTF8);

                //SubFileParserResults sr = s.Load(@"M:\Videos_M\Movies\Assassins.Creed.2016.1080p.WEB-DL.H264.AC3-EVO_track3_eng.srt", Encoding.UTF8);
                //s.Save(r.SubFile, @"F:\Videos_Red_3\Series_Red_3\Scream\Season 2\Scream.S02E01.720p.WEB-DL.DD5.1.H.264-VietHD_track3_eng.new.srt");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtInputFile_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                // check if the drop data is actually a file or folder
                if (e != null && e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    String[] s = (String[])e.Data.GetData(DataFormats.FileDrop, false);
                    if (s != null && s.Length > 0)
                    {
                        TxtInputFile.Text = s[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtInputFile_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (e != null && e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    // check if it is a file or not
                    String[] s = (String[])e.Data.GetData(DataFormats.FileDrop);
                    if (s != null && s.Length > 0 && File.Exists(s[0]))
                    {
                        e.Effect = DragDropEffects.All;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
