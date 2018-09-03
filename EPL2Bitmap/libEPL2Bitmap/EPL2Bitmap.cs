using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace libEPL2Bitmap
{
    // TODO:
    // text - font size selection, can't change size on font without a new one?)
    // barcode - scale(given in widths of bars?) Not implementing a barcode system myself.
    // EAN13 and maybe EAN8 barcodes

    /// <summary>
    /// Create bitmap from string
    /// </summary>
    public interface IEPL2Bitmap
    {
        Bitmap ConvertFromString(string EPL);
    }

    /// <summary>
    /// Create bitmap from EPL string
    /// </summary>
    public partial class EPL2Bitmap : IEPL2Bitmap
    {
        public static List<Font> fonts = new List<Font>();
        public static Font barcode;
        public static Graphics graphics;
        public static string[] args;
        public static int width = 430;
        public static int height = 350;
        public Dictionary<string, string> forms;
        public string currentForm = "";

        // util
        private static int GetArg(int i) { return int.Parse(args[i]); }
        private string StripComments(string line) => line.Contains(";") ? line.Split(';')[0] : line;
        private static void Log(string msg)
        {
            Console.WriteLine(msg);
        }

        private void LoadFonts()
        {
            // external barcde font
            /*string name = @"free3of9.ttf";
            var modernFont = new PrivateFontCollection();
            modernFont.AddFontFile(name);
            barcode = new Font(modernFont.Families[0], 20.0f);*/

            // differnt sizes for text
            forms = new Dictionary<string, string>();
            fonts.Add(new Font("Times New Roman", 6, FontStyle.Regular));
            fonts.Add(new Font("Times New Roman", 7, FontStyle.Regular));
            fonts.Add(new Font("Times New Roman", 10, FontStyle.Regular));
            fonts.Add(new Font("Times New Roman", 12, FontStyle.Regular));
            fonts.Add(new Font("Times New Roman", 24, FontStyle.Regular));
        }

        private static Bitmap ResizeBitmap(int w, int h, Bitmap bitmap)
        {
            width = w;
            height = h;
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);

            // use new bitmap for render commands
            graphics = Graphics.FromImage(result);

            return result;
        }

        // 1-5
        private static int[] sizes1 = { 6, 7, 10, 12, 24 }; // 203dpi
        private static int[] sizes2 = { 4, 6, 8, 10, 21 }; // 300dpi
        // A-Z reserved for soft font storage
        // a-z reserved for printer dirver support for stoarge of user selected soft fonts
        // 6/7 - 14x19 dots
 
        /// <summary>
        /// rotate and scale around origin(0,0 top left)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="rotation"></param>
        /// <param name="scaleX"></param>
        /// <param name="scaleY"></param>
        private static void SetTransform(int x, int y, int rotation, int scaleX, int scaleY)
        {
            graphics.ResetTransform();
            graphics.TranslateTransform(x, y);
            graphics.RotateTransform(rotation);
            graphics.ScaleTransform(scaleX, scaleY);
            graphics.TranslateTransform(-x, -y);
        }

        /// <summary>
        /// Take the EPL string and convert it to a Bitmap object
        /// </summary>
        /// <param name="EPL"></param>
        /// <returns></returns>
        public Bitmap ConvertFromString(string EPL)
        {
            LoadFonts();

            var lines = EPL.Split(Environment.NewLine.ToCharArray());

            width = 430;
            height = 350;
            Bitmap bmp = new Bitmap(width, height);
            graphics = Graphics.FromImage(bmp);
            graphics.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
  
            Log("Converting EPL string");

            foreach (var line in lines)
            {     
                // command arguments, type varies in length
                var strippedLine = StripComments(line);
                // strippedline = strippedline.Remove(0, 1);
                args = strippedLine.Split(',');

                // return if no commands
                if (strippedLine == string.Empty)
                    continue;

                // convert to enum type
                var type = GetEPLType(args[0].Substring(0, 1).ToCharArray()[0]);

                // split first argument, store number to remove type
                args[0] = Regex.Match(args[0], @"\d+").ToString();
   
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
                        bmp = ApplyFormat(strippedLine, bmp);
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
                    case EPLTypeEnum.Box:
                        RenderBox(line);
                        break;
                    case EPLTypeEnum.Line:
                        RenderLine(line);
                        break;
                }
            }
            //bmp = ResizeBitmap(700, 700, bmp);
            return bmp;
        }

        /// <summary>
        /// Handle commands to do with the form
        /// </summary>
        /// <param name="line"></param>
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

        /// <summary>
        /// Display form information, called from HandleForm()
        /// </summary>
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
            // DrawString(info, 0, 0);
        }

        /// <summary>
        /// Start a new form
        /// </summary>
        /// <param name="line"></param>
        private void BeginForm(string line)
        {
            Log("BeginForm " + line);
            forms.Add(line, "");
            currentForm = "";
        }

        /// <summary>
        /// Set the current form to be used
        /// </summary>
        /// <param name="line"></param>
        private void RetrieveForm(string line)
        {
            if (forms.ContainsKey(line))
                currentForm = forms[line];
        }

        /// <summary>
        /// Remmove a form
        /// </summary>
        /// <param name="line"></param>
        private void DeleteForm(string line)
        {
            Log("DeleteForm" + line);
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
            Log("EndForm");
        }

        private void ApplyNewLine()
        {
            Log("ApplyNewLine");
            graphics.FillRectangle(Brushes.White, 0, 0, width, height);
            //throw new NotImplementedException();
        }

        private static void SetQuantity(string line)
        {
            Log("SetQuantity " + line);
            //throw new NotImplementedException();
        }

        private static Bitmap ApplyFormat(string line, Bitmap bitmap)
        {
            Log("ApplyFormat" + line);

            var format = line.ToCharArray()[0];
            int width = bitmap.Width;
            int height = bitmap.Height;

            if (format == 'q')
            {
                width = int.Parse(args[0]);
            }
            else if (format == 'Q')
            {
                height = int.Parse(args[0]);
            }
            var result = ResizeBitmap(width, bitmap.Height, bitmap);
            return result;
        }

        private static void ApplySetting(string line)
        {
            Log("ApplySetting " + line);
        }
    }
}
