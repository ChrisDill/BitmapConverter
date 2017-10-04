using System.Drawing;

namespace libEPL2Bitmap
{
    public interface IEPL2Bitmap
    {
        Bitmap ConvertFromString(string EPL);
    }

    public class EPL2Bitmap : IEPL2Bitmap
    {
        public Bitmap ConvertFromString(string EPL)
        {
            throw new System.NotImplementedException();
        }
    }
}
