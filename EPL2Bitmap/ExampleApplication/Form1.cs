using System;
using System.Drawing;
using System.Windows.Forms;
using libEPL2Bitmap;
using System.IO;

namespace ExampleApplication
{
    public partial class Form1 : Form
    {
        EPL2Bitmap Epl2Bitmap = new EPL2Bitmap();
        public Form1()
        {
            InitializeComponent();
        }

        public static Bitmap label;

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // file browser
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();
            if (result != DialogResult.OK)
                return;

            // load file into string
            string text = File.ReadAllText(dialog.FileName);
            label = Epl2Bitmap.ConvertFromString(text);//TODO: make open file dialog
            
            // draws to the bitmap not from it
            // pbLabel.DrawToBitmap(label, new Rectangle(new Point(), label.Size));

            pbLabel.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLabel.Image = label;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (label != null)
                label.Save("test.png");
        }
    }
}
