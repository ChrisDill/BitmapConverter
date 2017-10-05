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
                var type = GetEPLType(line.Substring(0, 1).ToCharArray()[0]);

                switch (type)
                {
                    case EPLTypeEnum.String:
                        RenderString(line, ref bmp);
                        break;
                    case EPLTypeEnum.Barcode:
                        RenderBarcode(line, ref bmp);
                        break;
                    case EPLTypeEnum.NewLine:
                        ApplyNewLine(line, ref bmp);
                        break;
                    case EPLTypeEnum.Setting: // If a setting is defined midway, it only applies to lines after it
                        ApplySetting(line);
                        break;
                    case EPLTypeEnum.Format:
                        ApplyFormat(line);
                        break;
                    case EPLTypeEnum.Quantity: // What do we do with this?
                        SetQuantity(line);
                        break;
                    case EPLTypeEnum.Unknown:
                        var num = lines.ToList().IndexOf(line);
                        throw new Exception($"unknown character on line: {num}:{Environment.NewLine}{line}");
                }
            }
            // ReSharper disable once ExpressionIsAlwaysNull
            return bmp;
        }

        private static void SetQuantity(string line)
        {
            throw new NotImplementedException();
        }

        private static void ApplyFormat(string line)
        {
            throw new NotImplementedException();
        }

        private static void ApplyNewLine(string line, ref Bitmap bmp)
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
            graphics.DrawString(line, font, Brushes.Black, point);
        } 
    }
}
