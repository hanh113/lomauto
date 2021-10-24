using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using KAutoHelper;

namespace LOMAuto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SwipeToRight();
        }
        public void SwipeToRight()
        {
            string deviceID = null;
            var listDevice = KAutoHelper.ADBHelper.GetDevices();
            if (listDevice != null && listDevice.Count > 0)
            {
                deviceID = listDevice.First();
            }
            ADBHelper.Swipe(deviceID, 895, 700, 235, 1322);
        }
        public void SwipeToLeft()
        {
            string deviceID = null;
            var listDevice = KAutoHelper.ADBHelper.GetDevices();
            if (listDevice != null && listDevice.Count > 0)
            {
                deviceID = listDevice.First();
            }
            ADBHelper.Swipe(deviceID, 235, 1322, 895, 700);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string deviceID = null;
            var listDevice = KAutoHelper.ADBHelper.GetDevices();
            if (listDevice != null && listDevice.Count > 0)
            {
                deviceID = listDevice.First();
            }
            ADBHelper.Tap(deviceID, 582.9, 1967.3);
            //KAutoHelper.ADBHelper.Tap(deviceID, 582.9, 1967.3);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SwipeToLeft();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // now add the following C# line in the code page  
            //var imgsource = new Bitmap(@"E:\project\auto\Code\LOMAuto\bin\Debug\1.png");

            //var ocrtext = string.Empty;
            //using (var engine = new TesseractEngine(@"E:\project\auto\Code\LOMAuto\bin\Debug\tessdata", "eng", EngineMode.Default))
            //{
            //    using (var img = PixConverter.ToPix(imgsource))
            //    {
            //        using (var page = engine.Process(img))
            //        {
            //            ocrtext = page.GetText();
            //        }
            //    }
            //}

            //using (var image = new Image<Bgr, byte>(Path.GetFullPath(@"E:\project\auto\Code\LOMAuto\bin\Debug\1.png")))
            //{
            //    using (var tess = new Tesseract(@"E:\project\auto\Code\LOMAuto\bin\Debug\tessdata", "eng", OcrEngineMode.Default))
            //    {
            //        tess.Recognize();
            //        var text = tess.GetUTF8Text().ToString();
            //    }
            //}

            var imgsource = new Bitmap(@"E:\project\auto\Code\LOMAuto\bin\Debug\2.png");
            //string a = KAutoHelper.Get_Text_From_Image.Get_Text(imgsource);

        }

        private void btnChallenge_Click(object sender, EventArgs e)
        {
            string deviceID = null;
            var listDevice = KAutoHelper.ADBHelper.GetDevices();
            if (listDevice != null && listDevice.Count > 0)
            {
                deviceID = listDevice.First();
            }
            ADBHelper.Tap(deviceID, 525.7, 1509.2);
        }

        private void btnAddPet_Click(object sender, EventArgs e)
        {
            string deviceID = null;
            var listDevice = KAutoHelper.ADBHelper.GetDevices();
            if (listDevice != null && listDevice.Count > 0)
            {
                deviceID = listDevice.First();
            }
            ADBHelper.Tap(deviceID, 235.8, 1154.9);
        }

        private void btnPlayEnter_Click(object sender, EventArgs e)
        {
            string deviceID = null;
            var listDevice = KAutoHelper.ADBHelper.GetDevices();
            if (listDevice != null && listDevice.Count > 0)
            {
                deviceID = listDevice.First();
            }
            ADBHelper.Tap(deviceID, 686.7, 2074.7);
        }

        private void btnAutoBattle_Click(object sender, EventArgs e)
        {
            string deviceID = null;
            var listDevice = KAutoHelper.ADBHelper.GetDevices();
            if (listDevice != null && listDevice.Count > 0)
            {
                deviceID = listDevice.First();
            }
            ADBHelper.Tap(deviceID, 944.4, 596.5);
        }

        private void btnAuto11_Click(object sender, EventArgs e)
        {
            bool lv6 = cbLV6.Checked;
            int i = 0;
            bool isRunning = false;
            bool isWaiting = false;
            Task.Run(() =>
            {
                do
                {
                    if (!isRunning)
                    {
                        isRunning = true;
                        //button2_Click(sender, e);
                        btnStartMap1_2_Click(sender, e);
                        Thread.Sleep(3000);

                        btnChallenge_Click(sender, e);
                        Thread.Sleep(3000);

                        btnPlayEnter_Click(sender, e);
                        Thread.Sleep(3000);



                    }
                } while (true);
            });
            Task.Run(() =>
            {
                do
                {
                    if ((i >= 200 && !isWaiting && !lv6) || (lv6 && i >= 180 && !isWaiting))
                    {
                        isWaiting = true;
                        btnPlayEnter_Click(sender, e);
                        Thread.Sleep(3000);
                        if (lv6)
                        {
                            btnBackButton_Click(sender, e);
                            Thread.Sleep(1000);
                            SwipeToLeft();
                            Thread.Sleep(1000);
                            btnMap1Enter_Click(sender, e);
                        }
                        isRunning = false;
                        isWaiting = false;
                        i = 0;
                    }
                } while (true);
            });

            Thread t = new Thread(() =>
            {
                do
                {
                    //lblSeconds.Text = i.ToString();
                    Console.WriteLine(i.ToString());
                    Thread.Sleep(1000);
                    i++;
                } while (true);
            });
            t.Start();

            //Task.Run(() =>
            //{
            //    do
            //    {
            //        lblSeconds.Text = i.ToString();
            //        Thread.Sleep(1000);
            //        i++;
            //    } while (true);
            //});

            MessageBox.Show("action");
        }

        private void btnStartMap1_2_Click(object sender, EventArgs e)
        {
            string deviceID = null;
            var listDevice = KAutoHelper.ADBHelper.GetDevices();
            if (listDevice != null && listDevice.Count > 0)
            {
                deviceID = listDevice.First();
            }
            ADBHelper.Tap(deviceID, 565.1, 610.9);
        }

        private void btnBackButton_Click(object sender, EventArgs e)
        {
            string deviceID = null;
            var listDevice = KAutoHelper.ADBHelper.GetDevices();
            if (listDevice != null && listDevice.Count > 0)
            {
                deviceID = listDevice.First();
            }
            ADBHelper.Tap(deviceID, 106.9, 2124.8);
        }

        private void btnMap1Enter_Click(object sender, EventArgs e)
        {
            string deviceID = null;
            var listDevice = KAutoHelper.ADBHelper.GetDevices();
            if (listDevice != null && listDevice.Count > 0)
            {
                deviceID = listDevice.First();
            }
            ADBHelper.Tap(deviceID, 550.7, 1766.9);
        }

        private void btnNewControl_Click(object sender, EventArgs e)
        {
            var f = new Form2();
            f.Show();
        }

        private void btnSwipeUp_Click(object sender, EventArgs e)
        {
            string deviceID = null;
            var listDevice = KAutoHelper.ADBHelper.GetDevices();
            if (listDevice != null && listDevice.Count > 0)
            {
                deviceID = listDevice.First();
            }
            ADBHelper.Swipe(deviceID, 529, 523, 529, 1819);
            //ADBHelper.Swipe(deviceID, 529, 1723, 529, 1619);
        }

        private void btnSwipeDown_Click(object sender, EventArgs e)
        {
            string deviceID = null;
            var listDevice = KAutoHelper.ADBHelper.GetDevices();
            if (listDevice != null && listDevice.Count > 0)
            {
                deviceID = listDevice.First();
            }
            ADBHelper.Swipe(deviceID, 529, 1819, 529, 523);
        }
    }
}
