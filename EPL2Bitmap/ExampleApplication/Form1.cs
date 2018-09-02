using System;
using System.Drawing;
using System.Windows.Forms;
using libEPL2Bitmap;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace ExampleApplication
{
    public partial class Form1 : Form
    {
        EPL2Bitmap Epl2Bitmap = new EPL2Bitmap();
        string filename;

        public Form1()
        {
            InitializeComponent();
            AllowDrop = true;
        }

        private void loadEPLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // load EPL from file
            string text;
            using (var dialog = new OpenFileDialog())
            {
                //var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                //dialog.InitialDirectory = path + "../../";
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }
                text = File.ReadAllText(dialog.FileName);
                filename = Path.GetFileNameWithoutExtension(dialog.FileName);
                LoadEPL(text);
                // TODO: setting to process folders or if file not found
            }
        }

        private void loadZPLToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveEPLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pbLabel.Image != null)
                pbLabel.Image.Save(filename + "_test.png");
        }

        private void githubSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://github.com/ChrisDill/EPL2Bitmap");
            Process.Start(sInfo);
        }

        public void LoadEPL(string text)
        {
            Bitmap label = null;
            var thread = new Thread(() =>
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

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) Console.WriteLine(file);

            if (files.Length > 0)
            {
                var text = File.ReadAllText(files[0]);
                LoadEPL(text);
            }
        }
    }
}
