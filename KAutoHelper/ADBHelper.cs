using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace KAutoHelper
{
	// Token: 0x02000002 RID: 2
	public class ADBHelper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static string SetADBFolderPath(string folderPath)
		{
			ADBHelper.ADB_FOLDER_PATH = folderPath;
			ADBHelper.ADB_PATH = folderPath + "\\adb.exe";
			bool flag = !File.Exists(ADBHelper.ADB_PATH);
			string result;
			if (flag)
			{
				result = "ADB Path not Exits!!!";
			}
			else
			{
				result = "OK";
			}
			return result;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002098 File Offset: 0x00000298
		public void SetTextFromClipboard(string deviceID, string text)
		{
			string[] array = text.Split(new string[]
			{
				"\r\n"
			}, StringSplitOptions.None);
			int num = 0;
			foreach (string text2 in array)
			{
				string text3 = ADBHelper.ExecuteCMDBat(deviceID, string.Concat(new string[]
				{
					"adb -s ",
					deviceID,
					" shell am broadcast -a clipper.set -e text \"\\\"",
					text2,
					"\\\"\""
				}));
				ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell input keyevent 279");
				num++;
				bool flag = num < array.Length;
				if (flag)
				{
					ADBHelper.Key(deviceID, ADBKeyEvent.KEYCODE_ENTER);
				}
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000213C File Offset: 0x0000033C
		private void Note(string deviceID)
		{
			ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell am force-stop com.zing.zalo");
			string text = ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell rm -f /sdcard/Pictures/Images/*");
			text = ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell mkdir /sdcard/Pictures/Images");
			DirectoryInfo directoryInfo = new DirectoryInfo("C:\\images");
			IEnumerable<string> enumerable = from x in directoryInfo.GetFiles()
			select x.FullName;
			foreach (string text2 in enumerable)
			{
				ADBHelper.ExecuteCMD(string.Concat(new string[]
				{
					"adb -s ",
					deviceID,
					" push ",
					text2,
					" sdcard/Pictures/Images"
				}));
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002230 File Offset: 0x00000430
		public static string ExecuteCMD(string cmdCommand)
		{
			string result;
			try
			{
				Process process = new Process();
				process.StartInfo = new ProcessStartInfo
				{
					WorkingDirectory = ADBHelper.ADB_FOLDER_PATH,
					FileName = "cmd.exe",
					CreateNoWindow = true,
					UseShellExecute = false,
					WindowStyle = ProcessWindowStyle.Hidden,
					RedirectStandardInput = true,
					RedirectStandardOutput = true,
					Verb = "runas"
				};
				process.Start();
				process.StandardInput.WriteLine(cmdCommand);
				process.StandardInput.Flush();
				process.StandardInput.Close();
				process.WaitForExit();
				string text = process.StandardOutput.ReadToEnd();
				result = text;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000022FC File Offset: 0x000004FC
		public static string ExecuteCMDBat(string deviceID, string cmdCommand)
		{
			string result;
			try
			{
				string text = "bat_" + deviceID + ".bat";
				File.WriteAllText(text, cmdCommand);
				Process process = new Process();
				process.StartInfo = new ProcessStartInfo
				{
					WorkingDirectory = ADBHelper.ADB_FOLDER_PATH,
					FileName = text,
					CreateNoWindow = true,
					UseShellExecute = false,
					WindowStyle = ProcessWindowStyle.Hidden,
					RedirectStandardInput = true,
					RedirectStandardOutput = true,
					Verb = "runas"
				};
				process.Start();
				process.StandardInput.WriteLine(cmdCommand);
				process.StandardInput.Flush();
				process.StandardInput.Close();
				process.WaitForExit();
				string text2 = process.StandardOutput.ReadToEnd();
				result = text2;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000023E0 File Offset: 0x000005E0
		public static List<string> GetDevices()
		{
			List<string> list = new List<string>();
			string input = ADBHelper.ExecuteCMD("adb devices");
			string pattern = "(?<=List of devices attached)([^\\n]*\\n+)+";
			MatchCollection matchCollection = Regex.Matches(input, pattern, RegexOptions.Singleline);
			bool flag = matchCollection.Count > 0;
			if (flag)
			{
				string value = matchCollection[0].Groups[0].Value;
				string[] array = Regex.Split(value, "\r\n");
				string[] array2 = array;
				int i = 0;
				while (i < array2.Length)
				{
					string text = array2[i];
					bool flag2 = !string.IsNullOrEmpty(text) && text != " ";
					if (flag2)
					{
						string[] array3 = text.Trim().Split(new char[]
						{
							'\t'
						});
						string text2 = array3[0];
						try
						{
							string a = array3[1];
							bool flag3 = a != "device";
							if (flag3)
							{
								goto IL_EA;
							}
						}
						catch
						{
						}
						list.Add(text2.Trim());
					}
					IL_EA:
					i++;
					continue;
					goto IL_EA;
				}
			}
			return list;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002500 File Offset: 0x00000700
		public static string GetDeviceName(string deviceID)
		{
			string result = "";
			string cmdCommand = "";
			string text = ADBHelper.ExecuteCMD(cmdCommand);
			return result;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002528 File Offset: 0x00000728
		public static void TapByPercent(string deviceID, double x, double y, int count = 1)
		{
			Point screenResolution = ADBHelper.GetScreenResolution(deviceID);
			int num = (int)(x * ((double)screenResolution.X * 1.0 / 100.0));
			int num2 = (int)(y * ((double)screenResolution.Y * 1.0 / 100.0));
			string text = string.Format(ADBHelper.TAP_DEVICES, deviceID, num, num2);
			for (int i = 1; i < count; i++)
			{
				text = text + " && " + string.Format(ADBHelper.TAP_DEVICES, deviceID, x, y);
			}
			string text2 = ADBHelper.ExecuteCMD(text);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000025D8 File Offset: 0x000007D8
		public static void Tap(string deviceID, int x, int y, int count = 1)
		{
			string text = string.Format(ADBHelper.TAP_DEVICES, deviceID, x, y);
			for (int i = 1; i < count; i++)
			{
				text = text + " && " + string.Format(ADBHelper.TAP_DEVICES, deviceID, x, y);
			}
			string text2 = ADBHelper.ExecuteCMD(text);
		}
		public static void Tap(string deviceID, double x, double y, int count = 1)
		{
			string text = string.Format(ADBHelper.TAP_DEVICES, deviceID, x, y);
			for (int i = 1; i < count; i++)
			{
				text = text + " && " + string.Format(ADBHelper.TAP_DEVICES, deviceID, x, y);
			}
			string text2 = ADBHelper.ExecuteCMD(text);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000263C File Offset: 0x0000083C
		public static void Key(string deviceID, ADBKeyEvent key)
		{
			string cmdCommand = string.Format(ADBHelper.KEY_DEVICES, deviceID, key);
			string text = ADBHelper.ExecuteCMD(cmdCommand);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002664 File Offset: 0x00000864
		public static void InputText(string deviceID, string text)
		{
			string cmdCommand = string.Format(ADBHelper.INPUT_TEXT_DEVICES, deviceID, text.Replace(" ", "%s").Replace("&", "\\&").Replace("<", "\\<").Replace(">", "\\>").Replace("?", "\\?").Replace(":", "\\:").Replace("{", "\\{").Replace("}", "\\}").Replace("[", "\\[").Replace("]", "\\]").Replace("|", "\\|"));
			string text2 = ADBHelper.ExecuteCMD(cmdCommand);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000272C File Offset: 0x0000092C
		public static void SwipeByPercent(string deviceID, double x1, double y1, double x2, double y2, int duration = 100)
		{
			Point screenResolution = ADBHelper.GetScreenResolution(deviceID);
			int num = (int)(x1 * ((double)screenResolution.X * 1.0 / 100.0));
			int num2 = (int)(y1 * ((double)screenResolution.Y * 1.0 / 100.0));
			int num3 = (int)(x2 * ((double)screenResolution.X * 1.0 / 100.0));
			int num4 = (int)(y2 * ((double)screenResolution.Y * 1.0 / 100.0));
			string cmdCommand = string.Format(ADBHelper.SWIPE_DEVICES, new object[]
			{
				deviceID,
				num,
				num2,
				num3,
				num4,
				duration
			});
			string text = ADBHelper.ExecuteCMD(cmdCommand);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002814 File Offset: 0x00000A14
		public static void Swipe(string deviceID, int x1, int y1, int x2, int y2, int duration = 100)
		{
			string cmdCommand = string.Format(ADBHelper.SWIPE_DEVICES, new object[]
			{
				deviceID,
				x1,
				y1,
				x2,
				y2,
				duration
			});
			string text = ADBHelper.ExecuteCMD(cmdCommand);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002870 File Offset: 0x00000A70
		public static void LongPress(string deviceID, int x, int y, int duration = 100)
		{
			string cmdCommand = string.Format(ADBHelper.SWIPE_DEVICES, new object[]
			{
				deviceID,
				x,
				y,
				x,
				y,
				duration
			});
			string text = ADBHelper.ExecuteCMD(cmdCommand);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000028C8 File Offset: 0x00000AC8
		public static Point GetScreenResolution(string deviceID)
		{
			string cmdCommand = string.Format(ADBHelper.GET_SCREEN_RESOLUTION, deviceID);
			string text = ADBHelper.ExecuteCMD(cmdCommand);
			text = text.Substring(text.IndexOf("- "));
			text = text.Substring(text.IndexOf(' '), text.IndexOf(')') - text.IndexOf(' '));
			string[] array = text.Split(new char[]
			{
				','
			});
			int x = Convert.ToInt32(array[0].Trim());
			int y = Convert.ToInt32(array[1].Trim());
			return new Point(x, y);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002958 File Offset: 0x00000B58
		public static Bitmap ScreenShoot(string deviceID = null, bool isDeleteImageAfterCapture = true, string fileName = "screenShoot.png")
		{
			bool flag = string.IsNullOrEmpty(deviceID);
			bool flag2 = flag;
			if (flag2)
			{
				List<string> devices = ADBHelper.GetDevices();
				bool flag3 = devices != null && devices.Count > 0;
				bool flag4 = !flag3;
				if (flag4)
				{
					return null;
				}
				deviceID = devices.First<string>();
			}
			string str = deviceID;
			try
			{
				str = deviceID.Split(new char[]
				{
					':'
				})[1];
			}
			catch
			{
			}
			string text = Path.GetFileNameWithoutExtension(fileName) + str + Path.GetExtension(fileName);
			for (;;)
			{
				bool flag5 = File.Exists(text);
				bool flag6 = !flag5;
				if (flag6)
				{
					break;
				}
				try
				{
					File.Delete(text);
					break;
				}
				catch (Exception ex)
				{
					break;
				}
			}
			string filename = Directory.GetCurrentDirectory() + "\\" + text;
			string text2 = Directory.GetCurrentDirectory().Replace("\\\\", "\\");
			text2 = "\"" + text2 + "\"";
			string cmdCommand = string.Format("adb -s {0} shell screencap -p \"{1}\"", deviceID, "/sdcard/" + text);
			string cmdCommand2 = string.Format(string.Concat(new string[]
			{
				"adb -s ",
				deviceID,
				" pull /sdcard/",
				text,
				" ",
				text2
			}), new object[0]);
			string text3 = ADBHelper.ExecuteCMD(cmdCommand);
			string text4 = ADBHelper.ExecuteCMD(cmdCommand2);
			Bitmap result = null;
			try
			{
				using (Bitmap bitmap = new Bitmap(filename))
				{
					result = new Bitmap(bitmap);
				}
			}
			catch
			{
			}
			if (isDeleteImageAfterCapture)
			{
				try
				{
					File.Delete(text);
				}
				catch
				{
				}
				try
				{
					string cmdCommand3 = string.Format(string.Concat(new string[]
					{
						"adb -s ",
						deviceID,
						" shell \"rm /sdcard/",
						text,
						"\""
					}), new object[0]);
					string text5 = ADBHelper.ExecuteCMD(cmdCommand3);
				}
				catch
				{
				}
			}
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002B94 File Offset: 0x00000D94
		public static void ConnectNox(int count = 1)
		{
			string text = "";
			int num = 62000;
			bool flag = count <= 1;
			if (flag)
			{
				text = text + "adb connect 127.0.0.1:" + (num + 1).ToString();
			}
			else
			{
				text = text + "adb connect 127.0.0.1:" + (num + 1).ToString();
				for (int i = 25; i < count + 24; i++)
				{
					text = text + Environment.NewLine + "adb connect 127.0.0.1:" + (num + i).ToString();
				}
			}
			ADBHelper.ExecuteCMD(text);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002C30 File Offset: 0x00000E30
		public static void PlanModeON(string deviceID, CancellationToken cancellationToken)
		{
			bool isCancellationRequested = cancellationToken.IsCancellationRequested;
			if (!isCancellationRequested)
			{
				string text = "adb -s " + deviceID + " settings put global airplane_mode_on 1";
				text = string.Concat(new string[]
				{
					text,
					Environment.NewLine,
					"adb -s ",
					deviceID,
					" am broadcast -a android.intent.action.AIRPLANE_MODE"
				});
				ADBHelper.ExecuteCMD(text);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002C90 File Offset: 0x00000E90
		public static void PlanModeOFF(string deviceID, CancellationToken cancellationToken)
		{
			bool isCancellationRequested = cancellationToken.IsCancellationRequested;
			if (!isCancellationRequested)
			{
				string text = "adb -s " + deviceID + " settings put global airplane_mode_on 0";
				text = string.Concat(new string[]
				{
					text,
					Environment.NewLine,
					"adb -s ",
					deviceID,
					" am broadcast -a android.intent.action.AIRPLANE_MODE"
				});
				ADBHelper.ExecuteCMD(text);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002CF0 File Offset: 0x00000EF0
		public static void Delay(double delayTime)
		{
			for (double num = 0.0; num < delayTime; num += 100.0)
			{
				Thread.Sleep(TimeSpan.FromMilliseconds(100.0));
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002D34 File Offset: 0x00000F34
		public static Point? FindImage(string deviceID, string ImagePath, int delayPerCheck = 2000, int count = 5)
		{
			return null;
			//vkl
			//DirectoryInfo directoryInfo = new DirectoryInfo(ImagePath);
			//FileInfo[] files = directoryInfo.GetFiles();
			//Point? result;
			//for (;;)
			//{
			//	Bitmap bitmap = null;
			//	int num = 3;
			//	do
			//	{
			//		try
			//		{
			//			bitmap = ADBHelper.ScreenShoot(deviceID, true, "screenShoot.png");
			//			break;
			//		}
			//		catch (Exception ex)
			//		{
			//			num--;
			//			ADBHelper.Delay(1000.0);
			//		}
			//	}
			//	while (num > 0);
			//	bool flag = bitmap == null;
			//	if (flag)
			//	{
			//		break;
			//	}
			//	result = null;
			//	foreach (FileInfo fileInfo in files)
			//	{
			//		Bitmap subBitmap = (Bitmap)Image.FromFile(fileInfo.FullName);
			//		result = ImageScanOpenCV.FindOutPoint(bitmap, subBitmap, 0.9);
			//		bool flag2 = result != null;
			//		if (flag2)
			//		{
			//			break;
			//		}
			//	}
			//	bool flag3 = result != null;
			//	if (flag3)
			//	{
			//		goto Block_4;
			//	}
			//	ADBHelper.Delay(2000.0);
			//	count--;
			//	if (count <= 0)
			//	{
			//		goto Block_5;
			//	}
			//}
			//return null;
			//Block_4:
			//return result;
			//Block_5:
			//return null;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002E5C File Offset: 0x0000105C
		public static bool FindImageAndClick(string deviceID, string ImagePath, int delayPerCheck = 2000, int count = 5)
		{
			return false;
			//vkl
			//DirectoryInfo directoryInfo = new DirectoryInfo(ImagePath);
			//FileInfo[] files = directoryInfo.GetFiles();
			//Point? point;
			//for (;;)
			//{
			//	Bitmap bitmap = null;
			//	int num = 3;
			//	do
			//	{
			//		try
			//		{
			//			bitmap = ADBHelper.ScreenShoot(deviceID, true, "screenShoot.png");
			//			break;
			//		}
			//		catch (Exception ex)
			//		{
			//			num--;
			//			ADBHelper.Delay(1000.0);
			//		}
			//	}
			//	while (num > 0);
			//	bool flag = bitmap == null;
			//	if (flag)
			//	{
			//		break;
			//	}
			//	point = null;
			//	foreach (FileInfo fileInfo in files)
			//	{
			//		Bitmap subBitmap = (Bitmap)Image.FromFile(fileInfo.FullName);
			//		point = ImageScanOpenCV.FindOutPoint(bitmap, subBitmap, 0.9);
			//		bool flag2 = point != null;
			//		if (flag2)
			//		{
			//			break;
			//		}
			//	}
			//	bool flag3 = point != null;
			//	if (flag3)
			//	{
			//		goto Block_4;
			//	}
			//	ADBHelper.Delay((double)delayPerCheck);
			//	count--;
			//	if (count <= 0)
			//	{
			//		goto Block_5;
			//	}
			//}
			//return false;
			//Block_4:
			//ADBHelper.Tap(deviceID, point.Value.X, point.Value.Y, 1);
			//return true;
			//Block_5:
			//return false;
		}

		// Token: 0x04000001 RID: 1
		private static string LIST_DEVICES = "adb devices";

		// Token: 0x04000002 RID: 2
		private static string TAP_DEVICES = "adb -s {0} shell input tap {1} {2}";

		// Token: 0x04000003 RID: 3
		private static string SWIPE_DEVICES = "adb -s {0} shell input swipe {1} {2} {3} {4} {5}";

		// Token: 0x04000004 RID: 4
		private static string KEY_DEVICES = "adb -s {0} shell input keyevent {1}";

		// Token: 0x04000005 RID: 5
		private static string INPUT_TEXT_DEVICES = "adb -s {0} shell input text \"{1}\"";

		// Token: 0x04000006 RID: 6
		private static string CAPTURE_SCREEN_TO_DEVICES = "adb -s {0} shell screencap -p \"{1}\"";

		// Token: 0x04000007 RID: 7
		private static string PULL_SCREEN_FROM_DEVICES = "adb -s {0} pull \"{1}\"";

		// Token: 0x04000008 RID: 8
		private static string REMOVE_SCREEN_FROM_DEVICES = "adb -s {0} shell rm -f \"{1}\"";

		// Token: 0x04000009 RID: 9
		private static string GET_SCREEN_RESOLUTION = "adb -s {0} shell dumpsys display | Find \"mCurrentDisplayRect\"";

		// Token: 0x0400000A RID: 10
		private const int DEFAULT_SWIPE_DURATION = 100;

		// Token: 0x0400000B RID: 11
		private static string ADB_FOLDER_PATH = "";

		// Token: 0x0400000C RID: 12
		private static string ADB_PATH = "";
	}
}
