using KAutoHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOMAuto
{
    public static class LomCropImg
    {
        public static Bitmap Crop(Bitmap bm, CropRectangle rec)
        {
            Bitmap bmCrop = CaptureHelper.CropImage(bm, new Rectangle((int)rec.xPercent, (int)rec.yPercent, (int)rec.widthPercent, (int)rec.heightPercent));
            return bmCrop;
        }
    }
}
