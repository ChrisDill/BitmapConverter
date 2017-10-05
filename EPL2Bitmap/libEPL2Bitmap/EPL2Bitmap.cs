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
        public Bitmap ConvertFromString(string EPL)
        {
            var lines = EPL.Split(Environment.NewLine.ToCharArray());
            Bitmap bmp = null;
            foreach (var line in lines)
            {
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
            throw new NotImplementedException();
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
