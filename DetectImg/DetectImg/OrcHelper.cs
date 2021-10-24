using Patagames.Ocr;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectImg
{
    public class OrcHelper
    {
        public static string GetTextFromImg(string imgPath)
        {
            string res = "";
            try
            {
                using (var objOcr = OcrApi.Create())
                {
                    objOcr.Init(Patagames.Ocr.Enums.Languages.English);
                    res = objOcr.GetTextFromImage(imgPath);
                }
            }
            catch (Exception ex)
            {
                res = ex.Message;
            }
            return res;
        }

        public static string GetTextFromImg(Bitmap imgBitmap)
        {
            string res = "";
            try
            {
                using (var objOcr = OcrApi.Create())
                {
                    objOcr.Init(Patagames.Ocr.Enums.Languages.English);
                    res = objOcr.GetTextFromImage(imgBitmap);
                }
            }
            catch (Exception ex)
            {
                res = ex.Message;
            }
            return res;
        }
    }
}
