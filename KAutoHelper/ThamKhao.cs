using System;
using System.Runtime.InteropServices;

namespace KAutoHelper
{
	// Token: 0x0200001D RID: 29
	internal class ThamKhao
	{
		// Token: 0x06000117 RID: 279
		[DllImport("user32.dll", SetLastError = true)]
		private static extern uint SendInput(uint numberOfInputs, INPUT[] inputs, int sizeOfInputStructure);

		// Token: 0x06000118 RID: 280 RVA: 0x00006F78 File Offset: 0x00005178
		public void SendKeyPress(KeyCode keyCode)
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
			bool flag = ThamKhao.SendInput(2U, inputs, Marshal.SizeOf(typeof(INPUT))) == 0U;
			if (flag)
			{
				throw new Exception();
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000706C File Offset: 0x0000526C
		public void SendKeyDown(KeyCode keyCode)
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
			bool flag = ThamKhao.SendInput(1U, inputs, Marshal.SizeOf(typeof(INPUT))) == 0U;
			if (flag)
			{
				throw new Exception();
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007130 File Offset: 0x00005330
		public void SendKeyUp(KeyCode keyCode)
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
			bool flag = ThamKhao.SendInput(1U, inputs, Marshal.SizeOf(typeof(INPUT))) == 0U;
			if (flag)
			{
				throw new Exception();
			}
		}
	}
}
