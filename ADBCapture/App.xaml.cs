using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace ADBCapture
{
    // Token: 0x02000003 RID: 3
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //var f = new Window1();
            var f = new MainWindow();
            f.Show();
        }
    }
}
