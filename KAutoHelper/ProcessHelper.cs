using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace KAutoHelper
{
	// Token: 0x0200001A RID: 26
	public class ProcessHelper
	{
		// Token: 0x060000FD RID: 253
		[DllImport("user32")]
		private static extern bool EnumWindows(ProcessHelper.EnumWindowsProc lpEnumFunc, IntPtr lParam);

		// Token: 0x060000FE RID: 254
		[DllImport("user32.dll")]
		private static extern bool EnumChildWindows(IntPtr hWndStart, ProcessHelper.EnumWindowsProc callback, IntPtr lParam);

		// Token: 0x060000FF RID: 255
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam, uint fuFlags, uint uTimeout, out IntPtr lpdwResult);

		// Token: 0x06000100 RID: 256 RVA: 0x00006A88 File Offset: 0x00004C88
		public static List<string> GetWindowTitles(bool includeChildren)
		{
			ProcessHelper.EnumWindows(new ProcessHelper.EnumWindowsProc(ProcessHelper.EnumWindowsCallback), includeChildren ? ((IntPtr)1) : IntPtr.Zero);
			return ProcessHelper.windowTitles;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00006AC4 File Offset: 0x00004CC4
		public static bool EnumWindowsCallback(IntPtr testWindowHandle, IntPtr includeChildren)
		{
			string windowTitle = ProcessHelper.GetWindowTitle(testWindowHandle);
			bool flag = ProcessHelper.TitleMatches(windowTitle);
			if (flag)
			{
				ProcessHelper.windowTitles.Add(windowTitle);
			}
			bool flag2 = !includeChildren.Equals(IntPtr.Zero);
			if (flag2)
			{
				ProcessHelper.EnumChildWindows(testWindowHandle, new ProcessHelper.EnumWindowsProc(ProcessHelper.EnumWindowsCallback), IntPtr.Zero);
			}
			return true;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006B28 File Offset: 0x00004D28
		public static bool TitleMatches(string title)
		{
			return title.Contains("e");
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006B48 File Offset: 0x00004D48
		public static string GetWindowTitle(IntPtr windowHandle)
		{
			uint fuFlags = 2U;
			uint msg = 13U;
			int num = 32768;
			string text = string.Empty;
			IntPtr intPtr = Marshal.AllocCoTaskMem(num);
			Marshal.Copy(text.ToCharArray(), 0, intPtr, text.Length);
			IntPtr intPtr2;
			ProcessHelper.SendMessageTimeout(windowHandle, msg, (IntPtr)num, intPtr, fuFlags, 1000U, out intPtr2);
			text = Marshal.PtrToStringAuto(intPtr);
			Marshal.FreeCoTaskMem(intPtr);
			return text;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006BB8 File Offset: 0x00004DB8
		public static List<Port> GetNetStatPorts()
		{
			List<Port> list = new List<Port>();
			try
			{
				using (Process process = new Process())
				{
					process.StartInfo = new ProcessStartInfo
					{
						Arguments = "-a -n -o",
						FileName = "netstat.exe",
						CreateNoWindow = true,
						UseShellExecute = false,
						WindowStyle = ProcessWindowStyle.Hidden,
						RedirectStandardInput = true,
						RedirectStandardOutput = true,
						RedirectStandardError = true
					};
					process.Start();
					StreamReader standardOutput = process.StandardOutput;
					StreamReader standardError = process.StandardError;
					string input = standardOutput.ReadToEnd() + standardError.ReadToEnd();
					string a = process.ExitCode.ToString();
					bool flag = a != "0";
					if (flag)
					{
					}
					string[] array = Regex.Split(input, "\r\n");
					foreach (string input2 in array)
					{
						string[] array3 = Regex.Split(input2, "\\s+");
						bool flag2 = array3.Length > 4 && (array3[1].Equals("UDP") || array3[1].Equals("TCP"));
						if (flag2)
						{
							string text = Regex.Replace(array3[2], "\\[(.*?)\\]", "1.1.1.1");
							list.Add(new Port
							{
								protocol = (text.Contains("1.1.1.1") ? string.Format("{0}v6", array3[1]) : string.Format("{0}v4", array3[1])),
								port_number = text.Split(new char[]
								{
									':'
								})[1],
								process_name = ((array3[1] == "UDP") ? ProcessHelper.LookupProcess((int)Convert.ToInt16(array3[4])) : ProcessHelper.LookupProcess((int)Convert.ToInt16(array3[5]))),
								pid = (int)Convert.ToInt16(array3[5])
							});
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
			return list;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006E00 File Offset: 0x00005000
		public static string LookupProcess(int pid)
		{
			string result;
			try
			{
				result = Process.GetProcessById(pid).ProcessName;
			}
			catch (Exception)
			{
				result = "-";
			}
			return result;
		}

		// Token: 0x040001AB RID: 427
		public static List<string> windowTitles = new List<string>();

		// Token: 0x02000030 RID: 48
		// (Invoke) Token: 0x06000147 RID: 327
		private delegate bool EnumWindowsProc(IntPtr windowHandle, IntPtr lParam);
	}
}
