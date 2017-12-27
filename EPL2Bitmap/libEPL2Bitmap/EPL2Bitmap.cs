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
        public static bool debug = true;
        public static Font font = new Font("Times New Roman", 10.0f);
        public static Font barcode; // = new Font("Times New Roman", 10.0f);
        public static Graphics graphics;
        public static int spacing;
        public static string[] args;
        public int width;
        public int height;

        // current form stores new commands
        public Dictionary<string, string> forms;
        public string currentForm = "";

        private static int GetArg(int i) { return int.Parse(args[i]); }

        // store for use from form or output directly
        private static void Log(string msg) //, object[] args = null) how to pass multiple args without compile error?
        {
            if (debug)
            {
                Console.WriteLine(msg);
            }
        }

        // Load barcode font from file
        private void LoadBarcodeFont(string name)
        {
            PrivateFontCollection modernFont = new PrivateFontCollection();
            modernFont.AddFontFile(name);
            barcode = new Font(modernFont.Families[0], 20.0f);
        }

        // 203dpi
        private static int[] sizes = { 6, 7, 10, 12, 24 };
        private static void SetFont(int id)
        {
            // convert id to size
            int size = sizes[id - 1];
            if(font.Size != size)
            {
                font = new Font(font.FontFamily, size);
            }
        }

        // rotate and scale around origin(0,0 top left)
        private static void SetTransform(int x, int y, int rotation, int scaleX, int scaleY)
        {
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
            width = 400;
            height = 350;

            Bitmap bmp = new Bitmap(width, height);
            graphics = Graphics.FromImage(bmp);
            graphics.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
            spacing = 15;

            Log("Converting EPL string...");

            foreach (var line in lines)
            {      
                if (line == string.Empty)
                    continue;

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
                    RetrieveForm(line);
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
            Log("FormInformation");
            Console.WriteLine(info);
            // drawString(info, 0, 0);
        }

        private void BeginForm(string line)
        {
            forms.Add(line, "");
            currentForm = "";
            Log("BeginForm " + line);
        }

        private void RetrieveForm(string line)
        {
            if (forms.ContainsKey(line))
                currentForm = forms[line];
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
            Log("DeleteForm" + line);
        }

        private void EndForm()
        {
            currentForm = "";
            Log("EndForm");
        }

        private void ApplyNewLine()
        {
            Log("ApplyNewLine");
            graphics.FillRectangle(Brushes.White, 0, 0, width, height);
            //throw new NotImplementedException();
        }

        private string StripComments(string line) => line.Contains(";") ? line.Split(';')[0] : line;

        private static void SetQuantity(string line)
        {
            Log("SetQuantity " + line);
            //throw new NotImplementedException();
        }

        private static void ApplyFormat(string line)
        {
            Log("ApplyFormat" + line);
        }

        private static void ApplySetting(string line)
        {
            Log("ApplySetting " + line);
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
            char code = args[7].ToCharArray()[0];
            string data = args[8];

            Log("RenderBarcode");
            Log(string.Format("Position({0},{1}), Rotation({2}), Selection({3}), Narrow/Wide/Height({4},{5},{6}), Code({6}), Data({7})\n", x, y, rotation, selection, narrow, wide, height, code, data));

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
            int fontId = GetArg(3); 
            int scaleX = GetArg(4); 
            int scaleY = GetArg(5);
            EPLReverseTypeEnum type = GetEPLReverseType(args[6].ToCharArray()[0]);
            string data = args[7];

            // select brush
            Brush back = null, text = null;
            switch (type)
            {
                case EPLReverseTypeEnum.BlackOnWhite:
                    back = Brushes.White;
                    text = Brushes.Black;
                    break;
                case EPLReverseTypeEnum.WhiteOnBlack:
                    back = Brushes.Black;
                    text = Brushes.White;
                    break;
                case EPLReverseTypeEnum.Unknown:
                    Log("EPLReverseTypeEnum Unknown. Cannot select brush type for string.");
                    throw (new ArgumentException());
            }

            Log("RenderString");
            Log(string.Format("Position({0},{1}), Rotation({2}), Font({3}), Scale({4},{5}), Type({6}), Data({7})\n", x, y, rotation, fontId, scaleX, scaleY, type, data));

            // render to bitmap
            // use size of text for background
            SetTransform(x, y, rotation, scaleX, scaleY);
            SetFont(fontId);
            var size = graphics.MeasureString(data, font);
            graphics.FillRectangle(back, x, y, size.Width, size.Height);
            graphics.DrawString(data, font, text, x, y);
        }
    }
}
