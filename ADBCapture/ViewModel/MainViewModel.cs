using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using KAutoHelper;

namespace ADBCapture.ViewModel
{
    // Token: 0x02000009 RID: 9
    public class MainViewModel : BaseViewModel
    {
        // Token: 0x17000009 RID: 9
        // (get) Token: 0x06000032 RID: 50 RVA: 0x00002B29 File Offset: 0x00000D29
        // (set) Token: 0x06000033 RID: 51 RVA: 0x00002B31 File Offset: 0x00000D31
        public MainWindow MainWindow
        {
            get
            {
                return this._MainWindow;
            }
            set
            {
                this._MainWindow = value;
                this.OnPropertyChanged("MainWindow");
            }
        }

        // Token: 0x1700000A RID: 10
        // (get) Token: 0x06000034 RID: 52 RVA: 0x00002B47 File Offset: 0x00000D47
        // (set) Token: 0x06000035 RID: 53 RVA: 0x00002B4F File Offset: 0x00000D4F
        public int ImageWidth
        {
            get
            {
                return this._ImageWidth;
            }
            set
            {
                this._ImageWidth = value;
                this.OnPropertyChanged("ImageWidth");
            }
        }

        // Token: 0x1700000B RID: 11
        // (get) Token: 0x06000036 RID: 54 RVA: 0x00002B65 File Offset: 0x00000D65
        // (set) Token: 0x06000037 RID: 55 RVA: 0x00002B6D File Offset: 0x00000D6D
        public string SelectedDevice
        {
            get
            {
                return this._SelectedDevice;
            }
            set
            {
                this._SelectedDevice = value;
                this.OnPropertyChanged("SelectedDevice");
            }
        }

        // Token: 0x1700000C RID: 12
        // (get) Token: 0x06000038 RID: 56 RVA: 0x00002B83 File Offset: 0x00000D83
        // (set) Token: 0x06000039 RID: 57 RVA: 0x00002B8B File Offset: 0x00000D8B
        public List<string> DevicesList
        {
            get
            {
                return this._DevicesList;
            }
            set
            {
                this._DevicesList = value;
                this.OnPropertyChanged("DevicesList");
            }
        }

        // Token: 0x1700000D RID: 13
        // (get) Token: 0x0600003A RID: 58 RVA: 0x00002BA1 File Offset: 0x00000DA1
        // (set) Token: 0x0600003B RID: 59 RVA: 0x00002BA9 File Offset: 0x00000DA9
        public int ImageHeight
        {
            get
            {
                return this._ImageHeight;
            }
            set
            {
                this._ImageHeight = value;
                this.OnPropertyChanged("ImageHeight");
            }
        }

        // Token: 0x1700000E RID: 14
        // (get) Token: 0x0600003C RID: 60 RVA: 0x00002BBF File Offset: 0x00000DBF
        // (set) Token: 0x0600003D RID: 61 RVA: 0x00002BC7 File Offset: 0x00000DC7
        public double X
        {
            get
            {
                return this._X;
            }
            set
            {
                this._X = value;
                this.OnPropertyChanged("X");
                this.UpdateCodePostion();
            }
        }

        // Token: 0x1700000F RID: 15
        // (get) Token: 0x0600003E RID: 62 RVA: 0x00002BE4 File Offset: 0x00000DE4
        // (set) Token: 0x0600003F RID: 63 RVA: 0x00002BEC File Offset: 0x00000DEC
        public double Y
        {
            get
            {
                return this._Y;
            }
            set
            {
                this._Y = value;
                this.OnPropertyChanged("Y");
                this.UpdateCodePostion();
            }
        }

        // Token: 0x17000010 RID: 16
        // (get) Token: 0x06000040 RID: 64 RVA: 0x00002C09 File Offset: 0x00000E09
        // (set) Token: 0x06000041 RID: 65 RVA: 0x00002C11 File Offset: 0x00000E11
        public double XPercent
        {
            get
            {
                return this._XPercent;
            }
            set
            {
                this._XPercent = value;
                this.OnPropertyChanged("XPercent");
                this.UpdateCodePercent();
            }
        }

        // Token: 0x17000011 RID: 17
        // (get) Token: 0x06000042 RID: 66 RVA: 0x00002C2E File Offset: 0x00000E2E
        // (set) Token: 0x06000043 RID: 67 RVA: 0x00002C36 File Offset: 0x00000E36
        public double YPercent
        {
            get
            {
                return this._YPercent;
            }
            set
            {
                this._YPercent = value;
                this.OnPropertyChanged("YPercent");
                this.UpdateCodePercent();
            }
        }

