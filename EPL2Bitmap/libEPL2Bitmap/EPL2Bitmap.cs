using System;
using System.Drawing;
using System.Linq;

namespace libEPL2Bitmap
{
    public interface IEPL2Bitmap
    {
        Bitmap ConvertFromString(string EPL);
    }

    public partial class EPL2Bitmap : IEPL2Bitmap
    {
        public static Font font = new Font("Times New Roman", 12.0f);
        public static Graphics graphics;
        public static Point point;
        public static int spacing;

        public Bitmap ConvertFromString(string EPL)
        {
            var lines = EPL.Split(Environment.NewLine.ToCharArray());

            Bitmap bmp = new Bitmap(200, 200);
            graphics = Graphics.FromImage(bmp);
            graphics.FillRectangle(Brushes.Gray, 0, 0, 200, 200);

            // styling
            point = new Point(0, 0);
            spacing = 15;
          
            foreach (var line in lines)
            {
                if (line == string.Empty) continue;
                var strippedLine = StripComments(line);
                var type = GetEPLType(strippedLine.Substring(0, 1).ToCharArray()[0]);

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
                        throw new Exception($"unknown character on line: {num}:{Environment.NewLine}{line}");
                }
            }
            // ReSharper disable once ExpressionIsAlwaysNull
            return bmp;
        }

        private void ApplyNewLine()
        {
            throw new NotImplementedException();
        }

        private string StripComments(string line) => line.Contains(";") ? line.Split(';')[0] : line;

        private static void SetQuantity(string line)
        {
            throw new NotImplementedException();
        }

        private static void ApplyFormat(string line)
        {
            point.Y += spacing;
        }

        private static void ApplySetting(string line)
        {
            throw new NotImplementedException();
        }

        private static void RenderBarcode(string line, ref Bitmap bmp)
        {
            throw new NotImplementedException();
        }

        private static void RenderString(string line, ref Bitmap bmp)
        {
            throw new NotImplementedException();
        }

        
    }
}
