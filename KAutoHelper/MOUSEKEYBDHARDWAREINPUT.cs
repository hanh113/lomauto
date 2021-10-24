using System;
using System.Runtime.InteropServices;

namespace KAutoHelper
{
	// Token: 0x02000010 RID: 16
	[StructLayout(LayoutKind.Explicit)]
	public struct MOUSEKEYBDHARDWAREINPUT
	{
		// Token: 0x04000102 RID: 258
		[FieldOffset(0)]
		public HARDWAREINPUT Hardware;

		// Token: 0x04000103 RID: 259
		[FieldOffset(0)]
		public KEYBDINPUT Keyboard;

		// Token: 0x04000104 RID: 260
		[FieldOffset(0)]
		public MOUSEINPUT Mouse;
	}
}
