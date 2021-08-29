using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TestAnalyzerHelper
{
    public class RoundButton : Button
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            var graphicsPath = new GraphicsPath();
            graphicsPath.AddEllipse(0, 0, 30, 15);
            Region = new Region(graphicsPath);
            base.OnPaint(e);
        }
    }
}