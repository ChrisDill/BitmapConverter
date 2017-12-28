﻿using System;
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
            // file browser
            string text;
            using (var dialog = new OpenFileDialog())
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK) return;
                text = File.ReadAllText(dialog.FileName);
            }

            // testing thread
            Bitmap label = null;
            Thread thread = new Thread(() => label = Epl2Bitmap.ConvertFromString(text));
            thread.Start();
            thread.Join();
            // var label = Epl2Bitmap.ConvertFromString(text);

            // draws to the bitmap not from it
            // pbLabel.DrawToBitmap(label, new Rectangle(new Point(), label.Size));
            pbLabel.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLabel.Image = label;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pbLabel.Image != null)
                pbLabel.Image.Save("test.png");
        }
    }
}