        // Token: 0x17000012 RID: 18
        // (get) Token: 0x06000044 RID: 68 RVA: 0x00002C53 File Offset: 0x00000E53
        // (set) Token: 0x06000045 RID: 69 RVA: 0x00002C5B File Offset: 0x00000E5B
        public double GridCropImageHeight
        {
            get
            {
                return this._GridCropImageHeight;
            }
            set
            {
                this._GridCropImageHeight = value;
                this.OnPropertyChanged("GridCropImageHeight");
            }
        }

        // Token: 0x17000013 RID: 19
        // (get) Token: 0x06000046 RID: 70 RVA: 0x00002C71 File Offset: 0x00000E71
        // (set) Token: 0x06000047 RID: 71 RVA: 0x00002C79 File Offset: 0x00000E79
        public double GridCropImageWidth
        {
            get
            {
                return this._GridCropImageWidth;
            }
            set
            {
                this._GridCropImageWidth = value;
                this.OnPropertyChanged("GridCropImageWidth");
            }
        }

        // Token: 0x17000014 RID: 20
        // (get) Token: 0x06000048 RID: 72 RVA: 0x00002C8F File Offset: 0x00000E8F
        // (set) Token: 0x06000049 RID: 73 RVA: 0x00002C97 File Offset: 0x00000E97
        public string ClickPositionCode
        {
            get
            {
                return this._ClickPositionCode;
            }
            set
            {
                this._ClickPositionCode = value;
                this.OnPropertyChanged("ClickPositionCode");
            }
        }

        // Token: 0x17000015 RID: 21
        // (get) Token: 0x0600004A RID: 74 RVA: 0x00002CAD File Offset: 0x00000EAD
        // (set) Token: 0x0600004B RID: 75 RVA: 0x00002CB5 File Offset: 0x00000EB5
        public string ClickPercentCode
        {
            get
            {
                return this._ClickPercentCode;
            }
            set
            {
                this._ClickPercentCode = value;
                this.OnPropertyChanged("ClickPercentCode");
            }
        }

        // Token: 0x17000016 RID: 22
        // (get) Token: 0x0600004C RID: 76 RVA: 0x00002CCB File Offset: 0x00000ECB
        // (set) Token: 0x0600004D RID: 77 RVA: 0x00002CD3 File Offset: 0x00000ED3
        public string FirstDeviceIdCode
        {
            get
            {
                return this._FirstDeviceIdCode;
            }
            set
            {
                this._FirstDeviceIdCode = value;
                this.OnPropertyChanged("FirstDeviceIdCode");
            }
        }

        // Token: 0x17000017 RID: 23
        // (get) Token: 0x0600004E RID: 78 RVA: 0x00002CE9 File Offset: 0x00000EE9
        // (set) Token: 0x0600004F RID: 79 RVA: 0x00002CF1 File Offset: 0x00000EF1
        public BitmapSource BitmapSource
        {
            get
            {
                return this._BitmapSource;
            }
            set
            {
                this._BitmapSource = value;
                this.OnPropertyChanged("BitmapSource");
            }
        }

        // Token: 0x17000018 RID: 24
        // (get) Token: 0x06000050 RID: 80 RVA: 0x00002D07 File Offset: 0x00000F07
        // (set) Token: 0x06000051 RID: 81 RVA: 0x00002D10 File Offset: 0x00000F10
        public Bitmap ScreenCaptured
        {
            get
            {
                return this._ScreenCaptured;
            }
            set
            {
                this._ScreenCaptured = value;
                this.OnPropertyChanged("ScreenCaptured");
                bool flag = this._ScreenCaptured != null;
                if (flag)
                {
                    this.ImageWidth = this.ScreenCaptured.Width;
                    this.ImageHeight = this.ScreenCaptured.Height;
                }
                this.BitmapSource = BitmapConversion.BitmapToBitmapSource(this._ScreenCaptured);
            }
        }
        private string _txtListDevice;

        public string txtListDevice
        {
            get { return _txtListDevice; }
            set
            {
                if (value != _txtListDevice)
                {
                    _txtListDevice = value;
                    OnPropertyChanged("txtListDevice");
                }
            }
        }

