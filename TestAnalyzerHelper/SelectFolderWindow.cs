using System;
using System.Drawing;
using System.Windows.Forms;

namespace TestAnalyzerHelper
{
    public partial class SelectFolderWindow : Form
    {
        private Point _mouseLocation;
        public static string PathToFolder { get; private set; }

        public SelectFolderWindow()
        {
            InitializeComponent();
        }

        private void SelectFolderButton_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                PathToFolder = folderBrowserDialog.SelectedPath;
            }

            Dispose();
        }

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseLocation = new Point(-e.X, -e.Y);
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var mousePosition = MousePosition;
                mousePosition.Offset(_mouseLocation.X, _mouseLocation.Y);
                Location = mousePosition;
            }
        }

        private void CloseWindowButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}