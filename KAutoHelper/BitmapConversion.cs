using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace KAutoHelper
{
	// Token: 0x02000003 RID: 3
	public static class BitmapConversion
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000301C File Offset: 0x0000121C
		public static BitmapSource BitmapToBitmapSource(Bitmap source)
		{
			bool flag = source == null;
			BitmapSource result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = Imaging.CreateBitmapSourceFromHBitmap(source.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
			}
			return result;
		}
	}
}