        // Token: 0x17000019 RID: 25
        // (get) Token: 0x06000052 RID: 82 RVA: 0x00002D76 File Offset: 0x00000F76
        // (set) Token: 0x06000053 RID: 83 RVA: 0x00002D7E File Offset: 0x00000F7E
        public ICommand CaptureCMD { get; set; }
        public ICommand ReloadDevice { get; set; }

        // Token: 0x1700001A RID: 26
        // (get) Token: 0x06000054 RID: 84 RVA: 0x00002D87 File Offset: 0x00000F87
        // (set) Token: 0x06000055 RID: 85 RVA: 0x00002D8F File Offset: 0x00000F8F
        public ICommand CheckPositionCMD { get; set; }

        // Token: 0x1700001B RID: 27
        // (get) Token: 0x06000056 RID: 86 RVA: 0x00002D98 File Offset: 0x00000F98
        // (set) Token: 0x06000057 RID: 87 RVA: 0x00002DA0 File Offset: 0x00000FA0
        public ICommand ClickOnPositionCMD { get; set; }

        // Token: 0x1700001C RID: 28
        // (get) Token: 0x06000058 RID: 88 RVA: 0x00002DA9 File Offset: 0x00000FA9
        // (set) Token: 0x06000059 RID: 89 RVA: 0x00002DB1 File Offset: 0x00000FB1
        public ICommand ClickConnectNoxCMD { get; set; }

        // Token: 0x1700001D RID: 29
        // (get) Token: 0x0600005A RID: 90 RVA: 0x00002DBA File Offset: 0x00000FBA
        // (set) Token: 0x0600005B RID: 91 RVA: 0x00002DC2 File Offset: 0x00000FC2
        public ICommand ClickOnPercentCMD { get; set; }

        // Token: 0x1700001E RID: 30
        // (get) Token: 0x0600005C RID: 92 RVA: 0x00002DCB File Offset: 0x00000FCB
        // (set) Token: 0x0600005D RID: 93 RVA: 0x00002DD3 File Offset: 0x00000FD3
        public ICommand DragMoveCMD { get; set; }

        // Token: 0x1700001F RID: 31
        // (get) Token: 0x0600005E RID: 94 RVA: 0x00002DDC File Offset: 0x00000FDC
        // (set) Token: 0x0600005F RID: 95 RVA: 0x00002DE4 File Offset: 0x00000FE4
        public ICommand ClosedXCMD { get; set; }

        // Token: 0x17000020 RID: 32
        // (get) Token: 0x06000060 RID: 96 RVA: 0x00002DED File Offset: 0x00000FED
        // (set) Token: 0x06000061 RID: 97 RVA: 0x00002DF5 File Offset: 0x00000FF5
        public ICommand MinimizeCMD { get; set; }

        // Token: 0x17000021 RID: 33
        // (get) Token: 0x06000062 RID: 98 RVA: 0x00002DFE File Offset: 0x00000FFE
        // (set) Token: 0x06000063 RID: 99 RVA: 0x00002E06 File Offset: 0x00001006
        public ICommand CopyPercentCodeCMD { get; set; }

        // Token: 0x17000022 RID: 34
        // (get) Token: 0x06000064 RID: 100 RVA: 0x00002E0F File Offset: 0x0000100F
        // (set) Token: 0x06000065 RID: 101 RVA: 0x00002E17 File Offset: 0x00001017
        public ICommand CopyFirstDeviceIdCodeCMD { get; set; }

        // Token: 0x17000023 RID: 35
        // (get) Token: 0x06000066 RID: 102 RVA: 0x00002E20 File Offset: 0x00001020
        // (set) Token: 0x06000067 RID: 103 RVA: 0x00002E28 File Offset: 0x00001028
        public ICommand CopyPositionCodeCMD { get; set; }

        // Token: 0x17000024 RID: 36
        // (get) Token: 0x06000068 RID: 104 RVA: 0x00002E31 File Offset: 0x00001031
        // (set) Token: 0x06000069 RID: 105 RVA: 0x00002E39 File Offset: 0x00001039
        public ICommand ImgMainSizeChangedCMD { get; set; }

        // Token: 0x0600006A RID: 106 RVA: 0x00002E42 File Offset: 0x00001042
        public MainViewModel(MainWindow mainWindow)
        {
            this.MainWindow = mainWindow;
            this.FirstLoad();
            this.LoadCommands();
        }

