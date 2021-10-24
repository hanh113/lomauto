using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Navigation;
using ADBCapture.ViewModel;

namespace ADBCapture
{
	// Token: 0x02000004 RID: 4
	public partial class MainWindow : Window
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000022FB File Offset: 0x000004FB
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002303 File Offset: 0x00000503
		public MainViewModel ViewModel { get; set; }

		// Token: 0x06000010 RID: 16 RVA: 0x0000230C File Offset: 0x0000050C
		public MainWindow()
		{
			this.InitializeComponent();
			base.DataContext = (this.ViewModel = new MainViewModel(this));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000238E File Offset: 0x0000058E
		private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
		{
			Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
			e.Handled = true;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023B0 File Offset: 0x000005B0
		private void gridSize_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			this.isMoving = true;
			this.oldPoint = e.GetPosition(this);
			this.oldWidth = this.cldWidth.Width.Value;
			this.oldHeight = this.rwdHeight.Height.Value;
			this.oldWidthBottom = this.cldWidthBottom.Width.Value;
			this.oldHeightBottom = this.rwdHeightBottom.Height.Value;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002436 File Offset: 0x00000636
		private void gridSize_PreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			this.isMoving = false;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002440 File Offset: 0x00000640
		private void gridSize_PreviewMouseMove(object sender, MouseEventArgs e)
		{
			bool flag = this.isMoving;
			if (flag)
			{
				System.Windows.Point currentPos = e.GetPosition(this);
				Vector deltaPoint = System.Windows.Point.Subtract(currentPos, this.oldPoint);
				double newWidth = this.oldWidth + deltaPoint.X;
				double newHeight = this.oldHeight + deltaPoint.Y;
				double newWidthBottom = this.oldWidthBottom - deltaPoint.X;
				double newHeightBottom = this.oldHeightBottom - deltaPoint.Y;
				newWidth = ((newWidth < 0.0) ? 0.0 : newWidth);
				newWidthBottom = ((newWidthBottom < 0.0) ? 0.0 : newWidthBottom);
				newHeight = ((newHeight < 0.0) ? 0.0 : newHeight);
				newHeightBottom = ((newHeightBottom < 0.0) ? 0.0 : newHeightBottom);
				bool flag2 = newWidth >= 0.0 && newWidthBottom >= 0.0;
				if (flag2)
				{
					bool flag3 = newWidthBottom > 0.0;
					if (flag3)
					{
						this.cldWidth.Width = new GridLength(newWidth);
					}
					bool flag4 = newWidth > 0.0;
					if (flag4)
					{
						this.cldWidthBottom.Width = new GridLength(newWidthBottom);
					}
				}
				bool flag5 = newHeight >= 0.0 && newHeightBottom >= 0.0;
				if (flag5)
				{
					bool flag6 = newHeightBottom > 0.0;
					if (flag6)
					{
						this.rwdHeight.Height = new GridLength(newHeight);
					}
					bool flag7 = newHeight > 0.0;
					if (flag7)
					{
						this.rwdHeightBottom.Height = new GridLength(newHeightBottom);
					}
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002604 File Offset: 0x00000804
		private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			bool flag = this.ViewModel == null;
			if (!flag)
			{
				System.Drawing.Size size = new System.Drawing.Size((int)this.gridSize.ActualWidth, (int)this.gridSize.ActualHeight);
				System.Drawing.Point point = new System.Drawing.Point((int)this.gridPosition.ActualWidth + 3, (int)this.gridPosition.ActualHeight + 3);
				this.ViewModel.CropImage(size, point);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002671 File Offset: 0x00000871
		private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
		{
			this.ViewModel.LoadDevicesList();
		}

		// Token: 0x04000004 RID: 4
		private bool isMoving = false;

		// Token: 0x04000005 RID: 5
		private System.Windows.Point oldPoint = default(System.Windows.Point);

		// Token: 0x04000006 RID: 6
		private double oldWidth = 0.0;

		// Token: 0x04000007 RID: 7
		private double oldHeight = 0.0;

		// Token: 0x04000008 RID: 8
		private double oldWidthBottom = 0.0;

		// Token: 0x04000009 RID: 9
		private double oldHeightBottom = 0.0;
	}
}
