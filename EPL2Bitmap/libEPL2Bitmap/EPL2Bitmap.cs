using System;
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

        public void Test(string name)
        {
            PrivateFontCollection modernFont = new PrivateFontCollection();
            modernFont.AddFontFile(name);
            barcode = new Font(modernFont.Families[0], 20.0f);
        }

        public Bitmap ConvertFromString(string EPL)
        {
            // Test(@".\free3of9\free3of9.ttf");
            Test(@"free3of9.ttf");


            var lines = EPL.Split(Environment.NewLine.ToCharArray());

            Bitmap bmp = new Bitmap(344, 200);
            graphics = Graphics.FromImage(bmp);
<<<<<<< HEAD
            graphics.FillRectangle(Brushes.White, 0, 0, 344, 200);
=======
            graphics.FillRectangle(Brushes.Gray, 0, 0, 200, 200); // if this is meant to be the size of the label (for background) make it the q/Q size

            // styling
            point = new Point(0, 0);
>>>>>>> 6dfa3462eae31248ca6d1b13bf31b1fbf8a8eb06
            spacing = 15;

            foreach (var line in lines)
            {      
                if (line == string.Empty) continue;
                var strippedLine = StripComments(line);
                var type = GetEPLType(strippedLine.Substring(0, 1).ToCharArray()[0]);

                // split line and remove type
                var test = line.Remove(0, 1);
                args = test.Split(',');

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
<<<<<<< HEAD
                        var num = lines.ToList().IndexOf(line);
                        break;
                        // throw new Exception($"unknown character on line: {num}:{Environment.NewLine}{line}");
=======
                        var num = lines.ToList().IndexOf(strippedLine);
                        throw new Exception($"unknown character on line: {num}:{Environment.NewLine}{line}");
>>>>>>> 6dfa3462eae31248ca6d1b13bf31b1fbf8a8eb06
                }
            }
            return bmp;
        }

<<<<<<< HEAD
        public static int GetArg(int i)
        {
            return int.Parse(args[i]);
        }

        private static void SetQuantity(string line)
=======
        private void ApplyNewLine()
>>>>>>> 6dfa3462eae31248ca6d1b13bf31b1fbf8a8eb06
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

        private static void RenderBarcode(string line, ref Bitmap bmp)
        {
            int x = GetArg(0);
            int y = GetArg(1);
            int rotation = GetArg(2);
            string text = args[8];
            graphics.DrawString(text, barcode, Brushes.Black, x, y);
        }

        private static void RenderString(string line, ref Bitmap bmp)
        {
<<<<<<< HEAD
            int x = GetArg(0);
            int y = GetArg(1);
            int rotation = GetArg(2);
            int fontType = GetArg(3);
            int height = GetArg(4);
            int muliplier = GetArg(5);
            string text = args[7];
            graphics.DrawString(text, font, Brushes.Black, x, y);
        } 
=======
            throw new NotImplementedException();
        }

        
>>>>>>> 6dfa3462eae31248ca6d1b13bf31b1fbf8a8eb06
    }
}
