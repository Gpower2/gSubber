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
using gSubber.Extensions;
using gSubberGUI.Controls;
using System.Reflection;

namespace gSubberGUI
{
    public partial class frmMain : BaseForm
    {
        private ISubFileParserResults _results = null;
        private ISubFileParser _parser = null;
        private List<string> _cmdArguments = new List<string>();

        private frmFind _frmFind;
        private frmFind _frmReplace;
        private ContextMenuStrip _contextMenu;

        private bool _ignoreSelectionChangedEvent = false;

        public GDataGridView SubtitleGridView { get { return grdSubtitles; } }
        public GTextBox SubtitleItemTextBox { get { return txtSubtitleItem; } }

        public frmMain()
        {
            InitializeComponent();

            Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);

            this.gFilePicker1.TextChanged += GFilePicker1_TextChanged;

            // Get the command line arguments
            GetCommandLineArguments();

            // Ignore DataErrors from DataGridView
            grdSubtitles.DataError += (object s, DataGridViewDataErrorEventArgs dataGridViewDataErrorEventArgs) => { return; };

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

        private void SetData(object argData)
        {
            grdSubtitles.SuspendLayout();

            // Get the first displayed row index
            int prevDisplayedRowIndex = grdSubtitles.FirstDisplayedScrollingRowIndex;
            // Get the first displayed column index
            int prevDisplayedColumnIndex = grdSubtitles.FirstDisplayedScrollingColumnIndex;
            // Get the selected row index
            int prevSelectedRowIndex = grdSubtitles.SelectedIndex;

            // Clear the DataSource 
            grdSubtitles.DataSource = null;

            // Set the new DataSource
            if (argData != null)
            {
                grdSubtitles.DataSource = argData;
            }

            // Check if we had a valid previous first displayed row index
            if (prevDisplayedRowIndex > -1)
            {
                // Check if the previous first displayed row index is now valid
                if (prevDisplayedRowIndex > grdSubtitles.Rows.Count - 1)
                {
                    prevDisplayedRowIndex = grdSubtitles.Rows.Count - 1;
                }

                // Set the previous displayed row index
                grdSubtitles.FirstDisplayedScrollingRowIndex = prevDisplayedRowIndex;
            }

            // Check if we had a valid previous first displayed column index
            if (prevDisplayedColumnIndex > -1)
            {
                // Check if the previous first displayed column index is now valid
                if (prevDisplayedColumnIndex > grdSubtitles.Columns.Count - 1)
                {
                    prevDisplayedColumnIndex = grdSubtitles.Columns.Count - 1;
                }

                // Set the previous displayed row index
                grdSubtitles.FirstDisplayedScrollingColumnIndex = prevDisplayedColumnIndex;
            }

            // Check if we had a valid previous selected row index
            if (prevSelectedRowIndex > -1)
            {
                // Check if the previous selected row index is now valid
                if (prevSelectedRowIndex > grdSubtitles.Rows.Count - 1)
                {
                    prevSelectedRowIndex = grdSubtitles.Rows.Count - 1;
                }

                // Set the previous selected row index
                grdSubtitles.SetSelectedRowByIndex(prevSelectedRowIndex);
            }

            grdSubtitles.ResumeLayout();
        }

