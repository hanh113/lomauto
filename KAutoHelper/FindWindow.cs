using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace KAutoHelper
{
	// Token: 0x02000009 RID: 9
	public class FindWindow
	{
		// Token: 0x06000079 RID: 121
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool EnumWindows(FindWindow.EnumWindowsProc enumProc, IntPtr lParam);

		// Token: 0x0600007A RID: 122
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

		// Token: 0x0600007B RID: 123
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

		// Token: 0x0600007C RID: 124 RVA: 0x00004794 File Offset: 0x00002994
		public static List<IntPtr> GetWindowHandles(string processName, string className)
		{
			List<IntPtr> handleList = new List<IntPtr>();
			Process[] processes = Process.GetProcessesByName(processName);
			Process proc = null;
			FindWindow.EnumWindows(delegate(IntPtr hWnd, IntPtr lParam)
			{
				int processId;
				FindWindow.GetWindowThreadProcessId(hWnd, out processId);
				proc = processes.FirstOrDefault((Process p) => p.Id == processId);
				bool flag = proc != null;
				if (flag)
				{
					StringBuilder stringBuilder = new StringBuilder(256);
					FindWindow.GetClassName(hWnd, stringBuilder, 256);
					bool flag2 = stringBuilder.ToString() == className;
					if (flag2)
					{
						handleList.Add(hWnd);
					}
				}
				return true;
			}, IntPtr.Zero);
			return handleList;
		}

		// Token: 0x02000026 RID: 38
		// (Invoke) Token: 0x06000133 RID: 307
		public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
	}
}
