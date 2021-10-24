using JadeLib;
using KAutoHelper;
using LOMAuto.EF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static LOMAuto.Utilities;

namespace LOMAuto
{
    public class GameObject
    {
        public T_LOM mainChar { set; get; }
        //private Character MainChar { set; get; }

        public string DeviceID { set; get; }

        //public LOMDBContext db;

        CropRectangle mapCrop;
        CropRectangle keyCrop;
        CropRectangle keyCrop1x;
        CropRectangle enterButtonCrop;

        //public bool IS_AUTO = true;

        //private CurrentMapStatus CurrentStatus;

        public GameObject(string deviceID)
        {
            DeviceID = deviceID;
            //db = new LOMDBContext();
            mainChar = new T_LOM();

            //MainChar = new Character();

            //lay map
            mapCrop = new CropRectangle()
            {
                xPercent = 254.108383787169,
                yPercent = 300.635270959468,
                widthPercent = 93.0537743445972,
                heightPercent = 89.4747830236512
            };

            //key < 0
            keyCrop = new CropRectangle()
            {
                xPercent = 382.952071341227,
                yPercent = 157.475618121626,
                widthPercent = 57.2638611351368,
                heightPercent = 46.5268871722986
            };

            //key >= 10
            keyCrop1x = new CropRectangle()
            {
                xPercent = 393.689045304065,
                yPercent = 153.89662680068,
                widthPercent = 60.8428524560828,
                heightPercent = 53.6848698141907
            };

            //button enter vao map
            enterButtonCrop = new CropRectangle()
            {
                xPercent = 400.847027945957,
                yPercent = 1732.23179933789,
                widthPercent = 254.108383787169,
                heightPercent = 93.0537743445972
            };
        }

        //private void GetNewGame()
        //{
        //    Task.Run(() =>
        //    {
        //        mainChar = db.T_LOM.Where(x => x.DEVICE_ID == DeviceID).FirstOrDefault();
        //        if (mainChar == null)
        //        {
        //            mainChar = new T_LOM();
        //            mainChar.DEVICE_ID = DeviceID;
        //            //map 1.1 = 1+0
        //            //map 1 cuối = 1-0
        //            //map 1 gần cuối = 1-1

        //            mainChar.TARGET_MAP = 1;
        //            mainChar.MINI_MAP = 0;
        //            db.T_LOM.Add(mainChar);
        //            db.SaveChanges();
        //        }
        //        //SetCharInfo();


        //        //List<Task> TaskList = new List<Task>();
        //        //TaskList.Add(new Task(()=> { SetCurrentMap(); }));
        //        //TaskList.Add(new Task(()=> { SetCurrentKey(); }));
        //        //TaskList.Add(new Task(()=> { SetCurrentStatus(); }));
        //        //Task.WaitAll(TaskList.ToArray());

        //        SetCurrentMap();
        //        SetCurrentKey();
        //        SetCurrentStatus();

        //        Console.WriteLine("vkl");

        //        //Task.Run(() => { SetCurrentMap(); });
        //        //Task.Run(() => { SetCurrentKey(); });
        //        //Task.Run(() => { SetCurrentStatus(); });

        //        Auto();

        //    });
        //}
        public Task GetNewGame()
        {
            //db = new LOMDBContext();
            mainChar = ClientUtils.GetObj<T_LOM>("select * from t_lom where DEVICE_ID = '" + DeviceID + "'");// db.T_LOM.Where(x => x.DEVICE_ID == DeviceID).FirstOrDefault();
            if (mainChar == null)
            {
                mainChar = new T_LOM();
                mainChar.DEVICE_ID = DeviceID;
                //map 1.1 = 1+0
                //map 1 cuối = 1-0
                //map 1 gần cuối = 1-1

                mainChar.TARGET_MAP = 1;
                mainChar.MINI_MAP = 0;
                mainChar.IS_AUTO = true;

                ClientUtils.Add<T_LOM>(mainChar, nameof(T_LOM), new List<string>() { "ID" });

                //db.T_LOM.Add(mainChar);
                //db.SaveChanges();
            }


            return Task.Run(() =>
            {
                UpdateChar();
                //mainChar.UPDATE_TIME = DateTime.Now;
                //db.SaveChanges();
                ClientUtils.ExecuteSQLAsync("update t_lom set update_time = getdate() where Device_ID = '" + DeviceID + "'");

            });

            //await UpdateCharInfo();
            //Auto();
        }
        //public Task UpdateCharInfo()
        //{
        //    //db = new LOMDBContext();
        //    mainChar = ClientUtils.GetObj<T_LOM>("select * from t_lom where DEVICE_ID = '" + DeviceID + "'");// db.T_LOM.Where(x => x.DEVICE_ID == DeviceID).FirstOrDefault();
        //    if (mainChar == null)
        //    {
        //        mainChar = new T_LOM();
        //        mainChar.DEVICE_ID = DeviceID;
        //        //map 1.1 = 1+0
        //        //map 1 cuối = 1-0
        //        //map 1 gần cuối = 1-1

