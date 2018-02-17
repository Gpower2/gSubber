using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gSubberGUI.Controls
{
    [DefaultEvent("SelectionChanged")]
    public class GDataGridView : DataGridView
    {
        #region "Base Properties overrides"

        [Browsable(true)]
        [Category("Data")]
        public new Object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                try
                {
                    this.Enabled = false;
                    this.SuspendLayout();
                    // Check if DataSource is an IList, but not an ISortableBindingList
                    if (value is IList && !(value is ISortableBindingList))
                    {
                        // Create a SortableBindingList<T>, where T is the type of the List
                        Type genericListType = typeof(SortableBindingList<>).MakeGenericType(value.GetType().GetGenericArguments()[0]);
                        // Initialize the SortableBindingList<T>
                        ISortableBindingList objectList = (ISortableBindingList)Activator.CreateInstance(genericListType, new object[] { value });
                        // Set the SortableBindingList<T> to the base DataSource property
                        base.DataSource = objectList;
                    }
                    else
                    {
                        // If we don't have a list of objects then set the value as is 
                        base.DataSource = value;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    this.Enabled = true;
                    this.ResumeLayout();
                }
            }
        }

        #endregion

        protected PropertyGridForm _PropertyForm = null;
        protected Object _LastSelectedKey = null;

        protected Color _CustomRowBackgroundColor = Color.White;

        [Browsable(true)]
        [Category("Appearance")]
        public Color CustomRowBackgroundColor
        {
            get { return _CustomRowBackgroundColor; }
            set { _CustomRowBackgroundColor = value; SetCustomColumnProperties(); }
        }

        protected Color _CustomRowForeColor = Color.Black;

        [Browsable(true)]
        [Category("Appearance")]
        public Color CustomRowForeColor
        {
            get { return _CustomRowForeColor; }
            set { _CustomRowForeColor = value; SetCustomColumnProperties(); }
        }

        protected Color _CustomRowSelectionBackgroundColor = SystemColors.Highlight;

        [Browsable(true)]
        [Category("Appearance")]
        public Color CustomRowSelectionBackgroundColor
        {
            get { return _CustomRowSelectionBackgroundColor; }
            set { _CustomRowSelectionBackgroundColor = value; SetCustomColumnProperties(); }
        }

        protected Color _CustomRowSelectionForeColor = SystemColors.HighlightText;

        [Browsable(true)]
        [Category("Appearance")]
        public Color CustomRowSelectionForeColor
        {
            get { return _CustomRowSelectionForeColor; }
            set { _CustomRowSelectionForeColor = value; SetCustomColumnProperties(); }
        }

        protected Color _CustomRowAlternativeBackgroundColor = Color.MintCream;

        [Browsable(true)]
        [Category("Appearance")]
        public Color CustomRowAlternativeBackgroundColor
        {
            get { return _CustomRowAlternativeBackgroundColor; }
            set { _CustomRowAlternativeBackgroundColor = value; SetCustomColumnProperties(); }
        }

        protected Color _CustomRowAlternativeForeColor = Color.Black;

        [Browsable(true)]
        [Category("Appearance")]
        public Color CustomRowAlternativeForeColor
        {
            get { return _CustomRowAlternativeForeColor; }
            set { _CustomRowAlternativeForeColor = value; SetCustomColumnProperties(); }
        }

        protected String _CustomColumnFormats = String.Empty;

        [Browsable(true)]
        [Category("Custom Properties")]
        public String CustomColumnFormats
        {
            get { return _CustomColumnFormats; }
            set { _CustomColumnFormats = value; SetCustomColumnProperties(); }
        }

        protected String[] _CustomColumnFormatsArray
        {
            get
            {
                return GetStringArray(_CustomColumnFormats, "|");
            }
        }

        protected String[] GetStringArray(String argStringToSplit, String argSplitSeparator)
        {
            return String.IsNullOrWhiteSpace(argStringToSplit) ? new String[] { } : argStringToSplit.Split(new String[] { argSplitSeparator }, StringSplitOptions.None);
        }

        [Browsable(false)]
        public object SelectedItem
        {
            get
            {
                if (this.SelectedRows.Count > 0)
                {
                    return this.SelectedRows[0].DataBoundItem;
                }
                else
                {
                    return null;
                }
            }
        }

        [Browsable(false)]
        public Int32 SelectedIndex
        {
            get
            {
                if (this.SelectedRows.Count > 0)
                {
                    return this.SelectedRows[0].Index;
                }
                else
                {
                    return -1;
                }
            }
        }

        protected Int32 _KeyColumnIndex = -1;

        [Browsable(true)]
        public Int32 KeyColumnIndex
        {
            get { return _KeyColumnIndex; }
            set { _KeyColumnIndex = value; }
        }

        protected Int32 _LastClickedRowIndex = -1;

        [Browsable(false)]
        public Int32 LastClickedRowIndex
        {
            get { return _LastClickedRowIndex; }
            set { _LastClickedRowIndex = value; }
        }

        protected Int32 _LastClickedColumnIndex = -1;

        [Browsable(false)]
        public Int32 LastClickedColumnIndex
        {
            get { return _LastClickedColumnIndex; }
            set { _LastClickedColumnIndex = value; }
        }

        public GDataGridView()
            : base()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.DoubleBuffered = true;

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();

            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToOrderColumns = true;
            this.AllowUserToResizeRows = false;
            this.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.MultiSelect = false;
            this.ReadOnly = true;
            this.RowHeadersVisible = false;
            this.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridColor = Color.Gainsboro;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

            this.BackgroundColor = System.Drawing.Color.GhostWhite;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;

            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

            SetCustomColumnProperties();

            BindContextMenu();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (_PropertyForm != null)
            {
                if (!_PropertyForm.IsDisposed)
                {
                    _PropertyForm.Close();
                }
                _PropertyForm = null;
            }
        }

        #region "Context Menu"

        protected ContextMenuStrip _ContextMenu = new ContextMenuStrip();

        protected ToolStripMenuItem _OpenPropertyForm = new ToolStripMenuItem("Open Property Form");
        protected ToolStripMenuItem _CopyGridProperties = new ToolStripMenuItem("Copy Grid Properties");
        protected ToolStripMenuItem _CopyGridContents = new ToolStripMenuItem("Copy");
        protected ToolStripMenuItem _CopyCellContent = new ToolStripMenuItem("Copy Cell Value");
        protected ToolStripMenuItem _CopyColumnContent = new ToolStripMenuItem("Copy Column");
        protected ToolStripMenuItem _CopySelectionContent = new ToolStripMenuItem("Copy Selection");
        //protected ToolStripMenuItem _ExportGridContent = new ToolStripMenuItem("Export to Excel (*.xlsx)");
        protected ToolStripMenuItem _ClearSelection = new ToolStripMenuItem("Clear Selection");
        protected ToolStripMenuItem _SelectAll = new ToolStripMenuItem("Select All");

        protected void _ContextMenu_Popup(object sender, EventArgs e)
        {
            try
            {
                BuildContextMenu();
            }
            catch (Exception ex)
            {
                ex.ShowException();
            }
        }

        protected void BindContextMenu()
        {
            this.ContextMenuStrip = _ContextMenu;
            BuildContextMenu();

            this.ContextMenuStrip.Opening += _ContextMenu_Popup;

            _CopyGridProperties.Click += _CopyGridProperties_Click;
            _OpenPropertyForm.Click += _OpenPropertyForm_Click;
            _CopyGridContents.Click += _CopyGridContents_Click;
            _CopyCellContent.Click += _CopyCellContent_Click;
            _CopyColumnContent.Click += _CopyColumnContent_Click;
            _CopySelectionContent.Click += _CopySelectionContent_Click;
            //_ExportGridContent.Click += _ExportGridContents_Click;
            _SelectAll.Click += _SelectAll_Click;
            _ClearSelection.Click += _ClearSelection_Click;
        }

        protected void BuildContextMenu()
        {
            _ContextMenu.Items.Clear();

            // Start adding the Menu Items
            _ContextMenu.Items.Add(_CopyGridContents);
            _ContextMenu.Items.Add(_CopyCellContent);
            _ContextMenu.Items.Add(_CopyColumnContent);
            _CopySelectionContent.Text = String.Format("Copy Selection ({0})", this.SelectedRows != null ? this.SelectedRows.Count : 0);
            _ContextMenu.Items.Add(_CopySelectionContent);
            if (this.SelectedIndex == -1)
            {
                _ContextMenu.Items[_ContextMenu.Items.Count - 1].Enabled = false;
            }
            else
            {
                _ContextMenu.Items[_ContextMenu.Items.Count - 1].Enabled = true;
            }
            _ContextMenu.Items.Add("-");
            _SelectAll.Text = String.Format("Select All ({0})", this.Rows.Count);
            _ContextMenu.Items.Add(_SelectAll);
            if (!MultiSelect)
            {
                _ContextMenu.Items[_ContextMenu.Items.Count - 1].Enabled = false;
            }
            else
            {
                _ContextMenu.Items[_ContextMenu.Items.Count - 1].Enabled = true;
            }
            _ContextMenu.Items.Add(_ClearSelection);
            if (SelectedIndex == -1)
            {
                _ContextMenu.Items[_ContextMenu.Items.Count - 1].Enabled = false;
            }
            else
            {
                _ContextMenu.Items[_ContextMenu.Items.Count - 1].Enabled = true;
            }

            //_ContextMenu.Items.Add("-");
            //_ContextMenu.Items.Add(_ExportGridContent);

            _ContextMenu.Items.Add("-");
            _ContextMenu.Items.Add(_OpenPropertyForm);
            _ContextMenu.Items.Add(_CopyGridProperties);
        }

        private void _ClearSelection_Click(object sender, EventArgs e)
        {
            this.ClearSelection();
        }

        private void _SelectAll_Click(object sender, EventArgs e)
        {
            if (MultiSelect)
            {
                this.SelectAll();
            }
        }

        private void _CopyGridProperties_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.Columns != null))
                {
                    StringBuilder propBuilder = new StringBuilder();
                    propBuilder.AppendFormat("Column Count: {0}\r\n", Columns.Count);
                    propBuilder.Append("Column Real Widths: ");
                    foreach (DataGridViewColumn col in Columns)
                    {
                        propBuilder.AppendFormat("{0}|", col.Width);
                    }
                    propBuilder.Length -= 1;
                    propBuilder.AppendLine();
                    propBuilder.Append("Column Real Names: ");
                    foreach (DataGridViewColumn col in Columns)
                    {
                        propBuilder.AppendFormat("{0}|", col.HeaderText);
                    }
                    propBuilder.Length -= 1;
                    propBuilder.AppendLine();
                    //propBuilder.AppendFormat("Column Sizes: {0}", CustomColumnSizes);
                    //propBuilder.AppendLine();
                    //propBuilder.AppendFormat("Column Formats: {0}", CustomColumnFormats);
                    //propBuilder.AppendLine();
                    //propBuilder.AppendFormat("Column Names: {0}", CustomColumnNames);
                    //propBuilder.AppendLine();
                    //propBuilder.AppendFormat("Column Alignments: {0}", CustomColumnAlignments);
                    //propBuilder.AppendLine();
                    //propBuilder.AppendFormat("Column Sums: {0}", _ColSums);
                    //propBuilder.AppendLine();
                    //propBuilder.AppendFormat("Column Null Values: {0}", _ColNullValues);
                    //propBuilder.AppendLine();
                    //propBuilder.AppendFormat("Column Color: {0}", _ColorCol);
                    Clipboard.SetDataObject(propBuilder.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                ex.ShowException();
            }
        }

        private void _OpenPropertyForm_Click(object sender, EventArgs e)
        {
            try
            {
                Form parentForm = this.FindForm();
                if (_PropertyForm == null)
                {
                    _PropertyForm = new PropertyGridForm();
                    _PropertyForm.FormClosed += _PropertyForm_FormClosed;
                    if (parentForm.IsMdiChild)
                    {
                        _PropertyForm.MdiParent = parentForm.MdiParent;
                    }
                    _PropertyForm.PropertyGrid.SelectedObject = this;
                    _PropertyForm.Text = String.Format("Properties: {0}", this.Name);
                }
                _PropertyForm.Show();
            }
            catch (Exception ex)
            {
                ex.ShowException();
            }
        }

        private void _PropertyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _PropertyForm.FormClosed -= _PropertyForm_FormClosed;
            _PropertyForm.Dispose();
            _PropertyForm = null;
        }

        protected string _DefaultIntFormat = "#,##0";
        protected string _DefaultDateFormat = "dd/MM/yyyy";
        protected string _DefaultDecimalFormat = "#,###.00";
        protected CultureInfo _GreekCulture = CultureInfo.GetCultureInfo("el-GR", "el-GR");

        private void _CopyGridContents_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if we have Rows
                if (this.Rows.Count == 0)
                {
                    // Check if we have Columns
                    if(Columns != null && Columns.Count > 0)
                    {
                        // Set only the Headers Line
                        Clipboard.SetDataObject(String.Join("\t", Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Select(c => c.HeaderText)));
                    }
                    else
                    {
                        // Set an Empty String
                        Clipboard.SetDataObject("");
                    }                    
                }
                else
                {
                    Cursor.Current = Cursors.WaitCursor;
                    System.Text.StringBuilder textBuilder = new System.Text.StringBuilder();
                    
                    // Create the Headers line
                    textBuilder.AppendLine(String.Join("\t", Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Select(c => c.HeaderText)));

                    // Get the Grid's User Formats
                    String[] customColumnFormats = _CustomColumnFormatsArray;

                    // Create the lines for each DataTable row 
                    textBuilder.Append(
                        String.Join(Environment.NewLine,
                            Rows.Cast<DataGridViewRow>().Select(
                                dtRow => String.Join("\t", dtRow.Cells.Cast<DataGridViewCell>().
                                    Where(c => Columns[c.ColumnIndex].Visible).
                                    Select(c =>
                                        (c.ColumnIndex < customColumnFormats.Length) ?
                                        c.Value.GetClipboardTextFromProperty(_GreekCulture, customColumnFormats[c.ColumnIndex], customColumnFormats[c.ColumnIndex], customColumnFormats[c.ColumnIndex])
                                        : c.Value.GetClipboardTextFromProperty(_GreekCulture, _DefaultIntFormat, _DefaultDecimalFormat, _DefaultDateFormat)
                    )))));

                    // Set the final text to the Clipboard
                    Clipboard.SetDataObject(textBuilder.ToString());
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Cursor.Current = Cursors.Default;
                ex.ShowException();
            }
        }

        private void _CopyCellContent_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if we have Rows
                if (this.Rows.Count == 0)
                {
                    Clipboard.SetDataObject(string.Empty);
                }
                else
                {
                    // Check if we have a Selection saved
                    if ((LastClickedRowIndex > -1 && LastClickedColumnIndex > -1))
                    {
                        // Get the Grid's User Formats
                        String[] customColumnFormats = _CustomColumnFormatsArray;

                        string textForClipboard = string.Empty;
                        object dtObj = this.Rows[LastClickedRowIndex].Cells[LastClickedColumnIndex].Value;

                        if (LastClickedColumnIndex < customColumnFormats.Length)
                        {
                            textForClipboard = dtObj.GetClipboardTextFromProperty(_GreekCulture, customColumnFormats[LastClickedColumnIndex], customColumnFormats[LastClickedColumnIndex], customColumnFormats[LastClickedColumnIndex]);
                        }
                        else
                        {
                            textForClipboard = dtObj.GetClipboardTextFromProperty(_GreekCulture, _DefaultIntFormat, _DefaultDecimalFormat, _DefaultDateFormat);
                        }

                        // Set the final text to the Clipboard
                        Clipboard.SetDataObject(textForClipboard);
                    }
                    else
                    {
                        // If no selection was found return empty String
                        Clipboard.SetDataObject(string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Cursor.Current = Cursors.Default;
                ex.ShowException();
            }
        }

        private void _CopyColumnContent_Click(object sender, System.EventArgs e)
        {
            try
            {
                // Check if we have Rows
                if (this.Rows.Count == 0)
                {
                    Clipboard.SetDataObject(string.Empty);
                }
                else
                {
                    // Check if we have a Selection saved
                    if ((LastClickedRowIndex > -1 && LastClickedColumnIndex > -1))
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        StringBuilder textBuilder = new System.Text.StringBuilder();

                        // Get the Grid's User Formats
                        String[] customColumnFormats = _CustomColumnFormatsArray;

                        string separator = string.Empty;
                        if (MessageBox.Show("Do you want to copy the column contents as comma separated values?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            separator = ",";
                        }
                        else
                        {
                            separator = Environment.NewLine;
                        }

                        textBuilder.Append(String.Join(separator,
                            Rows.Cast<DataGridViewRow>().Select(r => r.Cells[LastClickedColumnIndex].Value).
                                Select(
                                    v => RemoveSpecialChars(
                                        (LastClickedColumnIndex < customColumnFormats.Length) ?
                                        v.GetClipboardTextFromProperty(_GreekCulture, customColumnFormats[LastClickedColumnIndex], customColumnFormats[LastClickedColumnIndex], customColumnFormats[LastClickedColumnIndex])
                                       : v.GetClipboardTextFromProperty(_GreekCulture, _DefaultIntFormat, _DefaultDecimalFormat, _DefaultDateFormat)
                                   )
                        )));

                        // Set the final text to the Clipboard
                        Clipboard.SetDataObject(textBuilder.ToString());
                        Cursor.Current = Cursors.Default;
                    }
                    else
                    {
                        // If no selection was found return empty String
                        Clipboard.SetDataObject(string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Cursor.Current = Cursors.Default;
                ex.ShowException();
            }
        }

        private void _CopySelectionContent_Click(object sender, System.EventArgs e)
        {
            try
            {
                // Check if we have Rows
                // Or if we have Multiple Selection Enabled but now Selected Rows
                // Or if we have Mulitple Selection disabled and no Selected Row
                if (this.Rows.Count == 0
                    || (MultiSelect && SelectedRows.Count == 0)
                    || ((!MultiSelect) && SelectedIndex == -1))
                {
                    // Check if we have Columns
                    if (Columns != null && Columns.Count > 0)
                    {
                        // Set only the Headers Line
                        Clipboard.SetDataObject(String.Join("\t", Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Select(c => c.HeaderText)));
                    }
                    else
                    {
                        // Set an Empty String
                        Clipboard.SetDataObject("");
                    }
                }
                else
                {
                    Cursor.Current = Cursors.WaitCursor;

                    System.Text.StringBuilder textBuilder = new System.Text.StringBuilder();

                    // Get the Grid's User Formats
                    String[] customColumnFormats = _CustomColumnFormatsArray;

                    // Create the Headers line
                    textBuilder.AppendLine(String.Join("\t", Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Select(c => c.HeaderText)));

                    // Create the lines for each DataTable row 
                    if ((MultiSelect))
                    {
                        textBuilder.Append(
                           String.Join(Environment.NewLine,
                               SelectedRows.Cast<DataGridViewRow>().Select(
                                   dtRow => String.Join("\t", dtRow.Cells.Cast<DataGridViewCell>().
                                       Where(c => Columns[c.ColumnIndex].Visible).
                                       Select(c =>
                                           (c.ColumnIndex < customColumnFormats.Length) ?
                                           c.Value.GetClipboardTextFromProperty(_GreekCulture, customColumnFormats[c.ColumnIndex], customColumnFormats[c.ColumnIndex], customColumnFormats[c.ColumnIndex])
                                           : c.Value.GetClipboardTextFromProperty(_GreekCulture, _DefaultIntFormat, _DefaultDecimalFormat, _DefaultDateFormat)
                       )))));
                    }
                    else
                    {
                        // Add the line to the final text 
                        textBuilder.AppendLine(String.Join("\t", SelectedRows[0].Cells.Cast<DataGridViewCell>().
                            Where(c => Columns[c.ColumnIndex].Visible).
                            Select(c =>
                                (c.ColumnIndex < customColumnFormats.Length) ?
                                c.Value.GetClipboardTextFromProperty(_GreekCulture, customColumnFormats[c.ColumnIndex], customColumnFormats[c.ColumnIndex], customColumnFormats[c.ColumnIndex])
                                : c.Value.GetClipboardTextFromProperty(_GreekCulture, _DefaultIntFormat, _DefaultDecimalFormat, _DefaultDateFormat)
                        )));
                    }

                    // Set the final text to the Clipboard
                    Clipboard.SetDataObject(textBuilder.ToString());
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Cursor.Current = Cursors.Default;
                ex.ShowException();
            }
        }

        //private void _ExportGridContents_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Check if we have Rows
        //        if (DataSource == null)
        //        {
        //            return;
        //        }

        //        // Ask user 
        //        SaveFileDialog sfd = new SaveFileDialog();
        //        sfd.Title = "Select an Excel file for export...";
        //        sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
        //        sfd.RestoreDirectory = true;
        //        sfd.OverwritePrompt = true;
        //        if (sfd.ShowDialog() == DialogResult.OK)
        //        {
        //            if (DataSource is DataTable)
        //            {
        //                DataSet printDataSet = new DataSet();
        //                printDataSet.Tables.Add((DataTable)DataSource);

        //                gExcelHelper.DataSetToExcelFile(printDataSet, sfd.FileName);
        //            }
        //            else if (DataSource is IList)
        //            {
        //                gExcelHelper.ListToExcelFile((IList)DataSource, sfd.FileName);
        //            }
        //            else
        //            {
        //                throw new Exception(String.Format("Excel exporting is not supported for this type of Data ({0})!", DataSource.GetType().Name));
        //            }
        //            MessageBox.Show(String.Format("The file {0} was exported successfully!", sfd.FileName), "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //        Cursor.Current = Cursors.Default;
        //        ex.ShowException();
        //    }
        //}

        private void gDataGridView_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            try
            {
                e.ContextMenuStrip = _ContextMenu;
            }
            catch (Exception ex)
            {
                ex.ShowException();
            }
        }

        private string RemoveSpecialChars(string str)
        {
            string strToReturn = str;
            strToReturn = strToReturn.Replace("\t", " ");
            strToReturn = strToReturn.Replace("\r\n", " ");
            strToReturn = strToReturn.Replace("\r", " ");
            strToReturn = strToReturn.Replace("\n", " ");
            strToReturn = strToReturn.Replace("\"", "");
            strToReturn = strToReturn.Replace("'", "");
            return strToReturn;
        }

        #endregion

        public void SetSelectedRowByIndex(Int32 idx)
        {
            // check if DataGridView contains any rows
            if (this.Rows.Count == 0)
            {
                return;
            }
            // check if index is greater than the last row index
            if (idx >= this.Rows.Count - 1)
            {
                idx = this.Rows.Count - 1;
            }
            // check if index is less than 0
            if (idx < 0)
            {
                idx = 0;
            }

            if (KeyColumnIndex > -1 && KeyColumnIndex < this.ColumnCount)
            {
                _LastSelectedKey = this.Rows[idx].Cells[0].Value;
            }

            this.Rows[idx].Selected = true;
            // Try to find the first visible cell
            Int32 firsVisibleCellIndex = this.Rows[idx].Cells.Cast<DataGridViewCell>().Where(c => c.Visible).FirstOrDefault().ColumnIndex;
            this.CurrentCell = this.Rows[idx].Cells[firsVisibleCellIndex];
            this.FirstDisplayedScrollingRowIndex = idx;
            this.PerformLayout();
        }

        public void SetSelectedRowByKey(object key)
        {
            // check if DataGridView contains any rows
            if (this.Rows.Count == 0)
            {
                return;
            }
            // check if KeyColumnIndex is set
            if (KeyColumnIndex == -1)
            {
                return;
            }
            // check if KeyColumnIndex is valid
            if (KeyColumnIndex >= this.ColumnCount)
            {
                return;
            }
            DataGridViewRow keyRow = null;
            foreach (DataGridViewRow row in this.Rows)
            {
                try
                {
                    if (Convert.ChangeType(row.Cells[KeyColumnIndex].Value, key.GetType()).Equals(key))
                    {
                        keyRow = row;
                        break;
                    }
                }
                catch (Exception)
                {
                    break;
                }
            }

            Int32 keyIndex = -1;
            if (keyRow != null)
            {
                keyIndex = keyRow.Index;
            }
            if (keyIndex > -1)
            {
                _LastSelectedKey = key;
                this.Rows[keyIndex].Selected = true;
                //this.CurrentCell = this.Rows[keyIndex].Cells[0];
                this.FirstDisplayedScrollingRowIndex = keyIndex;
                this.PerformLayout();
            }
        }

        protected void SetCustomColumnProperties()
        {
            try
            {
                this.Enabled = false;
                this.SuspendLayout();

                this.DefaultCellStyle.BackColor = _CustomRowBackgroundColor;
                this.DefaultCellStyle.ForeColor = _CustomRowForeColor;
                this.DefaultCellStyle.SelectionBackColor = _CustomRowSelectionBackgroundColor;
                this.DefaultCellStyle.SelectionForeColor = _CustomRowSelectionForeColor;
                this.AlternatingRowsDefaultCellStyle.BackColor = _CustomRowAlternativeBackgroundColor;
                this.AlternatingRowsDefaultCellStyle.ForeColor = _CustomRowAlternativeForeColor;
                this.AlternatingRowsDefaultCellStyle.SelectionBackColor = _CustomRowSelectionBackgroundColor;
                this.AlternatingRowsDefaultCellStyle.SelectionForeColor = _CustomRowSelectionForeColor;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                this.ResumeLayout();
                this.PerformLayout();
                this.Enabled = true;
            }
        }

        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {
            base.OnDataBindingComplete(e);

            try
            {
                this.SuspendLayout();
                if (_KeyColumnIndex > -1 && _LastSelectedKey != null && e.ListChangedType == ListChangedType.Reset)
                {
                    this.BeginInvoke((MethodInvoker)delegate ()
                    {
                        this.SetSelectedRowByKey(_LastSelectedKey);
                    });
                }
                SetCustomColumnProperties();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            finally
            {
                this.ResumeLayout();
            }
        }

        protected Int32 _ColumnResizedIndex = -1;
        protected Boolean _ColumnResizing = false;

        protected override void OnCellMouseUp(DataGridViewCellMouseEventArgs e)
        {
            base.OnCellMouseUp(e);

            try
            {
                _LastClickedRowIndex = e.RowIndex;
                _LastClickedColumnIndex = e.ColumnIndex;

                if (Cursor.Current == Cursors.SizeWE)
                {
                    _ColumnResizing = false;
                }
            }
            catch (Exception ex)
            {
                ex.ShowException();
            }
        }

        protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
        {
            base.OnCellMouseDown(e);

            try
            {
                _LastClickedRowIndex = e.RowIndex;
                _LastClickedColumnIndex = e.ColumnIndex;

                if (_KeyColumnIndex > -1 && this.SelectedRows.Count > 0)
                {
                    _LastSelectedKey = this.SelectedRows[0].Cells[_KeyColumnIndex].Value;
                }
                else
                {
                    _LastSelectedKey = null;
                }

                _ColumnResizing = false;
                if (Cursor.Current == Cursors.SizeWE && e.RowIndex == -1 && e.ColumnIndex > -1)
                {
                    _ColumnResizedIndex = e.ColumnIndex;

                    // if we are in the first 8 pixel, we assume we resize the left column
                    if (e.X < 8)
                    {
                        _ColumnResizedIndex = _ColumnResizedIndex - 1;
                    }

                    _ColumnResizing = true;
                }
            }
            catch (Exception ex)
            {
                ex.ShowException();
            }
        }

        protected override void OnCellMouseMove(DataGridViewCellMouseEventArgs e)
        {
            base.OnCellMouseMove(e);

            try
            {
                if (e.Button == MouseButtons.Left && _ColumnResizing)
                {
                    var c = new DataGridViewColumnEventArgs(this.Columns[_ColumnResizedIndex]);

                    if (e.ColumnIndex > _ColumnResizedIndex)
                    {
                        // If we are in the next column, then e.X is zeroed
                        // so we add the Column Width
                        c.Column.Width += e.X;
                    }
                    else
                    {
                        // If we are in the same column, then e.X equals to Width
                        c.Column.Width = e.X;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
