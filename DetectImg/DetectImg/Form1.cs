using Patagames.Ocr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetectImg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sourceFilePath1 = @"tessdata/1.png";
            var sourceFilePath = @"tessdata/2.png";
            var sourceFilePath2 = @"tessdata/3.png";

            using (var objOcr = OcrApi.Create())
            {
                objOcr.Init(Patagames.Ocr.Enums.Languages.English);

                string plainText = objOcr.GetTextFromImage(sourceFilePath);
                string plainText3 = objOcr.GetTextFromImage(sourceFilePath2);
                string plainText1 = objOcr.GetTextFromImage(@"E:\project\auto\DetectImg\DetectImg\bin\Debug\map5.png");

            }

            ////your sample image location
            //using (var engine = new TesseractEngine(path, "eng"))
            //{
            //    engine.SetVariable("user_defined_dpi", "70"); //set dpi for supressing warning
            //    using (var img = Pix.LoadFromFile(sourceFilePath))
            //    {
            //        using (var page = engine.Process(img))
            //        {
            //            var text = page.GetText();
            //            Console.WriteLine();
            //            Console.WriteLine("---Image Text---");
            //            Console.WriteLine();
            //            Console.WriteLine(text);
            //        }
            //    }
            //}
        }
    }
}
