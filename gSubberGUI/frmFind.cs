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
using gSubber.Core.SubtitleFile;
using System.Text.RegularExpressions;
using System.Linq;

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


        public frmFind(frmMain argFrmMain, bool argReplace) : base()
        {
            InitializeComponent();

            Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);

            this.Owner = argFrmMain;

            _frmMain = argFrmMain;

            FindSettings settings = LoadSettings();

            _IgnoreEvents = true;
            SetSettings(settings);
            _IgnoreEvents = false;

            if (!argReplace)
            {
                // Hide the replace buttons and text for better UX
                btnReplaceNext.Visible = false;
                btnReplacePrevious.Visible = false;
                btnReplaceAll.Visible = false;

                lblReplace.Visible = false;
                cmbTextForReplace.Visible = false;

                // Reduce the form's height for better UX
                this.Height -= 110;
                this.MaximumSize = new Size(this.MaximumSize.Width, this.MaximumSize.Height - 110);
            }
            else
            {
                this.Text = "Replace text";
            }

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
                if (!this.Disposing)
                {
                    if (chkUseTransparency.Checked && rbtnTransparencyOnLostFocus.Checked)
                    {
                        this.Opacity = trkTransparency.Value / 100.0;
                    }
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

        private void chkMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            if (_IgnoreEvents) return;

            SaveSettings();
        }

        private void chkWholeWord_CheckedChanged(object sender, EventArgs e)
        {
            if (_IgnoreEvents) return;

            SaveSettings();
        }

        private void chkWrapAround_CheckedChanged(object sender, EventArgs e)
        {
            if (_IgnoreEvents) return;

            SaveSettings();
        }

        private void rbtnNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (_IgnoreEvents) return;

            SaveSettings();
        }

        private void rbtnExtended_CheckedChanged(object sender, EventArgs e)
        {
            if (_IgnoreEvents) return;

            SaveSettings();
        }

        private void rbtnRegularExpression_CheckedChanged(object sender, EventArgs e)
        {
            if (_IgnoreEvents) return;

            SaveSettings();
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            Find(true);
        }

        private void btnFindPrevious_Click(object sender, EventArgs e)
        {
            Find(false);
        }

        private void btnCount_Click(object sender, EventArgs e)
        {
            CountMatches();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private enum SearchMode
        {
            Normal,
            Extended,
            RegularExpression
        }

        private static char[] _WordSeparators = new char[] { '.', ',', ' ', '?', '"', '\'', '\r', '\n', '\t', ':', ';', '<', '>', '/', '\\', '[', ']', '{', '}', '(', ')', '-', '_', '+', '=', '!', '@', '#', '$', '%', '^', '&', '*', '`', '~', '|', '\\' };

        private void Find(bool argNext)
        {
            string textToFind = cmbTextToFind.Text;

            if (string.IsNullOrWhiteSpace(textToFind))
            {
                return;
            }

            bool matchCase = chkMatchCase.Checked;
            bool matchWholeWord = chkWholeWord.Checked;
            bool wrapAround = chkWrapAround.Checked;

            SearchMode searchmode = SearchMode.Normal;

            if (rbtnNormal.Checked)
            {
                searchmode = SearchMode.Normal;
            }
            else if (rbtnExtended.Checked)
            {
                searchmode = SearchMode.Extended;
                textToFind = textToFind.Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t");
            }
            else if (rbtnRegularExpression.Checked)
            {
                searchmode = SearchMode.RegularExpression;
            }

            // If we have RegularExpression search mode, create the Regular Expression
            Regex regex = null;
            if (searchmode == SearchMode.RegularExpression)
            {
                RegexOptions regexOptions = RegexOptions.Compiled;
                string regexTextToFind = textToFind;

                // Check if we have match case
                if (!matchCase)
                {
                    regexOptions |= RegexOptions.IgnoreCase;
                }

                // Check if we have match whole word
                if (matchWholeWord)
                {
                    regexTextToFind = $@"\b{textToFind}\b";
                }

                regex = new Regex(regexTextToFind, regexOptions);
            }

            // Find the starting point (row)
            int startRowIndex = _frmMain.SubtitleGridView.SelectedIndex;
            int startTextIndex = 0;

            // If no selected row, start from the first one
            if (startRowIndex < 0)
            {
                startRowIndex = 0;
            }
            else
            {
                // Selected row was found, check for selected text
                if (!string.IsNullOrWhiteSpace(_frmMain.SubtitleItemTextBox.SelectedText))
                {
                    // Selected Text was found, set the startTextIndex at the end of selection
                    startTextIndex = _frmMain.SubtitleItemTextBox.SelectionStart + _frmMain.SubtitleItemTextBox.SelectionLength;
                }
            }

            // Set the current row index
            int currentRowIndex = startRowIndex;

            // Set a flag to know if the find operation succeeded
            bool textWasFound = false;

            // Set a flag to know if we have wrapped around
            bool wrappedAround = false;

            // Start searching the rows according to the search direction
            // argNext == true => Search down
            // argNext == false => Search up
            while (
                (argNext && currentRowIndex < _frmMain.SubtitleGridView.Rows.Count)
                || (!argNext && currentRowIndex >= 0)
                )
            {
                // Check if we wrapped around and surpassed the start row
                if (wrappedAround)
                {
                    if (
                        (argNext && currentRowIndex >= startRowIndex)
                        || (!argNext && currentRowIndex <= startRowIndex)
                        )
                    {
                        // We wrapped around and surpassed tha starting row!
                        // Break the search loop!
                        break;
                    }
                }

                // Get the subtitle item to search
                SubFileSubtitleItem sub = (SubFileSubtitleItem)_frmMain.SubtitleGridView.Rows[currentRowIndex].DataBoundItem;

                // Check if we have regular expression or normal/extended search mode
                if (searchmode == SearchMode.RegularExpression)
                {
                    Match match;
                    if (
                        // Check if we are in the start row and then search from the startTextIndex
                        currentRowIndex == startRowIndex && (match = regex.Match(sub.Text, startTextIndex)).Success
                        || currentRowIndex != startRowIndex && (match = regex.Match(sub.Text)).Success
                        )
                    {
                        // Found match! Select the row
                        _frmMain.SubtitleGridView.SetSelectedRowByIndex(currentRowIndex);

                        // Select the text
                        _frmMain.SubtitleItemTextBox.Select(match.Index, match.Length);

                        // Set the flag that the find operation was successfull
                        textWasFound = true;

                        // Exit the search loop
                        break;
                    }
                }
                else
                {
                    int startIndex;
                    if (
                        // Check if we are in the start row and then search from the startTextIndex
                        currentRowIndex == startRowIndex &&
                            (
                                // Check if we have match case and if not convert text to lower case to search
                                matchCase && (startIndex = sub.Text.IndexOf(textToFind, startTextIndex)) > -1
                                || !matchCase && (startIndex = sub.Text.ToLower().IndexOf(textToFind.ToLower(), startTextIndex)) > -1
                            )
                        // Check if we are in the start row and then search from the startTextIndex
                        || currentRowIndex != startRowIndex &&
                            (
                                // Check if we have match case and if not convert text to lower case to search
                                matchCase && (startIndex = sub.Text.IndexOf(textToFind)) > -1
                                || !matchCase && (startIndex = sub.Text.ToLower().IndexOf(textToFind.ToLower())) > -1
                            )
                        )
                    {
                        if (
                            (
                                // Check if we have match whole word and then check if the previous and next characters are in the word separators array
                                matchWholeWord &&
                                (
                                    (
                                        startIndex == 0
                                        || (startIndex > 0 && _WordSeparators.Contains(sub.Text[startIndex - 1]))
                                    )
                                    &&
                                    (
                                        startIndex + textToFind.Length == sub.Text.Length
                                        || (startIndex + textToFind.Length < sub.Text.Length && _WordSeparators.Contains(sub.Text[startIndex + textToFind.Length]))
                                    )
                                )
                            )
                            || !matchWholeWord
                        )
                        {
                            // Found match! Select the row
                            _frmMain.SubtitleGridView.SetSelectedRowByIndex(currentRowIndex);

                            // Select the text
                            _frmMain.SubtitleItemTextBox.Select(startIndex, textToFind.Length);

                            // Set the flag that the find operation was successfull
                            textWasFound = true;

                            // Exit the search loop
                            break;
                        }
                    }
                }

                // Go the next row according to search direction
                if (argNext)
                {
                    currentRowIndex++;
                    // We check if we have wrap around AND we haven't already wrapped around
                    if (wrapAround && !wrappedAround)
                    {
                        if (currentRowIndex >= _frmMain.SubtitleGridView.Rows.Count)
                        {
                            // We wrap around and start from the first row
                            currentRowIndex = 0;
                            // We set the flag to know that we wrapped around
                            wrappedAround = true;
                        }
                    }
                }
                else
                {
                    currentRowIndex--;
                    // We check if we have wrap around AND we haven't already wrapped around
                    if (wrapAround && !wrappedAround)
                    {
                        if (currentRowIndex < 0)
                        {
                            // We wrap around and start from the last row
                            currentRowIndex = _frmMain.SubtitleGridView.Rows.Count - 1;
                            // We set the flag to know that we wrapped around
                            wrappedAround = true;
                        }
                    }
                }
            }

            // Check if find operation was successfull
            if (!textWasFound)
            {
                ShowWarningMessage($"The text '{textToFind}' was not found!");
            }
        }

        private void CountMatches()
        {
            string textToFind = cmbTextToFind.Text;

            if (string.IsNullOrWhiteSpace(textToFind))
            {
                return;
            }

            bool matchCase = chkMatchCase.Checked;
            bool matchWholeWord = chkWholeWord.Checked;

            SearchMode searchmode = SearchMode.Normal;

            if (rbtnNormal.Checked)
            {
                searchmode = SearchMode.Normal;
                if (matchCase)
                {
                    textToFind = textToFind.ToLower();
                }
            }
            else if (rbtnExtended.Checked)
            {
                searchmode = SearchMode.Extended;
                textToFind = textToFind.Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t");
                if (matchCase)
                {
                    textToFind = textToFind.ToLower();
                }
            }
            else if (rbtnRegularExpression.Checked)
            {
                searchmode = SearchMode.RegularExpression;
            }

            // If we have RegularExpression search mode, create the Regular Expression
            Regex regex = null;
            if (searchmode == SearchMode.RegularExpression)
            {
                RegexOptions regexOptions = RegexOptions.Compiled;
                string regexTextToFind = textToFind;

                // Check if we have match case
                if (!matchCase)
                {
                    regexOptions |= RegexOptions.IgnoreCase;
                }

                // Check if we have match whole word
                if (matchWholeWord)
                {
                    regexTextToFind = $@"\b{textToFind}\b";
                }

                regex = new Regex(regexTextToFind, regexOptions);
            }

            // Set the current row index
            int currentRowIndex = 0;

            // Set the counter of matches
            long totalMatchCount = 0;

            // Start searching the rows
            while (currentRowIndex < _frmMain.SubtitleGridView.Rows.Count)
            {
                // Get the subtitle item to search
                SubFileSubtitleItem sub = (SubFileSubtitleItem)_frmMain.SubtitleGridView.Rows[currentRowIndex].DataBoundItem;

                // Check if we have regular expression or normal/extended search mode
                if (searchmode == SearchMode.RegularExpression)
                {
                    totalMatchCount += regex.Matches(sub.Text).Count;
                }
                else
                {
                    string subText = sub.Text;
                    if (!matchCase)
                    {
                        subText = sub.Text.ToLower();
                    }
                    int startIndex = 0;

                    while ((startIndex = subText.IndexOf(textToFind, startIndex)) > -1)
                    {
                        if (
                            (
                                // Check if we have match whole word and then check if the previous and next characters are in the word separators array
                                matchWholeWord &&
                                (
                                    (
                                        startIndex == 0
                                        || (startIndex > 0 && _WordSeparators.Contains(sub.Text[startIndex - 1]))
                                    )
                                    &&
                                    (
                                        startIndex + textToFind.Length == sub.Text.Length
                                        || (startIndex + textToFind.Length < sub.Text.Length && _WordSeparators.Contains(sub.Text[startIndex + textToFind.Length]))
                                    )
                                )
                            )
                            || !matchWholeWord
                        )
                        {
                            totalMatchCount++;
                        }

                        // Move the start index to avoid searching the same text again
                        startIndex += textToFind.Length;
                    }
                }

                currentRowIndex++;
            }

            ShowInformationMessage($"Total matches found for '{textToFind}' : {totalMatchCount}");
        }

        private void btnReplaceNext_Click(object sender, EventArgs e)
        {
            Replace(true);
        }

        private void btnReplacePrevious_Click(object sender, EventArgs e)
        {
            Replace(true);
        }

        private void Replace(bool argNext)
        {
            string textToFind = cmbTextToFind.Text;

            if (string.IsNullOrWhiteSpace(textToFind))
            {
                return;
            }

            string textForReplace = cmbTextForReplace.Text;

            bool matchCase = chkMatchCase.Checked;
            bool matchWholeWord = chkWholeWord.Checked;
            bool wrapAround = chkWrapAround.Checked;

            SearchMode searchmode = SearchMode.Normal;

            if (rbtnNormal.Checked)
            {
                searchmode = SearchMode.Normal;
            }
            else if (rbtnExtended.Checked)
            {
                searchmode = SearchMode.Extended;
                textToFind = textToFind.Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t");
            }
            else if (rbtnRegularExpression.Checked)
            {
                searchmode = SearchMode.RegularExpression;
            }


            // If we have RegularExpression search mode, create the Regular Expression
            Regex regex = null;
            if (searchmode == SearchMode.RegularExpression)
            {
                RegexOptions regexOptions = RegexOptions.Compiled;
                string regexTextToFind = textToFind;

                // Check if we have match case
                if (!matchCase)
                {
                    regexOptions |= RegexOptions.IgnoreCase;
                }

                // Check if we have match whole word
                if (matchWholeWord)
                {
                    regexTextToFind = $@"\b{textToFind}\b";
                }

                regex = new Regex(regexTextToFind, regexOptions);
            }

            // Find the starting point (row)
            int startRowIndex = _frmMain.SubtitleGridView.SelectedIndex;
            int startTextIndex = 0;

            // If no selected row, start from the first one
            if (startRowIndex < 0)
            {
                startRowIndex = 0;
            }
            else
            {
                // Selected row was found, check for selected text
                if (!string.IsNullOrWhiteSpace(_frmMain.SubtitleItemTextBox.SelectedText))
                {
                    // Selected Text was found, set the startTextIndex at the end of selection
                    startTextIndex = _frmMain.SubtitleItemTextBox.SelectionStart + _frmMain.SubtitleItemTextBox.SelectionLength;
                }
            }

            // Set the current row index
            int currentRowIndex = startRowIndex;

            // Set a flag to know if the find operation succeeded
            bool textWasFound = false;

            // Set a flag to know if we have wrapped around
            bool wrappedAround = false;

            // Start searching the rows according to the search direction
            // argNext == true => Search down
            // argNext == false => Search up
            while (
                (argNext && currentRowIndex < _frmMain.SubtitleGridView.Rows.Count)
                || (!argNext && currentRowIndex >= 0)
                )
            {
                // Check if we wrapped around and surpassed the start row
                if (wrappedAround)
                {
                    if (
                        (argNext && currentRowIndex >= startRowIndex)
                        || (!argNext && currentRowIndex <= startRowIndex)
                        )
                    {
                        // We wrapped around and surpassed tha starting row!
                        // Break the search loop!
                        break;
                    }
                }

                // Get the subtitle item to search
                SubFileSubtitleItem sub = (SubFileSubtitleItem)_frmMain.SubtitleGridView.Rows[currentRowIndex].DataBoundItem;

                // Check if we have regular expression or normal/extended search mode
                if (searchmode == SearchMode.RegularExpression)
                {
                    Match match;
                    if (
                        // Check if we are in the start row and then search from the startTextIndex
                        currentRowIndex == startRowIndex && (match = regex.Match(sub.Text, startTextIndex)).Success
                        || currentRowIndex != startRowIndex && (match = regex.Match(sub.Text)).Success
                        )
                    {
                        // Found match! Select the row
                        _frmMain.SubtitleGridView.SetSelectedRowByIndex(currentRowIndex);

                        // Replace the text
                        sub.Text = regex.Replace(sub.Text, textForReplace, 1, startTextIndex);

                        // Set the flag that the find operation was successfull
                        textWasFound = true;

                        // Exit the search loop
                        break;
                    }
                }
                else
                {
                    int startIndex;
                    if (
                        // Check if we are in the start row and then search from the startTextIndex
                        currentRowIndex == startRowIndex &&
                            (
                                // Check if we have match case and if not convert text to lower case to search
                                matchCase && (startIndex = sub.Text.IndexOf(textToFind, startTextIndex)) > -1
                                || !matchCase && (startIndex = sub.Text.ToLower().IndexOf(textToFind.ToLower(), startTextIndex)) > -1
                            )
                        // Check if we are in the start row and then search from the startTextIndex
                        || currentRowIndex != startRowIndex &&
                            (
                                // Check if we have match case and if not convert text to lower case to search
                                matchCase && (startIndex = sub.Text.IndexOf(textToFind)) > -1
                                || !matchCase && (startIndex = sub.Text.ToLower().IndexOf(textToFind.ToLower())) > -1
                            )
                        )
                    {
                        if (
                            (
                                // Check if we have match whole word and then check if the previous and next characters are in the word separators array
                                matchWholeWord &&
                                (
                                    (
                                        startIndex == 0
                                        || (startIndex > 0 && _WordSeparators.Contains(sub.Text[startIndex - 1]))
                                    )
                                    &&
                                    (
                                        startIndex + textToFind.Length == sub.Text.Length
                                        || (startIndex + textToFind.Length < sub.Text.Length && _WordSeparators.Contains(sub.Text[startIndex + textToFind.Length]))
                                    )
                                )
                            )
                            || !matchWholeWord
                        )
                        {
                            // Found match! Select the row
                            _frmMain.SubtitleGridView.SetSelectedRowByIndex(currentRowIndex);

                            // Replace the text
                            sub.Text = sub.Text.Remove(startIndex, textToFind.Length).Insert(startIndex, textForReplace);

                            // Set the flag that the find operation was successfull
                            textWasFound = true;

                            // Exit the search loop
                            break;
                        }
                    }
                }

                // Go the next row according to search direction
                if (argNext)
                {
                    currentRowIndex++;
                    // We check if we have wrap around AND we haven't already wrapped around
                    if (wrapAround && !wrappedAround)
                    {
                        if (currentRowIndex >= _frmMain.SubtitleGridView.Rows.Count)
                        {
                            // We wrap around and start from the first row
                            currentRowIndex = 0;
                            // We set the flag to know that we wrapped around
                            wrappedAround = true;
                        }
                    }
                }
                else
                {
                    currentRowIndex--;
                    // We check if we have wrap around AND we haven't already wrapped around
                    if (wrapAround && !wrappedAround)
                    {
                        if (currentRowIndex < 0)
                        {
                            // We wrap around and start from the last row
                            currentRowIndex = _frmMain.SubtitleGridView.Rows.Count - 1;
                            // We set the flag to know that we wrapped around
                            wrappedAround = true;
                        }
                    }
                }
            }

            // Update the grid to reflect the text changes
            _frmMain.SubtitleGridView.Refresh();

            // Update the text box
            if (_frmMain.SubtitleGridView.SelectedIndex == -1)
            {
                _frmMain.SubtitleItemTextBox.Clear();
            }
            else
            {
                var sub = _frmMain.SubtitleGridView.SelectedItem as SubFileSubtitleItem;
                _frmMain.SubtitleItemTextBox.Text = sub.Text;
            }

            // Check if find operation was successfull
            if (!textWasFound)
            {
                ShowWarningMessage($"The text '{textToFind}' was not found!");
            }
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            ReplaceAll();
        }

        private void ReplaceAll()
        {
            string textToFind = cmbTextToFind.Text;

            if (string.IsNullOrWhiteSpace(textToFind))
            {
                return;
            }

            string textForReplace = cmbTextForReplace.Text;

            bool matchCase = chkMatchCase.Checked;
            bool matchWholeWord = chkWholeWord.Checked;

            SearchMode searchmode = SearchMode.Normal;

            if (rbtnNormal.Checked)
            {
                searchmode = SearchMode.Normal;
                if (matchCase)
                {
                    textToFind = textToFind.ToLower();
                }
            }
            else if (rbtnExtended.Checked)
            {
                searchmode = SearchMode.Extended;
                textToFind = textToFind.Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t");
                if (matchCase)
                {
                    textToFind = textToFind.ToLower();
                }
            }
            else if (rbtnRegularExpression.Checked)
            {
                searchmode = SearchMode.RegularExpression;
            }

            // If we have RegularExpression search mode, create the Regular Expression
            Regex regex = null;
            if (searchmode == SearchMode.RegularExpression)
            {
                RegexOptions regexOptions = RegexOptions.Compiled;
                string regexTextToFind = textToFind;

                // Check if we have match case
                if (!matchCase)
                {
                    regexOptions |= RegexOptions.IgnoreCase;
                }

                // Check if we have match whole word
                if (matchWholeWord)
                {
                    regexTextToFind = $@"\b{textToFind}\b";
                }

                regex = new Regex(regexTextToFind, regexOptions);
            }

            // Set the current row index
            int currentRowIndex = 0;

            // Set the counter of matches
            long totalMatchCount = 0;

            // Start searching the rows
            while (currentRowIndex < _frmMain.SubtitleGridView.Rows.Count)
            {
                // Get the subtitle item to search
                SubFileSubtitleItem sub = (SubFileSubtitleItem)_frmMain.SubtitleGridView.Rows[currentRowIndex].DataBoundItem;

                // Check if we have regular expression or normal/extended search mode
                if (searchmode == SearchMode.RegularExpression)
                {
                    // Update the matches counter
                    totalMatchCount += regex.Matches(sub.Text).Count;

                    // Replace the text
                    sub.Text = regex.Replace(sub.Text, textForReplace);
                }
                else
                {
                    string subText = sub.Text;
                    if (!matchCase)
                    {
                        subText = sub.Text.ToLower();
                    }
                    int startIndex = 0;

                    while ((startIndex = subText.IndexOf(textToFind, startIndex)) > -1)
                    {
                        if (
                            (
                                // Check if we have match whole word and then check if the previous and next characters are in the word separators array
                                matchWholeWord &&
                                (
                                    (
                                        startIndex == 0
                                        || (startIndex > 0 && _WordSeparators.Contains(sub.Text[startIndex - 1]))
                                    )
                                    &&
                                    (
                                        startIndex + textToFind.Length == sub.Text.Length
                                        || (startIndex + textToFind.Length < sub.Text.Length && _WordSeparators.Contains(sub.Text[startIndex + textToFind.Length]))
                                    )
                                )
                            )
                            || !matchWholeWord
                        )
                        {
                            // Replace the text
                            sub.Text = sub.Text.Remove(startIndex, textToFind.Length).Insert(startIndex, textForReplace);

                            // Update the matches counter
                            totalMatchCount++;
                        }

                        // Move the start index to avoid searching the same text again
                        startIndex += textToFind.Length;
                    }
                }

                currentRowIndex++;
            }

            // Update the grid to reflect the text changes
            _frmMain.SubtitleGridView.Refresh();

            // Update the text box
            if (_frmMain.SubtitleGridView.SelectedIndex == -1)
            {
                _frmMain.SubtitleItemTextBox.Clear();
            }
            else
            {
                var sub = _frmMain.SubtitleGridView.SelectedItem as SubFileSubtitleItem;
                _frmMain.SubtitleItemTextBox.Text = sub.Text;
            }

            ShowInformationMessage($"Replaced {totalMatchCount} matches for '{textToFind}' to '{textForReplace}'");
        }
    }
}
