using System;

namespace KAutoHelper
{
	// Token: 0x02000006 RID: 6
	public class AutoClient
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000031B1 File Offset: 0x000013B1
		public void Attach(IntPtr hwnd)
		{
			this.WindowHwnd = hwnd;
			MemoryHelper.GetWindowThreadProcessId(this.WindowHwnd, out this.processId);
			MemoryHelper.OpenProcess(this.processId, MemoryHelper.ProcessAccessFlags.All);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000031E0 File Offset: 0x000013E0
		public bool isInjected
		{
			get
			{
				return this._isInjected;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000031F8 File Offset: 0x000013F8
		public int Inject()
		{
			int num = HookGame.InjectDll(this.WindowHwnd);
			bool flag = num == 1;
			if (flag)
			{
				this._isInjected = true;
				this.HookMsg = HookGame.GetMsg();
			}
			return num;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003234 File Offset: 0x00001434
		public int DeInject()
		{
			int result = HookGame.UnmapDll(this.WindowHwnd);
			this._isInjected = false;
			return result;
		}

		// Token: 0x04000066 RID: 102
		public static int cmd_start = 1000;

		// Token: 0x04000067 RID: 103
		public static int cmd_end = 1001;

		// Token: 0x04000068 RID: 104
		public static int cmd_push = 1002;

		// Token: 0x04000069 RID: 105
		public const int cmd_sendchar = 1003;

		// Token: 0x0400006A RID: 106
		public IntPtr WindowHwnd;

		// Token: 0x0400006B RID: 107
		public uint processId;

		// Token: 0x0400006C RID: 108
		public uint HookMsg;

		// Token: 0x0400006D RID: 109
		private bool _isInjected = false;
	}
}
