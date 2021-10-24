using System;
using System.Runtime.InteropServices;

namespace KAutoHelper
{
	// Token: 0x02000007 RID: 7
	public static class HookGame
	{
		// Token: 0x06000029 RID: 41
		[DllImport("khook.dll", SetLastError = true)]
		public static extern int InjectDll(IntPtr gameHwnd);

		// Token: 0x0600002A RID: 42
		[DllImport("khook.dll", SetLastError = true)]
		public static extern int UnmapDll(IntPtr gameHwnd);

		// Token: 0x0600002B RID: 43
		[DllImport("khook.dll", SetLastError = true)]
		public static extern uint GetMsg();
	}
}