        // Token: 0x0600006B RID: 107 RVA: 0x00002E62 File Offset: 0x00001062
        private void FirstLoad()
        {
            this.FirstDeviceIdCode = "string deviceID = null;\r\nvar listDevice = KAutoHelper.ADBHelper.GetDevices();\r\nif (listDevice != null && listDevice.Count > 0)\r\n{\r\n    deviceID = listDevice.First();\r\n}";
            this.LoadDevicesList();
        }

        // Token: 0x0600006C RID: 108 RVA: 0x00002E78 File Offset: 0x00001078
        private void LoadCommands()
        {
            this.CaptureCMD = new RelayCommand<object>((object pp) => true, delegate (object pp)
            {
                this.CaptureScreen(this.SelectedDevice);
            });

            this.ReloadDevice = new RelayCommand<object>((object pp) => true, delegate (object pp)
            {
                this.ScreenCaptured = null;
                this.LoadDevicesList();
            });
            this.CheckPositionCMD = new RelayCommand<FrameworkElement>((FrameworkElement pp) => true, delegate (FrameworkElement pp)
            {
                this.CheckPosition(pp);
            });
            this.ClickOnPositionCMD = new RelayCommand<object>((object pp) => true, delegate (object pp)
            {
                this.ClickOnPosition();
            });
            this.ClickConnectNoxCMD = new RelayCommand<object>((object pp) => true, delegate (object pp)
            {
                ADBHelper.ConnectNox(1);
            });
            this.ClickOnPercentCMD = new RelayCommand<object>((object pp) => true, delegate (object pp)
            {
                this.ClickOnPercent();
            });
            this.MinimizeCMD = new RelayCommand<object>((object p) => true, delegate (object p)
            {
                this.MainWindow.WindowState = WindowState.Minimized;
            });
            this.ClosedXCMD = new RelayCommand<object>((object p) => true, delegate (object p)
            {
                this.MainWindow.Close();
            });
            this.DragMoveCMD = new RelayCommand<object>((object p) => true, delegate (object p)
            {
                try
                {
                    this.MainWindow.DragMove();
                }
                catch
                {
                }
            });
            this.CopyPercentCodeCMD = new RelayCommand<object>((object p) => true, delegate (object p)
            {
                bool flag = this.ClickPercentCode == null;
                if (!flag)
                {
                    System.Windows.Clipboard.SetText(this.ClickPercentCode);
                }
            });
            this.CopyPositionCodeCMD = new RelayCommand<object>((object p) => true, delegate (object p)
            {
                bool flag = this.ClickPositionCode == null;
                if (!flag)
                {
                    System.Windows.Clipboard.SetText(this.ClickPositionCode);
                }
            });
            this.CopyFirstDeviceIdCodeCMD = new RelayCommand<object>((object p) => true, delegate (object p)
            {
                bool flag = this.FirstDeviceIdCode == null;
                if (!flag)
                {
                    System.Windows.Clipboard.SetText(this.FirstDeviceIdCode);
                }
            });
            this.ImgMainSizeChangedCMD = new RelayCommand<FrameworkElement>((FrameworkElement pp) => true, delegate (FrameworkElement pp)
            {
                System.Windows.Controls.Image img = pp as System.Windows.Controls.Image;
                this.GridCropImageHeight = img.ActualHeight;
                this.GridCropImageWidth = img.ActualWidth;
            });
        }

        // Token: 0x0600006D RID: 109 RVA: 0x0000312D File Offset: 0x0000132D
        public void LoadDevicesList()
        {
            this.DevicesList = ADBHelper.GetDevices();
            this.SelectedDevice = this.DevicesList.FirstOrDefault<string>();
            txtListDevice = String.Join("\r\n", this.DevicesList);
        }

        // Token: 0x0600006E RID: 110 RVA: 0x00003150 File Offset: 0x00001350
        public void CropImage(System.Drawing.Size size, System.Drawing.Point point)
        {
            double scale = (double)this.ImageWidth / this.MainWindow.imgMain.ActualWidth;
            double widthPercent = (double)size.Width * scale;
            double heightPercent = (double)size.Height * scale;
            double xPercent = (double)point.X * scale;
            double yPercent = (double)point.Y * scale;
            Bitmap imgCroped = CaptureHelper.CropImage(this.ScreenCaptured, new Rectangle((int)xPercent, (int)yPercent, (int)widthPercent, (int)heightPercent));
            Console.WriteLine("xPercent = {0},", xPercent);
            Console.WriteLine("yPercent = " + yPercent);
            Console.WriteLine("widthPercent = " + widthPercent);
            Console.WriteLine("heightPercent = " + heightPercent);

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "PNG image|*.png";
            bool flag = dialog.ShowDialog() == DialogResult.OK;
            if (flag)
            {
                imgCroped.Save(dialog.FileName);
            }
        }

