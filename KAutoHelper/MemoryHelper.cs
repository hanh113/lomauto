using System;
using System.Runtime.InteropServices;
using System.Text;

namespace KAutoHelper
{
	// Token: 0x02000019 RID: 25
	public class MemoryHelper
	{
		// Token: 0x060000D5 RID: 213
		[DllImport("kernel32.dll")]
		private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

		// Token: 0x060000D6 RID: 214
		[DllImport("kernel32.dll")]
		private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, UIntPtr nSize, uint lpNumberOfBytesWritten);

		// Token: 0x060000D7 RID: 215
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);

		// Token: 0x060000D8 RID: 216
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [MarshalAs(UnmanagedType.AsAny)] object lpBuffer, int dwSize, out IntPtr lpNumberOfBytesWritten);

		// Token: 0x060000D9 RID: 217
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, MemoryHelper.FreeType dwFreeType);

		// Token: 0x060000DA RID: 218
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern uint WaitForSingleObject(IntPtr hProcess, uint dwMilliseconds);

		// Token: 0x060000DB RID: 219
		[DllImport("kernel32.dll")]
		internal static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, IntPtr dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

		// Token: 0x060000DC RID: 220
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, uint flAllocationType, uint flProtect);

		// Token: 0x060000DD RID: 221
		[DllImport("kernel32.dll")]
		internal static extern int CloseHandle(IntPtr hProcess);

		// Token: 0x060000DE RID: 222
		[DllImport("kernel32", SetLastError = true)]
		public static extern int GetProcessId(IntPtr hProcess);

		// Token: 0x060000DF RID: 223
		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		// Token: 0x060000E0 RID: 224
		[DllImport("user32.dll")]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

		// Token: 0x060000E1 RID: 225 RVA: 0x00006364 File Offset: 0x00004564
		public static IntPtr OpenProcess(int pId, MemoryHelper.ProcessAccessFlags ProcessAccess = MemoryHelper.ProcessAccessFlags.All)
		{
			return MemoryHelper.OpenProcess((uint)ProcessAccess, false, (uint)pId);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006380 File Offset: 0x00004580
		public static IntPtr OpenProcess(uint pId, MemoryHelper.ProcessAccessFlags ProcessAccess = MemoryHelper.ProcessAccessFlags.All)
		{
			return MemoryHelper.OpenProcess((uint)ProcessAccess, false, pId);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000639C File Offset: 0x0000459C
		public static int AllocateMemory(IntPtr ProcessHandle, int memorySize)
		{
			return (int)MemoryHelper.VirtualAllocEx(ProcessHandle, (IntPtr)0, (IntPtr)memorySize, 4096U, 64U);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000063CC File Offset: 0x000045CC
		public static IntPtr CreateRemoteThread(IntPtr ProcessHandle, int address)
		{
			return MemoryHelper.CreateRemoteThread(ProcessHandle, (IntPtr)0, (IntPtr)0, (IntPtr)address, (IntPtr)0, 0U, (IntPtr)0);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00006404 File Offset: 0x00004604
		public static void WaitForSingleObject(IntPtr ProcessHandle, IntPtr threadHandle)
		{
			bool flag = MemoryHelper.WaitForSingleObject(threadHandle, uint.MaxValue) > 0U;
			if (flag)
			{
				Console.WriteLine("Failed waiting for single object");
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006430 File Offset: 0x00004630
		public static void FreeMemory(IntPtr ProcessHandle, int address)
		{
			bool flag = MemoryHelper.VirtualFreeEx(ProcessHandle, (IntPtr)address, (IntPtr)0, MemoryHelper.FreeType.Release);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006458 File Offset: 0x00004658
		public static void CloseProcess(IntPtr ProcessHandle, IntPtr handle)
		{
			int num = MemoryHelper.CloseHandle(handle);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006470 File Offset: 0x00004670
		public static bool WriteInt(IntPtr Handle, IntPtr pointer, uint offset, int value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			uint num = (uint)(MemoryHelper.ReadPointer(Handle, pointer) + (int)offset);
			IntPtr zero = IntPtr.Zero;
			return MemoryHelper.WriteProcessMemory(Handle, (IntPtr)((long)((ulong)num)), bytes, bytes.Length, out zero);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000064B4 File Offset: 0x000046B4
		public static bool WriteFloat(IntPtr Handle, IntPtr pointer, uint offset, float value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			uint num = (uint)(MemoryHelper.ReadPointer(Handle, pointer) + (int)offset);
			IntPtr zero = IntPtr.Zero;
			return MemoryHelper.WriteProcessMemory(Handle, (IntPtr)((long)((ulong)num)), bytes, bytes.Length, out zero);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000064F8 File Offset: 0x000046F8
		public static bool WriteUInt(IntPtr Handle, IntPtr pointer, uint offset, uint value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			uint num = (uint)(MemoryHelper.ReadPointer(Handle, pointer) + (int)offset);
			IntPtr zero = IntPtr.Zero;
			return MemoryHelper.WriteProcessMemory(Handle, (IntPtr)((long)((ulong)num)), bytes, bytes.Length, out zero);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000653C File Offset: 0x0000473C
		public static bool WriteString(IntPtr Handle, IntPtr pointer, uint offset, string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			uint num = (uint)(MemoryHelper.ReadPointer(Handle, pointer) + (int)offset);
			IntPtr zero = IntPtr.Zero;
			return MemoryHelper.WriteProcessMemory(Handle, (IntPtr)((long)((ulong)num)), bytes, bytes.Length, out zero);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006584 File Offset: 0x00004784
		public static bool WriteStruct(IntPtr Handle, IntPtr pointer, uint offset, object value)
		{
			byte[] array = MemoryHelper.RawSerialize(value);
			uint num = (uint)(MemoryHelper.ReadPointer(Handle, pointer) + (int)offset);
			IntPtr zero = IntPtr.Zero;
			return MemoryHelper.WriteProcessMemory(Handle, (IntPtr)((long)((ulong)num)), array, array.Length, out zero);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000065C8 File Offset: 0x000047C8
		public static bool WriteBytes(IntPtr Handle, IntPtr pointer, uint offset, byte[] bytes)
		{
			uint num = (uint)(MemoryHelper.ReadPointer(Handle, pointer) + (int)offset);
			IntPtr zero = IntPtr.Zero;
			return MemoryHelper.WriteProcessMemory(Handle, (IntPtr)((long)((ulong)num)), bytes, bytes.Length, out zero);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006604 File Offset: 0x00004804
		public static bool WriteByte(IntPtr Handle, IntPtr pointer, uint offset, byte value)
		{
			byte[] bytes = BitConverter.GetBytes((short)value);
			uint num = (uint)(MemoryHelper.ReadPointer(Handle, pointer) + (int)offset);
			IntPtr zero = IntPtr.Zero;
			return MemoryHelper.WriteProcessMemory(Handle, (IntPtr)((long)((ulong)num)), bytes, bytes.Length, out zero);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006648 File Offset: 0x00004848
		public static bool WriteUnicode(IntPtr Handle, IntPtr pointer, uint offset, string value)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(value);
			uint num = (uint)(MemoryHelper.ReadPointer(Handle, pointer) + (int)offset);
			IntPtr zero = IntPtr.Zero;
			return MemoryHelper.WriteProcessMemory(Handle, (IntPtr)((long)((ulong)num)), bytes, bytes.Length, out zero);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006690 File Offset: 0x00004890
		public static int ReadInt(IntPtr Handle, IntPtr pointer, uint offset)
		{
			byte[] array = new byte[24];
			uint num = (uint)(MemoryHelper.ReadPointer(Handle, pointer) + (int)offset);
			bool flag = MemoryHelper.ReadProcessMemory(Handle, (IntPtr)((long)((ulong)num)), array, (UIntPtr)4UL, 0U);
			bool flag2 = flag;
			int result;
			if (flag2)
			{
				result = BitConverter.ToInt32(array, 0);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000066E4 File Offset: 0x000048E4
		public static uint ReadUInt(IntPtr Handle, IntPtr pointer, uint offset)
		{
			byte[] array = new byte[24];
			uint num = (uint)(MemoryHelper.ReadPointer(Handle, pointer) + (int)offset);
			bool flag = MemoryHelper.ReadProcessMemory(Handle, (IntPtr)((long)((ulong)num)), array, (UIntPtr)4UL, 0U);
			bool flag2 = flag;
			uint result;
			if (flag2)
			{
				result = BitConverter.ToUInt32(array, 0);
			}
			else
			{
				result = 0U;
			}
			return result;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006738 File Offset: 0x00004938
		public static float ReadFloat(IntPtr Handle, IntPtr pointer, uint offset)
		{
			byte[] array = new byte[24];
			uint num = (uint)(MemoryHelper.ReadPointer(Handle, pointer) + (int)offset);
			bool flag = MemoryHelper.ReadProcessMemory(Handle, (IntPtr)((long)((ulong)num)), array, (UIntPtr)4UL, 0U);
			bool flag2 = flag;
			float result;
			if (flag2)
			{
				result = BitConverter.ToSingle(array, 0);
			}
			else
			{
				result = 0f;
			}
			return result;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006790 File Offset: 0x00004990
		public static string ReadString(IntPtr Handle, IntPtr pointer, uint offset)
		{
			byte[] array = new byte[24];
			uint num = (uint)(MemoryHelper.ReadPointer(Handle, pointer) + (int)offset);
			bool flag = MemoryHelper.ReadProcessMemory(Handle, (IntPtr)((long)((ulong)num)), array, (UIntPtr)((ulong)((long)Marshal.SizeOf("".GetType()))), 0U);
			bool flag2 = flag;
			string result;
			if (flag2)
			{
				result = Encoding.UTF8.GetString(array);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000067F4 File Offset: 0x000049F4
		public static string ReadUnicode(IntPtr Handle, IntPtr pointer, uint offset, uint maxSize)
		{
			byte[] array = new byte[maxSize];
			uint num = (uint)(MemoryHelper.ReadPointer(Handle, pointer) + (int)offset);
			bool flag = MemoryHelper.ReadProcessMemory(Handle, (IntPtr)((long)((ulong)num)), array, (UIntPtr)maxSize, 0U);
			bool flag2 = flag;
			string result;
			if (flag2)
			{
				result = MemoryHelper.ByteArrayToString(array, MemoryHelper.EncodingType.Unicode);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006844 File Offset: 0x00004A44
		public static byte[] ReadBytes(IntPtr Handle, IntPtr pointer, uint offset, uint maxSize)
		{
			byte[] array = new byte[maxSize];
			uint num = (uint)(MemoryHelper.ReadPointer(Handle, pointer) + (int)offset);
			bool flag = MemoryHelper.ReadProcessMemory(Handle, (IntPtr)((long)((ulong)num)), array, (UIntPtr)maxSize, 0U);
			bool flag2 = flag;
			byte[] result;
			if (flag2)
			{
				result = array;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000688C File Offset: 0x00004A8C
		public static object ReadStruct<T>(IntPtr Handle, IntPtr pointer, uint offset)
		{
			int num = Marshal.SizeOf(default(T));
			byte[] array = new byte[num];
			uint num2 = (uint)(MemoryHelper.ReadPointer(Handle, pointer) + (int)offset);
			bool flag = MemoryHelper.ReadProcessMemory(Handle, (IntPtr)((long)((ulong)num2)), array, (UIntPtr)((ulong)((long)num)), 0U);
			bool flag2 = flag;
			object result;
			if (flag2)
			{
				result = MemoryHelper.RawDeserialize<T>(array, 0);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000068F4 File Offset: 0x00004AF4
		public static int ReadPointer(IntPtr Handle, IntPtr pointer)
		{
			byte[] array = new byte[24];
			MemoryHelper.ReadProcessMemory(Handle, pointer, array, (UIntPtr)4UL, 0U);
			return BitConverter.ToInt32(array, 0);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006928 File Offset: 0x00004B28
		private static object RawDeserialize<T>(byte[] rawData, int position)
		{
			int num = Marshal.SizeOf(default(T));
			bool flag = num > rawData.Length;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IntPtr intPtr = Marshal.AllocHGlobal(num);
				Marshal.Copy(rawData, position, intPtr, num);
				object obj = Marshal.PtrToStructure(intPtr, typeof(T));
				Marshal.FreeHGlobal(intPtr);
				result = obj;
			}
			return result;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000698C File Offset: 0x00004B8C
		private static byte[] RawSerialize(object anything)
		{
			int num = Marshal.SizeOf(anything);
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.StructureToPtr(anything, intPtr, false);
			byte[] array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
			Marshal.FreeHGlobal(intPtr);
			return array;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000069D0 File Offset: 0x00004BD0
		private static string ByteArrayToString(byte[] bytes)
		{
			return MemoryHelper.ByteArrayToString(bytes, MemoryHelper.EncodingType.Unicode);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000069EC File Offset: 0x00004BEC
		private static string ByteArrayToString(byte[] bytes, MemoryHelper.EncodingType encodingType)
		{
			Encoding encoding = null;
			string result = "";
			switch (encodingType)
			{
			case MemoryHelper.EncodingType.ASCII:
				encoding = new ASCIIEncoding();
				break;
			case MemoryHelper.EncodingType.Unicode:
				encoding = new UnicodeEncoding();
				break;
			case MemoryHelper.EncodingType.UTF7:
				encoding = new UTF7Encoding();
				break;
			case MemoryHelper.EncodingType.UTF8:
				encoding = new UTF8Encoding();
				break;
			}
			for (int i = 0; i < bytes.Length; i += 2)
			{
				bool flag = bytes[i] == 0 && bytes[i + 1] == 0;
				if (flag)
				{
					result = encoding.GetString(bytes, 0, i);
					break;
				}
			}
			return result;
		}

		// Token: 0x040001A7 RID: 423
		private const uint INFINITE = 4294967295U;

		// Token: 0x040001A8 RID: 424
		private const uint WAIT_ABANDONED = 128U;

		// Token: 0x040001A9 RID: 425
		private const uint WAIT_OBJECT_0 = 0U;

		// Token: 0x040001AA RID: 426
		private const uint WAIT_TIMEOUT = 258U;

		// Token: 0x0200002D RID: 45
		private enum EncodingType
		{
			// Token: 0x040001E1 RID: 481
			ASCII,
			// Token: 0x040001E2 RID: 482
			Unicode,
			// Token: 0x040001E3 RID: 483
			UTF7,
			// Token: 0x040001E4 RID: 484
			UTF8
		}

		// Token: 0x0200002E RID: 46
		[Flags]
		internal enum FreeType
		{
			// Token: 0x040001E6 RID: 486
			Decommit = 16384,
			// Token: 0x040001E7 RID: 487
			Release = 32768
		}

		// Token: 0x0200002F RID: 47
		public enum ProcessAccessFlags : uint
		{
			// Token: 0x040001E9 RID: 489
			All = 2035711U,
			// Token: 0x040001EA RID: 490
			Terminate = 1U,
			// Token: 0x040001EB RID: 491
			CreateThread,
			// Token: 0x040001EC RID: 492
			VMOperation = 8U,
			// Token: 0x040001ED RID: 493
			VMRead = 16U,
			// Token: 0x040001EE RID: 494
			VMWrite = 32U,
			// Token: 0x040001EF RID: 495
			DupHandle = 64U,
			// Token: 0x040001F0 RID: 496
			SetInformation = 512U,
			// Token: 0x040001F1 RID: 497
			QueryInformation = 1024U,
			// Token: 0x040001F2 RID: 498
			Synchronize = 1048576U
		}
	}
}
