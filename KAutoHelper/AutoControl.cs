using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KAutoHelper
{
	// Token: 0x02000008 RID: 8
	public class AutoControl
	{
		// Token: 0x0600002C RID: 44
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		// Token: 0x0600002D RID: 45
		[DllImport("User32.dll")]
		public static extern bool EnumChildWindows(IntPtr hWndParent, AutoControl.CallBack lpEnumFunc, IntPtr lParam);

		// Token: 0x0600002E RID: 46
		[DllImport("User32.dll")]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder s, int nMaxCount);

		// Token: 0x0600002F RID: 47
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

		// Token: 0x06000030 RID: 48
		[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
		private static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

		// Token: 0x06000031 RID: 49
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000032 RID: 50
		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

		// Token: 0x06000033 RID: 51
		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000034 RID: 52
		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

		// Token: 0x06000035 RID: 53
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

		// Token: 0x06000036 RID: 54
		[DllImport("user32.dll")]
		private static extern IntPtr GetDlgItem(IntPtr hWnd, int nIDDlgItem);

		// Token: 0x06000037 RID: 55
		[DllImport("user32.dll")]
		private static extern bool SetDlgItemTextA(IntPtr hWnd, int nIDDlgItem, string gchar);

		// Token: 0x06000038 RID: 56
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

		// Token: 0x06000039 RID: 57
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x0600003A RID: 58
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

		// Token: 0x0600003B RID: 59
		[DllImport("user32.dll")]
		public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

		// Token: 0x0600003C RID: 60
		[DllImport("user32.dll")]
		private static extern bool SetForegroundWindow(IntPtr hWnd);

		// Token: 0x0600003D RID: 61
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

		// Token: 0x0600003E RID: 62
		[DllImport("user32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool EnumChildWindows(IntPtr window, AutoControl.EnumWindowProc callback, IntPtr lParam);

		// Token: 0x0600003F RID: 63
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
		public static extern void mouse_event(uint dwFlags, int dx, int dy, int dwData, UIntPtr dwExtraInfo);

		// Token: 0x06000040 RID: 64
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

		// Token: 0x06000041 RID: 65
		[DllImport("user32.dll", SetLastError = true)]
		private static extern uint SendInput(uint numberOfInputs, INPUT[] inputs, int sizeOfInputStructure);

		// Token: 0x06000042 RID: 66
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsWindowVisible(IntPtr hWnd);

		// Token: 0x06000043 RID: 67 RVA: 0x0000328C File Offset: 0x0000148C
		public static IntPtr BringToFront(string className, string windowName = null)
		{
			IntPtr intPtr = AutoControl.FindWindow(className, windowName);
			AutoControl.SetForegroundWindow(intPtr);
			return intPtr;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000032B0 File Offset: 0x000014B0
		public static IntPtr BringToFront(IntPtr hWnd)
		{
			AutoControl.SetForegroundWindow(hWnd);
			return hWnd;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000032CC File Offset: 0x000014CC
		public static bool IsWindowVisible_(IntPtr handle)
		{
			return AutoControl.IsWindowVisible(handle);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000032E4 File Offset: 0x000014E4
		public static IntPtr FindWindowHandle(string className, string windowName)
		{
			IntPtr zero = IntPtr.Zero;
			return AutoControl.FindWindow(className, windowName);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003308 File Offset: 0x00001508
		public static List<IntPtr> FindWindowHandlesFromProcesses(string className, string windowName, int maxCount = 1)
		{
			Process[] processes = Process.GetProcesses();
			List<IntPtr> list = new List<IntPtr>();
			int num = 0;
			Process[] array = processes;
			foreach (Process process in array)
			{
				IntPtr mainWindowHandle = process.MainWindowHandle;
				string className2 = AutoControl.GetClassName(mainWindowHandle);
				string text = AutoControl.GetText(mainWindowHandle);
				bool flag = className2 == className || text == windowName;
				if (flag)
				{
					list.Add(mainWindowHandle);
					bool flag2 = num >= maxCount;
					if (flag2)
					{
						break;
					}
					num++;
				}
			}
			return list;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000033A4 File Offset: 0x000015A4
		public static IntPtr FindWindowHandleFromProcesses(string className, string windowName)
		{
			Process[] processes = Process.GetProcesses();
			IntPtr result = IntPtr.Zero;
			foreach (Process process in from p in processes
			where p.MainWindowHandle != IntPtr.Zero
			select p)
			{
				IntPtr mainWindowHandle = process.MainWindowHandle;
				string className2 = AutoControl.GetClassName(mainWindowHandle);
				string text = AutoControl.GetText(mainWindowHandle);
				bool flag = className2 == className || text == windowName;
				if (flag)
				{
					result = mainWindowHandle;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000345C File Offset: 0x0000165C
		public static IntPtr FindWindowExFromParent(IntPtr parentHandle, string text, string className)
		{
			return AutoControl.FindWindowEx(parentHandle, IntPtr.Zero, className, text);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000347C File Offset: 0x0000167C
		private static IntPtr FindWindowByIndex(IntPtr hWndParent, int index)
		{
			bool flag = index == 0;
			IntPtr result;
			if (flag)
			{
				result = hWndParent;
			}
			else
			{
				int num = 0;
				IntPtr intPtr = IntPtr.Zero;
				do
				{
					intPtr = AutoControl.FindWindowEx(hWndParent, intPtr, "Button", null);
					bool flag2 = intPtr != IntPtr.Zero;
					if (flag2)
					{
						num++;
					}
				}
				while (num < index && intPtr != IntPtr.Zero);
				result = intPtr;
			}
			return result;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000034E4 File Offset: 0x000016E4
		public static IntPtr GetControlHandleFromControlID(IntPtr parentHandle, int controlId)
		{
			return AutoControl.GetDlgItem(parentHandle, controlId);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003500 File Offset: 0x00001700
		public static List<IntPtr> GetChildHandle(IntPtr parentHandle)
		{
			List<IntPtr> list = new List<IntPtr>();
			GCHandle value = GCHandle.Alloc(list);
			IntPtr lParam2 = GCHandle.ToIntPtr(value);
			try
			{
				AutoControl.EnumWindowProc callback = delegate(IntPtr hWnd, IntPtr lParam)
				{
					GCHandle gchandle = GCHandle.FromIntPtr(lParam);
					bool flag = gchandle.Target == null;
					bool result;
					if (flag)
					{
						result = false;
					}
					else
					{
						List<IntPtr> list2 = gchandle.Target as List<IntPtr>;
						list2.Add(hWnd);
						result = true;
					}
					return result;
				};
				AutoControl.EnumChildWindows(parentHandle, callback, lParam2);
			}
			finally
			{
				value.Free();
			}
			return list;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003574 File Offset: 0x00001774
		public static IntPtr FindHandleWithText(List<IntPtr> handles, string className, string text)
		{
			return handles.Find(delegate(IntPtr ptr)
			{
				string className2 = AutoControl.GetClassName(ptr);
				string text2 = AutoControl.GetText(ptr);
				return className2 == className || text2 == text;
			});
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000035B0 File Offset: 0x000017B0
		public static List<IntPtr> FindHandlesWithText(List<IntPtr> handles, string className, string text)
		{
			List<IntPtr> list = new List<IntPtr>();
			IEnumerable<IntPtr> source = handles.Where(delegate(IntPtr ptr)
			{
				string className2 = AutoControl.GetClassName(ptr);
				string text2 = AutoControl.GetText(ptr);
				return className2 == className || text2 == text;
			});
			return source.ToList<IntPtr>();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000035F8 File Offset: 0x000017F8
		public static IntPtr FindHandle(IntPtr parentHandle, string className, string text)
		{
			return AutoControl.FindHandleWithText(AutoControl.GetChildHandle(parentHandle), className, text);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003618 File Offset: 0x00001818
		public static List<IntPtr> FindHandles(IntPtr parentHandle, string className, string text)
		{
			return AutoControl.FindHandlesWithText(AutoControl.GetChildHandle(parentHandle), className, text);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003638 File Offset: 0x00001838
		public static bool CallbackChild(IntPtr hWnd, IntPtr lParam)
		{
			string text = AutoControl.GetText(hWnd);
			string className = AutoControl.GetClassName(hWnd);
			bool flag = text == "&Options >>" && className.StartsWith("ToolbarWindow32");
			bool result;
			if (flag)
			{
				AutoControl.SendMessage(hWnd, 0, IntPtr.Zero, IntPtr.Zero);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003690 File Offset: 0x00001890
		public static void SendClickOnControlById(IntPtr parentHWND, int controlID)
		{
			IntPtr dlgItem = AutoControl.GetDlgItem(parentHWND, controlID);
			int wParam = 0 | (controlID & 65535);
			AutoControl.SendMessage(parentHWND, 273, wParam, dlgItem);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000036BE File Offset: 0x000018BE
		public static void SendClickOnControlByHandle(IntPtr hWndButton)
		{
			AutoControl.SendMessage(hWndButton, 513, IntPtr.Zero, IntPtr.Zero);
			AutoControl.SendMessage(hWndButton, 514, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000036F0 File Offset: 0x000018F0
		public static void SendClickOnPosition(IntPtr controlHandle, int x, int y, EMouseKey mouseButton = EMouseKey.LEFT, int clickTimes = 1)
		{
			int msg = 0;
			int msg2 = 0;
			bool flag = mouseButton == EMouseKey.LEFT;
			if (flag)
			{
				msg = 513;
				msg2 = 514;
			}
			bool flag2 = mouseButton == EMouseKey.RIGHT;
			if (flag2)
			{
				msg = 516;
				msg2 = 517;
			}
			IntPtr lParam = AutoControl.MakeLParamFromXY(x, y);
			bool flag3 = mouseButton == EMouseKey.LEFT || mouseButton == EMouseKey.RIGHT;
			if (flag3)
			{
				for (int i = 0; i < clickTimes; i++)
				{
					AutoControl.PostMessage(controlHandle, 6, new IntPtr(1), lParam);
					AutoControl.PostMessage(controlHandle, msg, new IntPtr(1), lParam);
					AutoControl.PostMessage(controlHandle, msg2, new IntPtr(0), lParam);
				}
			}
			else
			{
				bool flag4 = mouseButton == EMouseKey.DOUBLE_LEFT;
				if (flag4)
				{
					msg = 515;
					msg2 = 514;
				}
				bool flag5 = mouseButton == EMouseKey.DOUBLE_RIGHT;
				if (flag5)
				{
					msg = 518;
					msg2 = 517;
				}
				AutoControl.PostMessage(controlHandle, msg, new IntPtr(1), lParam);
				AutoControl.PostMessage(controlHandle, msg2, new IntPtr(0), lParam);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000037E4 File Offset: 0x000019E4
		public static void SendClickOnPositionRandom(IntPtr controlHandle, int x, int y, int ranX, int ranY, EMouseKey mouseButton = EMouseKey.LEFT, int clickTimes = 1)
		{
			int msg = 0;
			int msg2 = 0;
			bool flag = mouseButton == EMouseKey.LEFT;
			if (flag)
			{
				msg = 513;
				msg2 = 514;
			}
			bool flag2 = mouseButton == EMouseKey.RIGHT;
			if (flag2)
			{
				msg = 516;
				msg2 = 517;
			}
			ranX = AutoControl.rand.Next(ranX);
			ranY = AutoControl.rand.Next(ranY);
			IntPtr lParam = AutoControl.MakeLParamFromXY(x + ranX, y + ranY);
			bool flag3 = mouseButton == EMouseKey.LEFT || mouseButton == EMouseKey.RIGHT;
			if (flag3)
			{
				for (int i = 0; i < clickTimes; i++)
				{
					AutoControl.PostMessage(controlHandle, 6, new IntPtr(1), lParam);
					AutoControl.PostMessage(controlHandle, msg, new IntPtr(1), lParam);
					AutoControl.PostMessage(controlHandle, msg2, new IntPtr(0), lParam);
				}
			}
			else
			{
				bool flag4 = mouseButton == EMouseKey.DOUBLE_LEFT;
				if (flag4)
				{
					msg = 515;
					msg2 = 514;
				}
				bool flag5 = mouseButton == EMouseKey.DOUBLE_RIGHT;
				if (flag5)
				{
					msg = 518;
					msg2 = 517;
				}
				AutoControl.PostMessage(controlHandle, msg, new IntPtr(1), lParam);
				AutoControl.PostMessage(controlHandle, msg2, new IntPtr(0), lParam);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000038FC File Offset: 0x00001AFC
		public static void SendDragAndDropOnPosition(IntPtr controlHandle, int x, int y, int x2, int y2, int stepx = 10, int stepy = 10, double delay = 0.05)
		{
			int msg = 513;
			int msg2 = 514;
			IntPtr lParam = AutoControl.MakeLParamFromXY(x, y);
			IntPtr lParam2 = AutoControl.MakeLParamFromXY(x2, y2);
			bool flag = x2 < x;
			if (flag)
			{
				stepx *= -1;
			}
			bool flag2 = y2 < y;
			if (flag2)
			{
				stepy *= -1;
			}
			AutoControl.PostMessage(controlHandle, 6, new IntPtr(1), lParam);
			AutoControl.PostMessage(controlHandle, msg, new IntPtr(1), lParam);
			bool flag3 = false;
			bool flag4 = false;
			for (;;)
			{
				AutoControl.PostMessage(controlHandle, 512, new IntPtr(1), AutoControl.MakeLParamFromXY(x, y));
				bool flag5 = stepx > 0;
				if (flag5)
				{
					bool flag6 = x < x2;
					if (flag6)
					{
						x += stepx;
					}
					else
					{
						flag3 = true;
					}
				}
				else
				{
					bool flag7 = x > x2;
					if (flag7)
					{
						x += stepx;
					}
					else
					{
						flag3 = true;
					}
				}
				bool flag8 = stepy > 0;
				if (flag8)
				{
					bool flag9 = y < y2;
					if (flag9)
					{
						y += stepy;
					}
					else
					{
						flag4 = true;
					}
				}
				else
				{
					bool flag10 = y > y2;
					if (flag10)
					{
						y += stepy;
					}
					else
					{
						flag4 = true;
					}
				}
				bool flag11 = flag3 && flag4;
				if (flag11)
				{
					break;
				}
				Thread.Sleep(TimeSpan.FromSeconds(delay));
			}
			AutoControl.PostMessage(controlHandle, msg2, new IntPtr(0), lParam2);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003A50 File Offset: 0x00001C50
		public static void SendDragAndDropOnMultiPosition(IntPtr controlHandle, Point[] points, int stepx = 10, int stepy = 10, double delay = 0.05)
		{
			bool flag = points == null || points.Length < 2;
			if (!flag)
			{
				int msg = 513;
				int msg2 = 514;
				IntPtr lParam = AutoControl.MakeLParamFromXY(points[0].X, points[0].Y);
				AutoControl.PostMessage(controlHandle, 6, new IntPtr(1), lParam);
				AutoControl.PostMessage(controlHandle, msg, new IntPtr(1), lParam);
				for (int i = 0; i < points.Length - 1; i++)
				{
					AutoControl.MouseMoveDrag(controlHandle, points[i].X, points[i].Y, points[i + 1].X, points[i + 1].Y, stepx, stepy, delay);
				}
				AutoControl.PostMessage(controlHandle, msg2, new IntPtr(0), AutoControl.MakeLParamFromXY(points[points.Length - 1].X, points[points.Length - 1].Y));
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003B54 File Offset: 0x00001D54
		private static void MouseMoveDrag(IntPtr controlHandle, int x, int y, int x2, int y2, int stepx = 10, int stepy = 10, double delay = 0.05)
		{
			IntPtr intPtr = AutoControl.MakeLParamFromXY(x2, y2);
			bool flag = x2 < x;
			if (flag)
			{
				stepx *= -1;
			}
			bool flag2 = y2 < y;
			if (flag2)
			{
				stepy *= -1;
			}
			bool flag3 = false;
			bool flag4 = false;
			for (;;)
			{
				AutoControl.PostMessage(controlHandle, 512, new IntPtr(1), AutoControl.MakeLParamFromXY(x, y));
				bool flag5 = stepx > 0;
				if (flag5)
				{
					bool flag6 = x < x2;
					if (flag6)
					{
						x += stepx;
					}
					else
					{
						flag3 = true;
					}
				}
				else
				{
					bool flag7 = x > x2;
					if (flag7)
					{
						x += stepx;
					}
					else
					{
						flag3 = true;
					}
				}
				bool flag8 = stepy > 0;
				if (flag8)
				{
					bool flag9 = y < y2;
					if (flag9)
					{
						y += stepy;
					}
					else
					{
						flag4 = true;
					}
				}
				else
				{
					bool flag10 = y > y2;
					if (flag10)
					{
						y += stepy;
					}
					else
					{
						flag4 = true;
					}
				}
				bool flag11 = flag3 && flag4;
				if (flag11)
				{
					break;
				}
				Thread.Sleep(TimeSpan.FromSeconds(delay));
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003C58 File Offset: 0x00001E58
		public static void SendClickDownOnPosition(IntPtr controlHandle, int x, int y, EMouseKey mouseButton = EMouseKey.LEFT, int clickTimes = 1)
		{
			int msg = 0;
			bool flag = mouseButton == EMouseKey.LEFT;
			if (flag)
			{
				msg = 513;
			}
			bool flag2 = mouseButton == EMouseKey.RIGHT;
			if (flag2)
			{
				msg = 516;
			}
			IntPtr lParam = AutoControl.MakeLParamFromXY(x, y);
			for (int i = 0; i < clickTimes; i++)
			{
				AutoControl.PostMessage(controlHandle, 6, new IntPtr(1), lParam);
				AutoControl.PostMessage(controlHandle, msg, new IntPtr(1), lParam);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003CC8 File Offset: 0x00001EC8
		public static void SendClickUpOnPosition(IntPtr controlHandle, int x, int y, EMouseKey mouseButton = EMouseKey.LEFT, int clickTimes = 1)
		{
			int msg = 0;
			bool flag = mouseButton == EMouseKey.LEFT;
			if (flag)
			{
				msg = 514;
			}
			bool flag2 = mouseButton == EMouseKey.RIGHT;
			if (flag2)
			{
				msg = 517;
			}
			IntPtr lParam = AutoControl.MakeLParamFromXY(x, y);
			for (int i = 0; i < clickTimes; i++)
			{
				AutoControl.PostMessage(controlHandle, 6, new IntPtr(1), lParam);
				AutoControl.SendMessage(controlHandle, msg, new IntPtr(0), lParam);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003D37 File Offset: 0x00001F37
		public static void SendText(IntPtr handle, string text)
		{
			AutoControl.SendMessage(handle, 12, 0, text);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003D48 File Offset: 0x00001F48
		public static void SendKeyBoardPress(IntPtr handle, VKeys key)
		{
			AutoControl.PostMessage(handle, 6, new IntPtr(1), new IntPtr(0));
			AutoControl.PostMessage(handle, 256, new IntPtr((int)key), new IntPtr(1));
			AutoControl.PostMessage(handle, 257, new IntPtr((int)key), new IntPtr(0));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003D9C File Offset: 0x00001F9C
		public static void SendKeyBoardPressStepByStep(IntPtr handle, string message, float delay = 0.1f)
		{
			foreach (char c in message.ToLower())
			{
				VKeys key = VKeys.VK_0;
				char c2 = c;
				char c3 = c2;
				switch (c3)
				{
				case '0':
					key = VKeys.VK_0;
					break;
				case '1':
					key = VKeys.VK_1;
					break;
				case '2':
					key = VKeys.VK_2;
					break;
				case '3':
					key = VKeys.VK_3;
					break;
				case '4':
					key = VKeys.VK_4;
					break;
				case '5':
					key = VKeys.VK_5;
					break;
				case '6':
					key = VKeys.VK_6;
					break;
				case '7':
					key = VKeys.VK_7;
					break;
				case '8':
					key = VKeys.VK_8;
					break;
				case '9':
					key = VKeys.VK_9;
					break;
				default:
					switch (c3)
					{
					case 'a':
						key = VKeys.VK_A;
						break;
					case 'b':
						key = VKeys.VK_B;
						break;
					case 'c':
						key = VKeys.VK_V;
						break;
					case 'd':
						key = VKeys.VK_D;
						break;
					case 'e':
						key = VKeys.VK_E;
						break;
					case 'f':
						key = VKeys.VK_F;
						break;
					case 'g':
						key = VKeys.VK_G;
						break;
					case 'h':
						key = VKeys.VK_H;
						break;
					case 'i':
						key = VKeys.VK_I;
						break;
					case 'j':
						key = VKeys.VK_J;
						break;
					case 'k':
						key = VKeys.VK_K;
						break;
					case 'l':
						key = VKeys.VK_L;
						break;
					case 'm':
						key = VKeys.VK_M;
						break;
					case 'n':
						key = VKeys.VK_N;
						break;
					case 'o':
						key = VKeys.VK_O;
						break;
					case 'p':
						key = VKeys.VK_P;
						break;
					case 'q':
						key = VKeys.VK_Q;
						break;
					case 'r':
						key = VKeys.VK_R;
						break;
					case 's':
						key = VKeys.VK_S;
						break;
					case 't':
						key = VKeys.VK_T;
						break;
					case 'u':
						key = VKeys.VK_U;
						break;
					case 'v':
						key = VKeys.VK_V;
						break;
					case 'w':
						key = VKeys.VK_W;
						break;
					case 'x':
						key = VKeys.VK_X;
						break;
					case 'y':
						key = VKeys.VK_Y;
						break;
					case 'z':
						key = VKeys.VK_Z;
						break;
					}
					break;
				}
				AutoControl.SendKeyBoardPress(handle, key);
				Thread.Sleep(TimeSpan.FromSeconds((double)delay));
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003F71 File Offset: 0x00002171
		public static void SendKeyBoardUp(IntPtr handle, VKeys key)
		{
			AutoControl.PostMessage(handle, 6, new IntPtr(1), new IntPtr(0));
			AutoControl.PostMessage(handle, 257, new IntPtr((int)key), new IntPtr(0));
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003FA0 File Offset: 0x000021A0
		public static void SendKeyChar(IntPtr handle, VKeys key)
		{
			AutoControl.PostMessage(handle, 6, new IntPtr(1), new IntPtr(0));
			AutoControl.PostMessage(handle, 258, new IntPtr((int)key), new IntPtr(0));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003FA0 File Offset: 0x000021A0
		public static void SendKeyChar(IntPtr handle, int key)
		{
			AutoControl.PostMessage(handle, 6, new IntPtr(1), new IntPtr(0));
			AutoControl.PostMessage(handle, 258, new IntPtr(key), new IntPtr(0));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003FCF File Offset: 0x000021CF
		public static void SendKeyBoardDown(IntPtr handle, VKeys key)
		{
			AutoControl.PostMessage(handle, 6, new IntPtr(1), new IntPtr(0));
			AutoControl.PostMessage(handle, 256, new IntPtr((int)key), new IntPtr(0));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004000 File Offset: 0x00002200
		public static void SendTextKeyBoard(IntPtr handle, string text, float delay = 0.1f)
		{
			foreach (char key in text)
			{
				AutoControl.SendKeyChar(handle, (int)key);
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004034 File Offset: 0x00002234
		public static void SendKeyFocus(KeyCode key)
		{
			AutoControl.SendKeyPress(key);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004040 File Offset: 0x00002240
		public static void SendMultiKeysFocus(KeyCode[] keys)
		{
			foreach (KeyCode keyCode in keys)
			{
				AutoControl.SendKeyDown(keyCode);
			}
			foreach (KeyCode keyCode2 in keys)
			{
				AutoControl.SendKeyUp(keyCode2);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004091 File Offset: 0x00002291
		public static void SendStringFocus(string message)
		{
			Clipboard.SetText(message);
			AutoControl.SendMultiKeysFocus(new KeyCode[]
			{
				KeyCode.CONTROL,
				KeyCode.KEY_V
			});
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000040B4 File Offset: 0x000022B4
		public static void SendKeyPress(KeyCode keyCode)
		{
			INPUT input = new INPUT
			{
				Type = 1U
			};
			input.Data.Keyboard = new KEYBDINPUT
			{
				Vk = (ushort)keyCode,
				Scan = 0,
				Flags = 0U,
				Time = 0U,
				ExtraInfo = IntPtr.Zero
			};
			INPUT input2 = new INPUT
			{
				Type = 1U
			};
			input2.Data.Keyboard = new KEYBDINPUT
			{
				Vk = (ushort)keyCode,
				Scan = 0,
				Flags = 2U,
				Time = 0U,
				ExtraInfo = IntPtr.Zero
			};
			INPUT[] inputs = new INPUT[]
			{
				input,
				input2
			};
			bool flag = AutoControl.SendInput(2U, inputs, Marshal.SizeOf(typeof(INPUT))) == 0U;
			if (flag)
			{
				throw new Exception();
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000041A8 File Offset: 0x000023A8
		public static void SendKeyPressStepByStep(string message, double delay = 0.5)
		{
			for (int i = 0; i < message.Length; i++)
			{
				switch (message[i])
				{
				case '0':
					AutoControl.SendKeyPress(KeyCode.KEY_0);
					break;
				case '1':
					AutoControl.SendKeyPress(KeyCode.KEY_1);
					break;
				case '2':
					AutoControl.SendKeyPress(KeyCode.KEY_2);
					break;
				case '3':
					AutoControl.SendKeyPress(KeyCode.KEY_3);
					break;
				case '4':
					AutoControl.SendKeyPress(KeyCode.KEY_4);
					break;
				case '5':
					AutoControl.SendKeyPress(KeyCode.KEY_5);
					break;
				case '6':
					AutoControl.SendKeyPress(KeyCode.KEY_6);
					break;
				case '7':
					AutoControl.SendKeyPress(KeyCode.KEY_7);
					break;
				case '8':
					AutoControl.SendKeyPress(KeyCode.KEY_8);
					break;
				case '9':
					AutoControl.SendKeyPress(KeyCode.KEY_9);
					break;
				}
				Thread.Sleep(TimeSpan.FromSeconds(delay));
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004288 File Offset: 0x00002488
		public static void SendKeyDown(KeyCode keyCode)
		{
			INPUT input = new INPUT
			{
				Type = 1U
			};
			input.Data.Keyboard = default(KEYBDINPUT);
			input.Data.Keyboard.Vk = (ushort)keyCode;
			input.Data.Keyboard.Scan = 0;
			input.Data.Keyboard.Flags = 0U;
			input.Data.Keyboard.Time = 0U;
			input.Data.Keyboard.ExtraInfo = IntPtr.Zero;
			INPUT[] inputs = new INPUT[]
			{
				input
			};
			bool flag = AutoControl.SendInput(1U, inputs, Marshal.SizeOf(typeof(INPUT))) == 0U;
			if (flag)
			{
				throw new Exception();
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000434C File Offset: 0x0000254C
		public static void SendKeyUp(KeyCode keyCode)
		{
			INPUT input = new INPUT
			{
				Type = 1U
			};
			input.Data.Keyboard = default(KEYBDINPUT);
			input.Data.Keyboard.Vk = (ushort)keyCode;
			input.Data.Keyboard.Scan = 0;
			input.Data.Keyboard.Flags = 2U;
			input.Data.Keyboard.Time = 0U;
			input.Data.Keyboard.ExtraInfo = IntPtr.Zero;
			INPUT[] inputs = new INPUT[]
			{
				input
			};
			bool flag = AutoControl.SendInput(1U, inputs, Marshal.SizeOf(typeof(INPUT))) == 0U;
			if (flag)
			{
				throw new Exception();
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000440E File Offset: 0x0000260E
		public static void MouseClick(int x, int y, EMouseKey mouseKey = EMouseKey.LEFT)
		{
			Cursor.Position = new Point(x, y);
			AutoControl.Click(mouseKey);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004428 File Offset: 0x00002628
		public static void MouseDragX(Point startPoint, int deltaX, bool isNegative = false)
		{
			Cursor.Position = startPoint;
			AutoControl.mouse_event(2U, 0, 0, 0, UIntPtr.Zero);
			for (int i = 0; i < deltaX; i++)
			{
				bool flag = !isNegative;
				if (flag)
				{
					AutoControl.mouse_event(1U, 1, 0, 0, UIntPtr.Zero);
				}
				else
				{
					AutoControl.mouse_event(1U, -1, 0, 0, UIntPtr.Zero);
				}
			}
			AutoControl.mouse_event(32772U, 0, 0, 0, UIntPtr.Zero);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000044A0 File Offset: 0x000026A0
		public static void MouseDragY(Point startPoint, int deltaY, bool isNegative = false)
		{
			Cursor.Position = startPoint;
			AutoControl.mouse_event(2U, 0, 0, 0, UIntPtr.Zero);
			for (int i = 0; i < deltaY; i++)
			{
				bool flag = !isNegative;
				if (flag)
				{
					AutoControl.mouse_event(1U, 0, 1, 0, UIntPtr.Zero);
				}
				else
				{
					AutoControl.mouse_event(1U, 0, -1, 0, UIntPtr.Zero);
				}
			}
			AutoControl.mouse_event(32772U, 0, 0, 0, UIntPtr.Zero);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004515 File Offset: 0x00002715
		public static void MouseScroll(Point startPoint, int deltaY, bool isNegative = false)
		{
			Cursor.Position = startPoint;
			AutoControl.mouse_event(2048U, 0, 0, deltaY, UIntPtr.Zero);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004532 File Offset: 0x00002732
		public static void MouseClick(Point point, EMouseKey mouseKey = EMouseKey.LEFT)
		{
			Cursor.Position = point;
			AutoControl.Click(mouseKey);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004544 File Offset: 0x00002744
		public static void Click(EMouseKey mouseKey = EMouseKey.LEFT)
		{
			switch (mouseKey)
			{
			case EMouseKey.LEFT:
				AutoControl.mouse_event(32774U, 0, 0, 0, UIntPtr.Zero);
				break;
			case EMouseKey.RIGHT:
				AutoControl.mouse_event(32792U, 0, 0, 0, UIntPtr.Zero);
				break;
			case EMouseKey.DOUBLE_LEFT:
				AutoControl.mouse_event(32774U, 0, 0, 0, UIntPtr.Zero);
				AutoControl.mouse_event(32774U, 0, 0, 0, UIntPtr.Zero);
				break;
			case EMouseKey.DOUBLE_RIGHT:
				AutoControl.mouse_event(32792U, 0, 0, 0, UIntPtr.Zero);
				AutoControl.mouse_event(32792U, 0, 0, 0, UIntPtr.Zero);
				break;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000045EC File Offset: 0x000027EC
		public static RECT GetWindowRect(IntPtr hWnd)
		{
			RECT result = default(RECT);
			AutoControl.GetWindowRect(hWnd, ref result);
			return result;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004610 File Offset: 0x00002810
		public static Point GetGlobalPoint(IntPtr hWnd, Point? point = null)
		{
			Point result = default(Point);
			RECT windowRect = AutoControl.GetWindowRect(hWnd);
			bool flag = point == null;
			if (flag)
			{
				point = new Point?(default(Point));
			}
			result.X = point.Value.X + windowRect.Left;
			result.Y = point.Value.Y + windowRect.Top;
			return result;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004690 File Offset: 0x00002890
		public static Point GetGlobalPoint(IntPtr hWnd, int x = 0, int y = 0)
		{
			Point result = default(Point);
			RECT windowRect = AutoControl.GetWindowRect(hWnd);
			result.X = x + windowRect.Left;
			result.Y = y + windowRect.Top;
			return result;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000046D4 File Offset: 0x000028D4
		public static string GetText(IntPtr hWnd)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			AutoControl.GetWindowText(hWnd, stringBuilder, 256);
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000470C File Offset: 0x0000290C
		public static string GetClassName(IntPtr hWnd)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			AutoControl.GetClassName(hWnd, stringBuilder, 256);
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004744 File Offset: 0x00002944
		public static IntPtr MakeLParam(int LoWord, int HiWord)
		{
			return (IntPtr)(HiWord << 16 | (LoWord & 65535));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004768 File Offset: 0x00002968
		public static IntPtr MakeLParamFromXY(int x, int y)
		{
			return (IntPtr)(y << 16 | x);
		}

		// Token: 0x0400006E RID: 110
		private static Random rand = new Random();

		// Token: 0x02000020 RID: 32
		// (Invoke) Token: 0x06000123 RID: 291
		public delegate bool CallBack(IntPtr hwnd, IntPtr lParam);

		// Token: 0x02000021 RID: 33
		// (Invoke) Token: 0x06000127 RID: 295
		private delegate bool EnumWindowProc(IntPtr hwnd, IntPtr lParam);

		// Token: 0x02000022 RID: 34
		[Flags]
		public enum MouseEventFlags : uint
		{
			// Token: 0x040001BB RID: 443
			LEFTDOWN = 2U,
			// Token: 0x040001BC RID: 444
			LEFTUP = 4U,
			// Token: 0x040001BD RID: 445
			MIDDLEDOWN = 32U,
			// Token: 0x040001BE RID: 446
			MIDDLEUP = 64U,
			// Token: 0x040001BF RID: 447
			MOVE = 1U,
			// Token: 0x040001C0 RID: 448
			ABSOLUTE = 32768U,
			// Token: 0x040001C1 RID: 449
			RIGHTDOWN = 8U,
			// Token: 0x040001C2 RID: 450
			RIGHTUP = 16U,
			// Token: 0x040001C3 RID: 451
			WHEEL = 2048U,
			// Token: 0x040001C4 RID: 452
			XDOWN = 128U,
			// Token: 0x040001C5 RID: 453
			XUP = 256U,
			// Token: 0x040001C6 RID: 454
			XBUTTON1 = 1U,
			// Token: 0x040001C7 RID: 455
			XBUTTON2 = 2U
		}
	}
}
