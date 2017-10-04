using System;
using System.Drawing;
using System.Windows.Forms;
using libEPL2Bitmap;

namespace ExampleApplication
{
    public partial class Form1 : Form
    {
        EPL2Bitmap Epl2Bitmap = new EPL2Bitmap();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var label = Epl2Bitmap.ConvertFromString("");//TODO: make open file dialog
            pbLabel.DrawToBitmap(label, new Rectangle(new Point(), label.Size));
        }
    }
}
