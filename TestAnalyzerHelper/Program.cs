using System;
using System.Windows.Forms;

namespace TestAnalyzerHelper
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SelectFolderWindow());

            if (SelectFolderWindow.PathToFolder != null)
                Application.Run(new TestAnalyzerHelper());
        }
    }
}