        // Token: 0x0600006F RID: 111 RVA: 0x000031F4 File Offset: 0x000013F4
        private void UpdateCodePostion()
        {
            this.ClickPositionCode = string.Format("KAutoHelper.ADBHelper.Tap(deviceID, {0},{1});", this.X.ToString("F1"), this.Y.ToString("F1"));
        }

        // Token: 0x06000070 RID: 112 RVA: 0x0000323C File Offset: 0x0000143C
        private void UpdateCodePercent()
        {
            this.ClickPercentCode = string.Format("KAutoHelper.ADBHelper.TapByPercent(deviceID, {0},{1});", this.XPercent.ToString("F1"), this.YPercent.ToString("F1"));
        }

        // Token: 0x06000071 RID: 113 RVA: 0x00003284 File Offset: 0x00001484
        private void CaptureScreen(string deviceID = null)
        {
            bool flag = deviceID == null;
            if (flag)
            {
                this.LoadDevicesList();
                deviceID = this.SelectedDevice;
            }
            this.ScreenCaptured = ADBHelper.ScreenShoot(deviceID, false, "screenShoot.png");
        }

        // Token: 0x06000072 RID: 114 RVA: 0x000032C0 File Offset: 0x000014C0
        private void CheckPosition(FrameworkElement control)
        {
            System.Windows.Point mousePosition = Mouse.GetPosition(control);
            double controlWidth = control.ActualWidth;
            double controlHeight = control.ActualHeight;
            double scale = (double)this.ImageWidth / controlWidth;
            this.X = mousePosition.X * scale;
            this.Y = mousePosition.Y * scale;
            this.XPercent = this.X / (double)this.ImageWidth * 100.0;
            this.YPercent = this.Y / (double)this.ImageHeight * 100.0;
        }

        // Token: 0x06000073 RID: 115 RVA: 0x00003354 File Offset: 0x00001554
        private void ClickOnPercent()
        {
            bool flag = string.IsNullOrEmpty(this.SelectedDevice);
            if (flag)
            {
                this.LoadDevicesList();
            }
            try
            {
                ADBHelper.TapByPercent(this.SelectedDevice, this.XPercent, this.YPercent, 1);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }

        // Token: 0x06000074 RID: 116 RVA: 0x000033BC File Offset: 0x000015BC
        private void ClickOnPosition()
        {
            bool flag = string.IsNullOrEmpty(this.SelectedDevice);
            if (flag)
            {
                this.LoadDevicesList();
            }
            try
            {
                ADBHelper.Tap(this.SelectedDevice, (int)this.X, (int)this.Y, 1);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }

        // Token: 0x04000025 RID: 37
        private MainWindow _MainWindow;

        // Token: 0x04000026 RID: 38
        private int _ImageWidth;

        // Token: 0x04000027 RID: 39
        private string _SelectedDevice;

        // Token: 0x04000028 RID: 40
        private List<string> _DevicesList;

        // Token: 0x04000029 RID: 41
        private int _ImageHeight;

        // Token: 0x0400002A RID: 42
        private double _X;

        // Token: 0x0400002B RID: 43
        private double _Y;

        // Token: 0x0400002C RID: 44
        private double _XPercent;

        // Token: 0x0400002D RID: 45
        private double _YPercent;

        // Token: 0x0400002E RID: 46
        private double _GridCropImageHeight;

        // Token: 0x0400002F RID: 47
        private double _GridCropImageWidth;

        // Token: 0x04000030 RID: 48
        private string _ClickPositionCode;

        // Token: 0x04000031 RID: 49
        private string _ClickPercentCode;

        // Token: 0x04000032 RID: 50
        private string _FirstDeviceIdCode;

        // Token: 0x04000033 RID: 51
        private BitmapSource _BitmapSource;

        // Token: 0x04000034 RID: 52
        private Bitmap _ScreenCaptured;
    }
}
