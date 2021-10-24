using System;
using System.Windows.Input;

namespace ADBCapture.ViewModel
{
	// Token: 0x02000008 RID: 8
	public class RelayCommand<T> : ICommand
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002A98 File Offset: 0x00000C98
		public RelayCommand(Predicate<T> canExecute, Action<T> execute)
		{
			bool flag = execute == null;
			if (flag)
			{
				throw new ArgumentNullException("execute");
			}
			this._canExecute = canExecute;
			this._execute = execute;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public bool CanExecute(object parameter)
		{
			return this._canExecute == null || this._canExecute((T)((object)parameter));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002B00 File Offset: 0x00000D00
		public void Execute(object parameter)
		{
			this._execute((T)((object)parameter));
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000030 RID: 48 RVA: 0x00002B15 File Offset: 0x00000D15
		// (remove) Token: 0x06000031 RID: 49 RVA: 0x00002B1F File Offset: 0x00000D1F
		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
			}
			remove
			{
				CommandManager.RequerySuggested -= value;
			}
		}

		// Token: 0x04000023 RID: 35
		private readonly Predicate<T> _canExecute;

		// Token: 0x04000024 RID: 36
		private readonly Action<T> _execute;
	}
}
