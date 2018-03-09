using System;
using System.Drawing;
using System.Windows.Forms;
using libEPL2Bitmap;
using System.IO;
using System.Threading;

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
            // load EPL from file
            string text;
            using (var dialog = new OpenFileDialog())
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }
                text = File.ReadAllText(dialog.FileName);
                LoadEPL(text);
                // TODO: setting to process folders or if file not found
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pbLabel.Image != null)
                pbLabel.Image.Save("test.png");
        }

        public void LoadEPL(string text)
        {
            Bitmap label = null;
            Thread thread = new Thread(() =>
            {
                label = Epl2Bitmap.ConvertFromString(text);
                Invoke(new Action(() =>
                {
                    pbLabel.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbLabel.Image = label;
                    Console.WriteLine("Updated bitmap");
                }));
            });
            thread.Start();
        }
    }
}
