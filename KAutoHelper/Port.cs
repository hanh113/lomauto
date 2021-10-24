using System;

namespace KAutoHelper
{
	// Token: 0x0200001B RID: 27
	public class Port
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00006E4C File Offset: 0x0000504C
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00006E7A File Offset: 0x0000507A
		public string name
		{
			get
			{
				return string.Format("{0} ({1} port {2})", this.process_name, this.protocol, this.port_number);
			}
			set
			{
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00006E7D File Offset: 0x0000507D
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00006E85 File Offset: 0x00005085
		public string port_number { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00006E8E File Offset: 0x0000508E
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00006E96 File Offset: 0x00005096
		public string process_name { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00006E9F File Offset: 0x0000509F
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00006EA7 File Offset: 0x000050A7
		public string protocol { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00006EB0 File Offset: 0x000050B0
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00006EB8 File Offset: 0x000050B8
		public int pid { get; set; }
	}
}
