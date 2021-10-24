using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace KAutoHelper
{
	// Token: 0x0200001C RID: 28
	public struct ParentProcessUtilities
	{
		// Token: 0x06000113 RID: 275
		[DllImport("ntdll.dll")]
		private static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass, ref ParentProcessUtilities processInformation, int processInformationLength, out int returnLength);

		// Token: 0x06000114 RID: 276 RVA: 0x00006EC4 File Offset: 0x000050C4
		public static Process GetParentProcess()
		{
			return ParentProcessUtilities.GetParentProcess(Process.GetCurrentProcess().Handle);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006EE8 File Offset: 0x000050E8
		public static Process GetParentProcess(int id)
		{
			Process processById = Process.GetProcessById(id);
			return ParentProcessUtilities.GetParentProcess(processById.Handle);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006F0C File Offset: 0x0000510C
		public static Process GetParentProcess(IntPtr handle)
		{
			ParentProcessUtilities parentProcessUtilities = default(ParentProcessUtilities);
			int num2;
			int num = ParentProcessUtilities.NtQueryInformationProcess(handle, 0, ref parentProcessUtilities, Marshal.SizeOf(parentProcessUtilities), out num2);
			bool flag = num != 0;
			if (flag)
			{
				throw new Win32Exception(num);
			}
			Process result;
			try
			{
				result = Process.GetProcessById(parentProcessUtilities.InheritedFromUniqueProcessId.ToInt32());
			}
			catch (ArgumentException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x040001B0 RID: 432
		internal IntPtr Reserved1;

		// Token: 0x040001B1 RID: 433
		internal IntPtr PebBaseAddress;

		// Token: 0x040001B2 RID: 434
		internal IntPtr Reserved2_0;

		// Token: 0x040001B3 RID: 435
		internal IntPtr Reserved2_1;

		// Token: 0x040001B4 RID: 436
		internal IntPtr UniqueProcessId;

		// Token: 0x040001B5 RID: 437
		internal IntPtr InheritedFromUniqueProcessId;
	}
}