        //        mainChar.TARGET_MAP = 1;
        //        mainChar.MINI_MAP = 0;

        //        ClientUtils.Add<T_LOM>(mainChar, nameof(T_LOM), new List<string>() { "ID" });

        //        //db.T_LOM.Add(mainChar);
        //        //db.SaveChanges();
        //    }


        //    return Task.Run(() =>
        //    {
        //        SetCurrentMap();
        //        SetCurrentKey();
        //        SetCurrentStatus();
        //        //mainChar.UPDATE_TIME = DateTime.Now;
        //        //db.SaveChanges();
        //        ClientUtils.ExecuteSQLAsync("update t_lom set update_time = getdate() where Device_ID = '" + DeviceID + "'");

        //    });
        //}

        private void UpdateChar()
        {
            SetCurrentMap();
            SetCurrentKey();
            SetCurrentStatus();
        }

        public void Auto()
        {
            //db = new LOMDBContext();
            int time1 = 0;
            bool isRunning = false;
            Task.Run(() =>
            {
                do
                {
                    if (!isRunning)
                    {
                        isRunning = true;

                        if (mainChar.CURRENT_MAP != mainChar.TARGET_MAP && mainChar.CURRENT_STATUS != Utilities.CurrentMapStatus.ENTER_TO_PLAY.ToString())
                        {
                            BackMap();
                            mainChar.CURRENT_STATUS = Utilities.CurrentMapStatus.ENTER_TO_PLAY.ToString();
                            ClientUtils.ExecuteSQLAsync("update t_lom set CURRENT_STATUS = '" + mainChar.CURRENT_STATUS + "' where Device_ID = '" + DeviceID + "'");
                            Thread.Sleep(3000);
                        }
                        if (mainChar.CURRENT_MAP < mainChar.TARGET_MAP)
                        {
                            for (int i = 0; i < mainChar.TARGET_MAP - mainChar.CURRENT_MAP; i++)
                            {
                                SwipeToRight();
                                Thread.Sleep(3000);
                            }
                        }
                        else if (mainChar.CURRENT_MAP > mainChar.TARGET_MAP)
                        {
                            for (int i = 0; i < mainChar.CURRENT_MAP - mainChar.TARGET_MAP; i++)
                            {
                                SwipeToLeft();
                                Thread.Sleep(3000);
                            }
                        }



                        if (mainChar.CURRENT_STATUS == Utilities.CurrentMapStatus.ENTER_TO_PLAY.ToString())
                        {
                            EnterClick();
                            Thread.Sleep(3000);
                        }

                        if (mainChar.MINI_MAP >= 0 && mainChar.MINI_MAP < 5)
                            SwipeToDown();
                        else
                            SwipeToUp();

                        Thread.Sleep(3000);

                        MiniMapClick(mainChar.MINI_MAP);
                        Thread.Sleep(3000);
                        btnChallenge_Click();
                        Thread.Sleep(3000);
                        btnPlayEnter_Click();

                        //if (mainChar.CURRENT_MAP != mainChar.TARGET_MAP)
                        //{
                        //    ClientUtils.ExecuteSQLAsync("update t_lom set CURRENT_MAP = TARGET_MAP where Device_ID = '" + DeviceID + "'");
                        //}
                    }
                } while (mainChar.IS_AUTO);

            });
            Task.Run(() =>
            {
                do
                {
                    if (time1 >= 250)
                    {
                        btnPlayEnter_Click();
                        Thread.Sleep(3000);

                        UpdateChar();

                        if (mainChar.CURRENT_KEY == 0)
                        {
                            //update level
                            ClientUtils.WriteLog(this.DeviceID + "#Key=" + mainChar.CURRENT_KEY);
                            mainChar.IS_AUTO = false;
                            ClientUtils.ExecuteSQLAsync("update t_lom set IS_AUTO = '0' where device_id='" + DeviceID + "'");
                            break;
                        }

                        Thread.Sleep(3000);
                        isRunning = false;
                        time1 = 0;
                    }
                } while (mainChar.IS_AUTO);
            });
            Thread t = new Thread(() =>
            {
                do
                {
                    Console.WriteLine(time1.ToString());
                    Thread.Sleep(1000);
                    time1++;
                } while (mainChar.IS_AUTO);
            });
            t.Start();

            Console.WriteLine(DeviceID + " Finish");
        }
        private void btnPlayEnter_Click()
        {
            ADBHelper.Tap(this.DeviceID, 686.7, 2074.7);
        }
        private void btnChallenge_Click()
        {
            ADBHelper.Tap(this.DeviceID, 525.7, 1509.2);
        }
        private void MiniMapClick(int miniMap)
        {
            double yMiniMap = 1967.3;
            double scale = 280;
            if (miniMap > 9)
            {
                miniMap = -1 * (miniMap - 999);
                yMiniMap = 539;
                scale = scale * -1;
            }

            ADBHelper.Tap(this.DeviceID, 582.9, yMiniMap - scale * miniMap);
        }
        private void EnterClick()
        {
            ADBHelper.Tap(this.DeviceID, 550.7, 1766.9);
        }
        private void BackMap()
        {
            ADBHelper.Tap(this.DeviceID, 106.9, 2124.8);
        }
        private void SwipeToRight()
        {
            ADBHelper.Swipe(this.DeviceID, 895, 700, 235, 1322);
        }
        private void SwipeToLeft()
        {
            ADBHelper.Swipe(this.DeviceID, 235, 1322, 895, 700);
        }
        public void SwipeToUp()
        {
            ADBHelper.Swipe(this.DeviceID, 529, 523, 529, 1819);
        }
        public void SwipeToDown()
        {
            ADBHelper.Swipe(this.DeviceID, 529, 1819, 529, 523);
        }
        public void SetCharInfo()
        {
            Task.Run(() => { SetCurrentMap(); });
            Task.Run(() => { SetCurrentKey(); });
            Task.Run(() => { SetCurrentStatus(); });

        }
        public void SetCurrentMap()
        {
            //db = new LOMDBContext();
            //Bitmap bmChar = new Bitmap(@"E:\project\auto\Code\ADBCapture\bin\Debug\2.png");
            Bitmap bm = KAutoHelper.ADBHelper.ScreenShoot(DeviceID, false, "char.png");
            //Bitmap bmLevel = CaptureHelper.CropImage(bmChar, new Rectangle((int)xPercent, (int)yPercent, (int)widthPercent, (int)heightPercent));
            Bitmap bmCrop = LomCropImg.Crop(bm, mapCrop);

            string text = DetectImg.OrcHelper.GetTextFromImg(bmCrop).Replace("\n\n", "");
            //MainChar.CurrentMap = int.Parse(text);
            mainChar.CURRENT_MAP = int.Parse(text);
            //db.SaveChanges();
            ClientUtils.ExecuteSQLAsync("update t_lom set CURRENT_MAP = " + mainChar.CURRENT_MAP + " where Device_ID = '" + DeviceID + "'");
            Console.WriteLine("map:" + text);
        }
        public void SetCurrentKey()
        {
            //db = new LOMDBContext();
            Bitmap bm = KAutoHelper.ADBHelper.ScreenShoot(DeviceID, false, "key.png");
            //Bitmap bmCrop = CaptureHelper.CropImage(bm, new Rectangle((int)xPercent, (int)yPercent, (int)widthPercent, (int)heightPercent));
            Bitmap bmCrop = LomCropImg.Crop(bm, keyCrop);
            //bmCrop.Save("key" + DeviceID + ".png");
            string text = DetectImg.OrcHelper.GetTextFromImg(bmCrop).Replace("\n\n", "");

            int currentKey;
            bool check = int.TryParse(text, out currentKey);
            if (!check)
            {
                Bitmap bm1 = KAutoHelper.ADBHelper.ScreenShoot(DeviceID, false, "key.png");
                //Bitmap bmCrop = CaptureHelper.CropImage(bm, new Rectangle((int)xPercent, (int)yPercent, (int)widthPercent, (int)heightPercent));
                Bitmap bmCrop1 = LomCropImg.Crop(bm1, keyCrop1x);
                //bmCrop1.Save("key" + DeviceID + ".png");
                text = DetectImg.OrcHelper.GetTextFromImg(bmCrop1).Replace("\n\n", "").Replace("O", "0");
                currentKey = int.Parse(text);
            }
            mainChar.CURRENT_KEY = currentKey;
            //db.SaveChanges();

            ClientUtils.ExecuteSQLAsync("update t_lom set CURRENT_KEY = " + mainChar.CURRENT_KEY + " where Device_ID = '" + DeviceID + "'");
            Console.WriteLine("key:" + text);
        }

        public void SetCurrentStatus()
        {
            //db = new LOMDBContext();
            Bitmap bm = KAutoHelper.ADBHelper.ScreenShoot(DeviceID, false, "enter.png");
            Bitmap bmCrop = LomCropImg.Crop(bm, enterButtonCrop);
            string text = DetectImg.OrcHelper.GetTextFromImg(bmCrop).Replace("\n\n", "");
            Console.WriteLine("current: " + text);
            if (text.ToUpper().Contains("ENTER"))
            {
                mainChar.CURRENT_STATUS = Utilities.CurrentMapStatus.ENTER_TO_PLAY.ToString();
            }
            else
                mainChar.CURRENT_STATUS = Utilities.CurrentMapStatus.UNKNOW.ToString();
            ClientUtils.ExecuteSQLAsync("update t_lom set CURRENT_STATUS = '" + mainChar.CURRENT_STATUS + "' where Device_ID = '" + DeviceID + "'");
        }
    }
}