        private void SetUpDataGridView()
        {
            grdSubtitles.SuspendLayout();

            grdSubtitles.MultiSelect = true;

            grdSubtitles.AutoGenerateColumns = false;
            grdSubtitles.Columns.Clear();
            grdSubtitles.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "LineNumber",
                HeaderText = "#",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].Width = 45;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grdSubtitles.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Zindex",
                HeaderText = "Z",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].Width = 25;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grdSubtitles.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "StartTime",
                HeaderText = "Start Time",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].Width = 85;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grdSubtitles.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "EndTime",
                HeaderText = "End Time",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].Width = 85;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grdSubtitles.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Duration",
                HeaderText = "Duration",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].Width = 55;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].DefaultCellStyle.Format = "#,##0.000";

            grdSubtitles.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "DisplayText",
                HeaderText = "Text",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdSubtitles.Columns[grdSubtitles.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            grdSubtitles.ResumeLayout();

            BuildContextMenu();
        }

        private void BuildContextMenu()
        {
            // Check if the context menu is initialized
            if (_contextMenu == null)
            {
                // Create the context menu
                _contextMenu = new ContextMenuStrip();
            }

            // Check if the context menu has any items
            if (_contextMenu.Items.Count == 0)
            {
                // Add the context menu items

                // Insert Before ====================
                _contextMenu.Items.Add(
                    new ToolStripMenuItem(
                        "Insert subtitle line (before last clicked line)",
                        null,
                        (object sender, EventArgs e) =>
                        {
                            if (_results == null || _results.SubFile == null || grdSubtitles.LastClickedRowIndex < 0)
                            {
                                return;
                            }

                            // Get the last clicked subtitle item
                            var subItem = grdSubtitles.Rows[grdSubtitles.LastClickedRowIndex].DataBoundItem as SubFileSubtitleItem;

                            // Get the index of the subtitle item
                            var idx = _results.SubFile.Subtitles.IndexOf(subItem);

                            // Insert a new subtitle item with the same times
                            _results.SubFile.Subtitles.Insert(idx,
                                new SubFileSubtitleItem()
                                {
                                    StartTime = subItem.StartTime
                                    ,
                                    EndTime = subItem.EndTime
                                }
                            );

                            SetData(_results.SubFile.Subtitles);
                        }
                    )
                );

                // Insert After ====================
                _contextMenu.Items.Add(
                    new ToolStripMenuItem(
                        "Insert subtitle line (after last clicked line)",
                        null,
                        (object sender, EventArgs e) =>
                        {
                            if (_results == null || _results.SubFile == null || grdSubtitles.LastClickedRowIndex < 0)
                            {
                                return;
                            }

                            // Get the last clicked subtitle item
                            var subItem = grdSubtitles.Rows[grdSubtitles.LastClickedRowIndex].DataBoundItem as SubFileSubtitleItem;

                            // Get the index of the subtitle item
                            var idx = _results.SubFile.Subtitles.IndexOf(subItem);

                            // Insert a new subtitle item after the selected one with the same times
                            _results.SubFile.Subtitles.Insert(idx + 1,
                                new SubFileSubtitleItem()
                                {
                                    StartTime = subItem.StartTime
                                    ,
                                    EndTime = subItem.EndTime
                                }
                            );

                            SetData(_results.SubFile.Subtitles);
                        }
                    )
                );


                // Separator -------------------------
                _contextMenu.Items.Add(new ToolStripSeparator());


                // Duplicate line ===================
                _contextMenu.Items.Add(
                    new ToolStripMenuItem(
                        "Duplicate last clicked subtitle line",
                        null,
                        (object sender, EventArgs e) =>
                        {
                            if (_results == null || _results.SubFile == null || grdSubtitles.LastClickedRowIndex < 0)
                            {
                                return;
                            }

                            // Get the last clicked subtitle item
                            var subItem = grdSubtitles.Rows[grdSubtitles.LastClickedRowIndex].DataBoundItem as SubFileSubtitleItem;

                            // Get the index of the subtitle item
                            var idx = _results.SubFile.Subtitles.IndexOf(subItem);

                            // Insert a new subtitle item with the same times
                            _results.SubFile.Subtitles.Insert(idx,
                                subItem.ShallowClone()
                            );

                            SetData(_results.SubFile.Subtitles);
                        }
                    )
                );


                // Delete last clicked line ===================
                _contextMenu.Items.Add(
                    new ToolStripMenuItem(
                        "Delete last clicked subtitle line",
                        null,
                        (object sender, EventArgs e) =>
                        {
                            if (_results == null || _results.SubFile == null || grdSubtitles.LastClickedRowIndex < 0)
                            {
                                return;
                            }

                            // Get the last clicked subtitle item
                            var subItem = grdSubtitles.Rows[grdSubtitles.LastClickedRowIndex].DataBoundItem as SubFileSubtitleItem;

                            // Get the index of the subtitle item
                            var idx = _results.SubFile.Subtitles.IndexOf(subItem);

                            // Delete the subtitle line
                            _results.SubFile.Subtitles.RemoveAt(idx);

                            SetData(_results.SubFile.Subtitles);
                        }
                    )
                );

                // Delete selected lines ===================
                _contextMenu.Items.Add(
                    new ToolStripMenuItem(
                        "Delete selected subtitle line(s)",
                        null,
                        (object sender, EventArgs e) =>
                        {
                            if (_results == null || _results.SubFile == null || grdSubtitles.SelectedRows.Count == 0)
                            {
                                return;
                            }

                            grdSubtitles.SuspendLayout();
                            _ignoreSelectionChangedEvent = true;

                            // Create a copy of the selected rows
                            var items = grdSubtitles.SelectedRows.Cast<DataGridViewRow>()
                                .OrderBy(r => r.Index)
                                .Select(r => r.DataBoundItem).ToList();

                            // Remove the lines from the data file
                            foreach (var item in items)
                            {
                                // Get the index of the subtitle item
                                var idx = _results.SubFile.Subtitles.IndexOf(item as SubFileSubtitleItem);

                                // Delete the subtitle line
                                _results.SubFile.Subtitles.RemoveAt(idx);
                            }
                            
                            // Clear DataGridView selection
                            grdSubtitles.ClearSelection();

                            _ignoreSelectionChangedEvent = false;
                            grdSubtitles.ResumeLayout();

                            // Reload the data to the DataGridView
                            SetData(_results.SubFile.Subtitles);
                        }
                    )
                );

                // Separator -------------------------
                _contextMenu.Items.Add(new ToolStripSeparator());


                // Copy subtitle text ====================
                _contextMenu.Items.Add(
                    new ToolStripMenuItem(
                        "Copy last clicked subtitle text",
                        null,
                        (object sender, EventArgs e) =>
                        {
                            if (_results == null || _results.SubFile == null || grdSubtitles.LastClickedRowIndex < 0)
                            {
                                return;
                            }

                            // Get the last clicked subtitle item
                            var subItem = grdSubtitles.Rows[grdSubtitles.LastClickedRowIndex].DataBoundItem as SubFileSubtitleItem;

                            // Copy the subtitle text
                            Clipboard.SetText(subItem.Text);
                        }
                    )
                );

                // Copy subtitle item ====================
                _contextMenu.Items.Add(
                    new ToolStripMenuItem(
                        "Copy last clicked subtitle line",
                        null,
                        (object sender, EventArgs e) =>
                        {
                            if (_results == null || _results.SubFile == null || grdSubtitles.LastClickedRowIndex < 0)
                            {
                                return;
                            }

                            // Get the last clicked subtitle item
                            var subItem = grdSubtitles.Rows[grdSubtitles.LastClickedRowIndex].DataBoundItem as SubFileSubtitleItem;

                            // Copy the subtitle text
                            Clipboard.SetDataObject(subItem);
                        }
                    )
                );

                // Separator -------------------------
                _contextMenu.Items.Add(new ToolStripSeparator());


                // Merge with line before ====================
                _contextMenu.Items.Add(
                    new ToolStripMenuItem(
                        "Merge last clicked subtitle line with previous line",
                        null,
                        (object sender, EventArgs e) =>
                        {
                            if (_results == null || _results.SubFile == null || grdSubtitles.LastClickedRowIndex <= 0)
                            {
                                return;
                            }

                            // Get the last clicked subtitle item
                            var subItem = grdSubtitles.Rows[grdSubtitles.LastClickedRowIndex].DataBoundItem as SubFileSubtitleItem;

                            // Get the previous last clicked subtitle item
                            var subItemPrevious = grdSubtitles.Rows[grdSubtitles.LastClickedRowIndex - 1].DataBoundItem as SubFileSubtitleItem;

                            // Check the times
                            if (subItemPrevious.StartTime > subItem.StartTime)
                            {
                                if (ShowQuestion($"The previous line's start time ({subItemPrevious.StartTime}) is after the line's start time ({subItem.StartTime}) to merge! Do you want to continue with the merge?", "Warning!", false) == DialogResult.No)
                                {
                                    return;
                                }
                            }

                            // Append the text to the previous line
                            subItemPrevious.Text += subItem.Text;
                            // Update the end time of the previous line
                            subItemPrevious.EndTime = subItem.EndTime;

                            // Get the index of the subtitle item
                            var idx = _results.SubFile.Subtitles.IndexOf(subItem);

                            // Delete the subtitle line
                            _results.SubFile.Subtitles.RemoveAt(idx);

                            SetData(_results.SubFile.Subtitles);
                        }
                    )
                );

                // Merge with line after ====================
                _contextMenu.Items.Add(
                    new ToolStripMenuItem(
                        "Merge last clicked subtitle line with next line",
                        null,
                        (object sender, EventArgs e) =>
                        {
                            if (_results == null || _results.SubFile == null || grdSubtitles.LastClickedRowIndex < 0 || grdSubtitles.LastClickedRowIndex == grdSubtitles.Rows.Count - 1)
                            {
                                return;
                            }

                            // Get the last clicked subtitle item
                            var subItem = grdSubtitles.Rows[grdSubtitles.LastClickedRowIndex].DataBoundItem as SubFileSubtitleItem;

                            // Get the next last clicked subtitle item
                            var subItemNext = grdSubtitles.Rows[grdSubtitles.LastClickedRowIndex + 1].DataBoundItem as SubFileSubtitleItem;

                            // Check the times
                            if (subItemNext.StartTime < subItem.StartTime)
                            {
                                if (ShowQuestion($"The next line's start time ({subItemNext.StartTime}) is before the line's start time ({subItem.StartTime}) to merge! Do you want to continue with the merge?", "Warning!", false) == DialogResult.No)
                                {
                                    return;
                                }
                            }

                            // Insert the text to the next line
                            subItemNext.Text = subItem.Text + subItemNext.Text;
                            // Update the start time of the next line
                            subItemNext.StartTime = subItem.StartTime;

                            // Get the index of the subtitle item
                            var idx = _results.SubFile.Subtitles.IndexOf(subItem);

                            // Delete the subtitle line
                            _results.SubFile.Subtitles.RemoveAt(idx);

                            SetData(_results.SubFile.Subtitles);
                        }
                    )
                );

            }

            grdSubtitles.ContextMenuStrip = _contextMenu;
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
                _parser.Save(_results.SubFile);

                ShowSuccessMessage(String.Format("The file '{0}' was saved!", _results.SubFile.Filename));
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

            Encoding enc = argFilename.GetEncoding();
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

                    while(_results.SubFile.Subtitles.Any(x => string.IsNullOrWhiteSpace(x.Text)))
                    {
                        _results.SubFile.Subtitles.Remove(_results.SubFile.Subtitles.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Text)));
                    }
                    //_results.SubFile.Subtitles.RemoveAll(x => String.IsNullOrWhiteSpace(x.Text));
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
            SetData(_results.SubFile.Subtitles);

            foreach (var item in _results.SubFile.Info)
            {
                lstInfo.Items.Add(item);
            }
            //_results.SubFile.Info.ForEach(i => lstInfo.Items.Add(i));

            foreach (var item in _results.SubFile.Properties)
            {
                lstProperties.Items.Add(item);
            }

            foreach (var item in _results.SubFile.Styles)
            {
                lstStyles.Items.Add(item);
            }

            foreach (var item in _results.SubFile.Attachments)
            {
                lstAttachments.Items.Add(item);
            }


            ShowSuccessMessage(String.Format("Success!{0}Subtitle lines:{1}", Environment.NewLine, _results.SubFile.Subtitles.Count));
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
                if (ofd.ShowDialog() == DialogResult.OK)
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
            if (_ignoreSelectionChangedEvent)
            {
                return;
            }

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

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Save file...";
                if (_parser is SrtFileParser)
                {
                    sfd.DefaultExt = ".srt";
                    sfd.Filter = "SRT Files (*.srt)|*.srt|ASS Files (*.ass)|*.ass";
                }
                else if (_parser is AssFileParser)
                {
                    sfd.DefaultExt = ".ass";
                    sfd.Filter = "ASS Files (*.ass)|*.ass|SRT Files (*.srt)|*.srt";
                }
                sfd.AddExtension = true;

                sfd.InitialDirectory = Path.GetDirectoryName(gFilePicker1.Text);
                sfd.FileName = Path.GetFileName(gFilePicker1.Text);

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ISubFileParser saveParser = _parser;
                    if (Path.GetExtension(sfd.FileName).Substring(1).ToLower().Equals("srt"))
                    {
                        if (!(_parser is SrtFileParser))
                        {
                            saveParser = new SrtFileParser();
                        }
                    }
                    else if (Path.GetExtension(sfd.FileName).Substring(1).ToLower().Equals("ass"))
                    {
                        if (!(_parser is AssFileParser))
                        {
                            saveParser = new AssFileParser();
                        }
                    }

                    saveParser.SaveAs(_results.SubFile, sfd.FileName, Encoding.UTF8);

                    ShowSuccessMessage(String.Format("The file '{0}' was saved!", sfd.FileName));
                }
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _parser.Save(_results.SubFile);

                ShowSuccessMessage(String.Format("The file '{0}' was saved!", _results.SubFile.Filename));
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_frmFind == null || _frmFind.IsDisposed)
                {
                    _frmFind = new frmFind(this, false);
                }
                _frmFind.Show();
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_frmReplace == null || _frmReplace.IsDisposed)
                {
                    _frmReplace = new frmFind(this, true);
                }
                _frmReplace.Show();
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
        }

        private void beforeCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_results == null || _results.SubFile == null || grdSubtitles.SelectedItem == null)
                {
                    return;
                }

                // Get the selected subtitle item
                var subItem = grdSubtitles.SelectedItem as SubFileSubtitleItem;

                // Get the index of the subtitle item
                var idx = _results.SubFile.Subtitles.IndexOf(subItem);

                // Insert a new subtitle item with the same times
                _results.SubFile.Subtitles.Insert(idx,
                    new SubFileSubtitleItem()
                    {
                        StartTime = subItem.StartTime
                        ,
                        EndTime = subItem.EndTime
                    }
                );

                SetData(_results.SubFile.Subtitles);
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
        }

        private void afterCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_results == null || _results.SubFile == null || grdSubtitles.SelectedItem == null)
                {
                    return;
                }

                // Get the selected subtitle item
                var subItem = grdSubtitles.SelectedItem as SubFileSubtitleItem;

                // Get the index of the subtitle item
                var idx = _results.SubFile.Subtitles.IndexOf(subItem);

                // Insert a new subtitle item after the selected one with the same times
                _results.SubFile.Subtitles.Insert(idx + 1,
                    new SubFileSubtitleItem()
                    {
                        StartTime = subItem.StartTime
                        ,
                        EndTime = subItem.EndTime
                    }
                );

                SetData(_results.SubFile.Subtitles);
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
        }

        private void duplicateSubtitleLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_results == null || _results.SubFile == null || grdSubtitles.SelectedItem == null)
                {
                    return;
                }

                // Get the selected subtitle item
                var subItem = grdSubtitles.SelectedItem as SubFileSubtitleItem;

                // Get the index of the subtitle item
                var idx = _results.SubFile.Subtitles.IndexOf(subItem);

                // Insert a new subtitle item with the same times
                _results.SubFile.Subtitles.Insert(idx,
                    subItem.ShallowClone()
                );

                SetData(_results.SubFile.Subtitles);
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
        }

        private void deleteSubtitleLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_results == null || _results.SubFile == null || grdSubtitles.SelectedRows.Count == 0)
                {
                    return;
                }

                while (grdSubtitles.SelectedRows.Count > 0)
                {
                    // Get the selected subtitle item
                    var subItem = grdSubtitles.SelectedRows[0].DataBoundItem as SubFileSubtitleItem;

                    // Remove selection
                    grdSubtitles.SelectedRows[0].Selected = false;

                    // Get the index of the subtitle item
                    var idx = _results.SubFile.Subtitles.IndexOf(subItem);

                    // Delete the subtitle line
                    _results.SubFile.Subtitles.RemoveAt(idx);
                }

                SetData(_results.SubFile.Subtitles);
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
        }

        private void adjustDurationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_results == null || _results.SubFile == null)
                {
                    return;
                }

                Time oldMinStartTime = _results.SubFile.Subtitles.Min(s => s.StartTime);
                Time oldMaxStartTime = _results.SubFile.Subtitles.Max(s => s.StartTime);

                Time newMinStartTime = new Time(0, 0, 7, 541);
                Time newMaxStartTime = new Time(0, 56, 57, 596);

                double newDiff = (newMaxStartTime - newMinStartTime).TotalNanoseconds;
                double oldDiff = (oldMaxStartTime - oldMinStartTime).TotalNanoseconds;
                double newMaxNanoseconds = newMaxStartTime.TotalNanoseconds;
                double oldMaxNanoseconds = oldMaxStartTime.TotalNanoseconds;

                for (int i = 0; i < _results.SubFile.Subtitles.Count; i++)
                {
                    // X = scores_new_max - ((scores_new_max - scores_new_min) * (scores_old_max - X) / (scores_old_max - scores_old_min))   
                    // X = newMaxStartTime - (( (newMaxStartTime - newMinStartTime) * (oldMaxStartTime - X)) / (oldMaxStartTime - oldMinStartTime))   
                    // X = newMaxStartTime - (( (newMaxStartTime - newMinStartTime) * (oldMaxStartTime - _results.SubFile.Subtitles[i].StartTime)) / (oldMaxStartTime - oldMinStartTime))   
                    double newNanoseconds = newMaxNanoseconds - (((newDiff) * (oldMaxNanoseconds - _results.SubFile.Subtitles[i].StartTime.TotalNanoseconds)) / (oldDiff));

                    Time durationInTime = _results.SubFile.Subtitles[i].EndTime - _results.SubFile.Subtitles[i].StartTime;

                    _results.SubFile.Subtitles[i].StartTime = new Time(0, 0, 0, 0, 0, (long)newNanoseconds);
                    _results.SubFile.Subtitles[i].EndTime = _results.SubFile.Subtitles[i].StartTime + durationInTime;
                }

                SetData(_results.SubFile.Subtitles);
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
        }
    }
}
