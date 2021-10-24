using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ADBCapture.ViewModel
{
	// Token: 0x02000007 RID: 7
	public class BaseViewModel : INotifyPropertyChanged
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000296A File Offset: 0x00000B6A
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002972 File Offset: 0x00000B72
		public bool IsUpdate { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000297B File Offset: 0x00000B7B
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002983 File Offset: 0x00000B83
		public object UpdateModel { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000298C File Offset: 0x00000B8C
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000029A4 File Offset: 0x00000BA4
		public bool IsRaiseNotify
		{
			get
			{
				return this._IsRaiseNotify;
			}
			set
			{
				this._IsRaiseNotify = value;
				this.OnPropertyChanged("IsRaiseNotify");
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000029BC File Offset: 0x00000BBC
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000029D4 File Offset: 0x00000BD4
		public string NotifyMessage
		{
			get
			{
				return this._NotifyMessage;
			}
			set
			{
				this._NotifyMessage = value;
				this.OnPropertyChanged("NotifyMessage");
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000029 RID: 41 RVA: 0x000029EC File Offset: 0x00000BEC
		// (remove) Token: 0x0600002A RID: 42 RVA: 0x00002A24 File Offset: 0x00000C24
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600002B RID: 43 RVA: 0x00002A5C File Offset: 0x00000C5C
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = this.PropertyChanged;
			bool flag = handler != null;
			if (flag)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0400001D RID: 29
		public bool IsFirstLoad = true;

		// Token: 0x04000020 RID: 32
		private bool _IsRaiseNotify;

		// Token: 0x04000021 RID: 33
		private string _NotifyMessage;
	}
}
