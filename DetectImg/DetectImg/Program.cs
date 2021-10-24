using Patagames.Ocr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetectImg
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            //string text = "";
            //if (args != null)
            //{
            //    try
            //    {
            //        using (var objOcr = OcrApi.Create())
            //        {
            //            objOcr.Init(Patagames.Ocr.Enums.Languages.English);

            //            text = objOcr.GetTextFromImage(args[0]);

            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        text = ex.Message;
            //    }
            //}
            //return text;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
