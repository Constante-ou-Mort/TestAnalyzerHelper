using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestAnalyzerHelper.Models;

namespace TestAnalyzerHelper
{
    public partial class TestAnalyzerHelper : Form
    {
        private Point _mouseLocation;
        private string _pathForSavedFile = @"C:\Users\User\Desktop";
        private readonly List<GridModel> _gridModels = GetTestCases();

        private const int _cGrip = 4;      // Grip size
        private const int _cCaption = 13;   // Caption bar height;
        private const int _wsMinimizebox = 0x20000;
        private const int _csDblclks = 0x8;

        public TestAnalyzerHelper()
        {
            InitializeComponent();
            AddDataToTable(_gridModels);
            SetDefaultPathForSaving();
            DataGridView.DoubleBuffered(true);
            MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.Style |= _wsMinimizebox;
                cp.ClassStyle |= _csDblclks;
                return cp;
            }
        }

        private static List<GridModel> GetTestCases()
        {
            var files = Directory.GetFiles(SelectFolderWindow.PathToFolder);
            var testCasesFromAllure = files.Select(file => JsonConvert.DeserializeObject<TestCaseModel>(File.ReadAllText(file)))
                .Where(testCase => testCase.Status == "failed").ToList();

            return testCasesFromAllure.Select(testCase => new GridModel { TestName = testCase.Name }).ToList();
        }

        private void SetDefaultPathForSaving()
        {
            CurrentPathToSavedFile.Text = $"Current path for saving: {_pathForSavedFile}";
        }

        private void AddDataToTable(List<GridModel> gridModels)
        {
            var table = new DataTable();
            table.Columns.Add("Test name");
            table.Columns.Add("Status");
            table.Columns.Add("Comment");

            for (var index = 0; index < gridModels.Count; index++)
            {
                var test = gridModels[index];
                var row = table.NewRow();
                row["Test name"] = test.TestName;
                row["Status"] = test.Status;
                row["Comment"] = test.Comment;
                table.Rows.Add(row);
                DataGridView.Rows.Add();
                var currentRow = DataGridView.Rows[index];
                currentRow.Cells[0].Value = test.TestName;
                currentRow.Cells[1].Value = test.Status;
                currentRow.Cells[2].Value = test.Comment;
                currentRow.Cells[1].Style.ForeColor = Color.White;
            }
        }

        private async Task SaveCurrentTableAsFile()
        {
            var rows = DataGridView.Rows;
            var file = File.CreateText($"{_pathForSavedFile}\\TestAnalyze_{DateTime.Now:dd_MM_yyyy}.txt");

            foreach (DataGridViewRow row in rows)
            {
                if (row.Cells[1].Value != null && !row.Cells[1].Value.Equals(string.Empty))
                    await file.WriteLineAsync(
                        $"{row.Cells[0].Value}, Status: {row.Cells[1].Value}, Comment: {row.Cells[2].Value}");
            }
            file.Close();
        }

        private List<GridModel> GetCurrentGridInfo() =>
            DataGridView.Rows.Cast<DataGridViewRow>()
                .Select(row => new GridModel
                {
                    TestName = row.Cells[0]?.Value?.ToString(),
                    Status = row.Cells[1]?.Value?.ToString(),
                    Comment = row.Cells[2]?.Value?.ToString()
                }).ToList();

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {  // Trap WM_NCHITTEST
                var pos = new Point(m.LParam.ToInt32());
                pos = PointToClient(pos);
                if (pos.Y < _cCaption)
                {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
                if (pos.X >= ClientSize.Width - _cGrip && pos.Y >= ClientSize.Height - _cGrip)
                {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }
            }
            base.WndProc(ref m);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var rc = new Rectangle(ClientSize.Width - _cGrip, ClientSize.Height - _cGrip, _cGrip, _cGrip);
            ControlPaint.DrawSizeGrip(e.Graphics, BackColor, rc);
            rc = new Rectangle(0, 0, ClientSize.Width, _cCaption);
            e.Graphics.FillRectangle(Brushes.DarkBlue, rc);
        }

        private void SetFolderForSavingButton_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                CurrentPathToSavedFile.Text = $"Selected path for saving: {folderBrowserDialog.SelectedPath}";
                _pathForSavedFile = folderBrowserDialog.SelectedPath;
            }
        }

        private async void SaveFileButton_Click(object sender, EventArgs e)
        {
            SaveFileButton.Enabled = false;
            await SaveCurrentTableAsFile();
            SaveFileButton.Enabled = true;
            MessageBox.Show("File has been saved");
        }

        private void TitleBar_MouseDown(object sender, MouseEventArgs e) => _mouseLocation = new Point(-e.X, -e.Y);

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var mousePosition = MousePosition;

                if (WindowState == FormWindowState.Maximized && mousePosition.Y > 10)
                    WindowState = FormWindowState.Normal;
                if (Bounds.Top <= 0 && WindowState != FormWindowState.Maximized)
                    WindowState = FormWindowState.Maximized;

                mousePosition.Offset(_mouseLocation.X, _mouseLocation.Y);
                Location = mousePosition;
            }
        }

        private void CloseWindowButton_Click(object sender, EventArgs e) => Application.Exit();

        private void MaximizeOrMinimizeButton_Click(object sender, EventArgs e) =>
            WindowState = WindowState == FormWindowState.Normal
                ? FormWindowState.Maximized
                : FormWindowState.Normal;

        private void RollUpButton_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void TitleBar_MouseDoubleClick(object sender, MouseEventArgs e) => MaximizeOrMinimizeButton_Click(sender, null);

        private void DataGridViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            /* If this isn't a valid cell location, leave now */
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            /* Get the row, and then the cell, of the clicked cell */
            var row = DataGridView.Rows[e.RowIndex];
            var cell = row.Cells[e.ColumnIndex];

            /* If the cell is a combobox, make it drop down */
            if (cell is DataGridViewComboBoxCell)
            {
                var comboboxEdit = (DataGridViewComboBoxEditingControl)DataGridView.EditingControl;
                if (comboboxEdit != null)
                {
                    comboboxEdit.DroppedDown = true;
                }
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            if (SearchInGridField.Text != string.Empty)
            {
                UpdateGridState();
                SearchInGridField.Text = string.Empty;
            }
        }

        private void SearchInGridField_TextChanged(object sender, EventArgs e)
        {
            //TODO refactor.
            var filteredGrid = _gridModels.Where(tc => tc.TestName.ToLower().Contains(SearchInGridField.Text.ToLower())).ToList();
            UpdateGridState();
            DataGridView.Rows.Clear();

            if (filteredGrid.Count == 0)
            {
                MessageBox.Show("Sorry no rows was found.");
                return;
            }

            AddDataToTable(SearchInGridField.Text == string.Empty ? _gridModels : filteredGrid);
        }

        private void UpdateGridState()
        {
            //TODO refactor.
            var gridState = _gridModels;
            var currentGrid = GetCurrentGridInfo();

            if (currentGrid.Count != 0)
            {
                foreach (var currentGridModel in currentGrid)
                {
                    var row = gridState.First(r => r.TestName.Equals(currentGridModel.TestName));
                    if (row.Status != currentGridModel.Status)
                        row.Status = currentGridModel.Status;
                    if (row.Comment != currentGridModel.Comment)
                        row.Comment = currentGridModel.Comment;
                }
            }
        }
    }
}