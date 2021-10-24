using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace KAutoHelper
{
	// Token: 0x02000005 RID: 5
	public class NoxMultiIni
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00003054 File Offset: 0x00001254
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000305C File Offset: 0x0000125C
		public int pid { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00003065 File Offset: 0x00001265
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000306D File Offset: 0x0000126D
		public int vmpid { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00003076 File Offset: 0x00001276
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000307D File Offset: 0x0000127D
		private static List<Port> Ports { get; set; }

		// Token: 0x06000020 RID: 32 RVA: 0x00003088 File Offset: 0x00001288
		public static List<NoxMultiIni> GetNoxMultiIni()
		{
			List<NoxMultiIni> list = new List<NoxMultiIni>();
			string fullName = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
			string[] array = File.ReadAllLines(fullName + "\\Local\\Nox\\multi.ini");
			for (int i = 0; i < array.Length; i += 4)
			{
				list.Add(new NoxMultiIni
				{
					pid = Convert.ToInt32(array[i + 1].Split(new char[]
					{
						'='
					})[1]),
					vmpid = Convert.ToInt32(array[i + 2].Split(new char[]
					{
						'='
					})[1])
				});
			}
			return list;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003134 File Offset: 0x00001334
		public static string GetNoxTitleFromADBPort(string port)
		{
			NoxMultiIni.Ports = ProcessHelper.GetNetStatPorts();
			List<NoxMultiIni> noxMultiIni = NoxMultiIni.GetNoxMultiIni();
			Port abc = (from p in NoxMultiIni.Ports
			where p.port_number == port
			select p).FirstOrDefault<Port>();
			NoxMultiIni noxMultiIni2 = (from p in noxMultiIni
			where p.vmpid == abc.pid
			select p).FirstOrDefault<NoxMultiIni>();
			Process processById = Process.GetProcessById(noxMultiIni2.pid);
			return processById.MainWindowTitle;
		}
	}
}
