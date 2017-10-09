using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;

namespace libEPL2Bitmap
{
    public interface IEPL2Bitmap
    {
        Bitmap ConvertFromString(string EPL);
    }

    public partial class EPL2Bitmap : IEPL2Bitmap
    {
        public static Font font = new Font("Times New Roman", 10.0f);
        public static Font barcode; // = new Font("Times New Roman", 10.0f);
        public static Graphics graphics;
        public static int spacing;
        public static string[] args;

        public Dictionary<string, string> forms;
        public string currentForm = "";

        public void LoadBarcodeFont(string name)
        {
            PrivateFontCollection modernFont = new PrivateFontCollection();
            modernFont.AddFontFile(name);
            barcode = new Font(modernFont.Families[0], 20.0f);
        }

        public static int GetArg(int i)
        {
            return int.Parse(args[i]);
        }

        public static void SetTransform(int x, int y, int rotation, int scaleX, int scaleY)
        {
            // rotate and scale around 0,0
            graphics.ResetTransform();
            graphics.TranslateTransform(x, y);
            graphics.RotateTransform(rotation);
            graphics.ScaleTransform(scaleX, scaleY);
            graphics.TranslateTransform(-x, -y);
        }

        public Bitmap ConvertFromString(string EPL)
        {
            forms = new Dictionary<string, string>();
            LoadBarcodeFont(@"free3of9.ttf");

            var lines = EPL.Split(Environment.NewLine.ToCharArray());

            Bitmap bmp = new Bitmap(344, 192);
            graphics = Graphics.FromImage(bmp);
            graphics.FillRectangle(Brushes.White, 0, 0, 344, 192);
            spacing = 15;

            foreach (var line in lines)
            {      
                if (line == string.Empty) continue;
                var strippedLine = StripComments(line);
                var type = GetEPLType(strippedLine.Substring(0, 1).ToCharArray()[0]);

                // split line and remove type
                var test = line.Remove(0, 1);
                args = test.Split(',');

                // store commands in form
                if (currentForm != string.Empty)
                    forms[currentForm] += line;

                switch (type)
                {
                    case EPLTypeEnum.String:
                        RenderString(strippedLine, ref bmp);
                        break;
                    case EPLTypeEnum.Barcode:
                        RenderBarcode(strippedLine, ref bmp);
                        break;
                    case EPLTypeEnum.NewLine:
                        ApplyNewLine();
                        break;
                    case EPLTypeEnum.Setting: // If a setting is defined midway, it only applies to strippedLines after it
                        ApplySetting(strippedLine);
                        break;
                    case EPLTypeEnum.Format:
                        ApplyFormat(strippedLine);
                        break;
                    case EPLTypeEnum.Quantity: // What do we do with this?
                        SetQuantity(strippedLine);
                        break;
                    case EPLTypeEnum.Unknown:
                        var num = lines.ToList().IndexOf(strippedLine);
                        break;
                        // throw new Exception($"unknown character on line: {num}:{Environment.NewLine}{line}");
                    case EPLTypeEnum.Form:
                        HandleForm(line);
                        break;
                }
            }
            return bmp;
        }

        private void HandleForm(string line)
        {
            EPLFormFunctions function = GetEPLFunction(line.Substring(1, 1).ToCharArray()[0]);
            line = line.Remove(0, 2);
            switch (function)
            {
                case EPLFormFunctions.Information:          
                    FormInformation();             
                    break;
                case EPLFormFunctions.Store:
                    BeginForm(line);
                    break;
                case EPLFormFunctions.Retrieve:
                   
                    break;
                case EPLFormFunctions.Delete:
                    DeleteForm(line);
                    break;
                case EPLFormFunctions.End:
                    EndForm();
                    break;
            }
        }

        private void FormInformation()
        {
            string info = "Form information:" + Environment.NewLine;
            foreach (var i in forms)
            {
                info += "1" + Environment.NewLine;
                info += i.Key + Environment.NewLine;
                info += "Form memory left:" + Environment.NewLine;
            }
            Console.WriteLine(info);
            // drawString(info, 0, 0);
        }

        private void BeginForm(string line)
        {
            forms.Add(line, "");
            currentForm = "";
        }

        private void DeleteForm(string line)
        {
            char arg = line.Substring(2, 1).ToCharArray()[0];
            if (arg == '*')
            {
                forms.Clear();
            }
            else
            {
                forms.Remove(line);
                currentForm = "";
            }
        }

        private void EndForm()
        {
            currentForm = "";
        }

        private void ApplyNewLine()
        {
            //throw new NotImplementedException();
        }

        private string StripComments(string line) => line.Contains(";") ? line.Split(';')[0] : line;

        private static void SetQuantity(string line)
        {
            //throw new NotImplementedException();
        }

        private static void ApplyFormat(string line)
        {
            
        }

        private static void ApplySetting(string line)
        {
            //throw new NotImplementedException();
        }

        // horizontal start position
        // vertical start position
        // rotation
        // barcode selection
        // narrow bar width
        // wide bar width
        // bar code height
        // reverse image
        private static void RenderBarcode(string line, ref Bitmap bmp)
        {
            // if (args.Length != 9)
                // throw (new ArgumentException());

            int x = GetArg(0); 
            int y = GetArg(1);
            int rotation = GetArg(2) * 90;
            string selection = args[3];
            int narrow = GetArg(4);
            int wide = GetArg(5);
            int height = GetArg(6);
            // char code = args[7];
            string data = args[8];

            SetTransform(x, y, rotation, 1, 1);
            graphics.DrawString(data, barcode, Brushes.Black, x, y);
        }

        // horizontal start position
        // vertical start position
        // rotation
        // font selection
        // horizontal multiplier
        // vertical multiplier
        // reverse image
        private static void RenderString(string line, ref Bitmap bmp)
        {
            if (args.Length < 8)
                throw (new ArgumentException());
            
            int x = GetArg(0); 
            int y = GetArg(1);
            int rotation = GetArg(2) * 90; 
            int fontType = GetArg(3); 
            int scaleX = GetArg(4); 
            int scaleY = GetArg(5);
            EPLReverseTypeEnum type = GetEPLReverseType(args[6].ToCharArray()[0]);
            string data = args[7];

            // select brush type
            Brush back;
            Brush text;
            if (type == EPLReverseTypeEnum.BlackOnWhite)
            {
                back = Brushes.White;
                text = Brushes.Black;
            }
            else
            {
                back = Brushes.Black;
                text = Brushes.White;          
            }

            // render to bitmap
            // use size of text for background
            var size = graphics.MeasureString(data, font);
            SetTransform(x, y, rotation, scaleX, scaleY);
            graphics.FillRectangle(back, x, y, size.Width, size.Height);
            graphics.DrawString(data, font, text, x, y);
        }
    }
}
