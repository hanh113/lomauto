using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace KAutoHelper
{
	// Token: 0x0200000A RID: 10
	public class CaptureHelper
	{
		// Token: 0x0600007E RID: 126 RVA: 0x000047F0 File Offset: 0x000029F0
		public static Image CaptureScreen()
		{
			return CaptureHelper.CaptureWindow(CaptureHelper.User32.GetDesktopWindow());
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000480C File Offset: 0x00002A0C
		public static Image CaptureWindow(IntPtr handle)
		{
			IntPtr windowDC = CaptureHelper.User32.GetWindowDC(handle);
			CaptureHelper.User32.RECT rect = default(CaptureHelper.User32.RECT);
			CaptureHelper.User32.GetWindowRect(handle, ref rect);
			int nWidth = rect.right - rect.left;
			int nHeight = rect.bottom - rect.top;
			IntPtr intPtr = CaptureHelper.GDI32.CreateCompatibleDC(windowDC);
			IntPtr intPtr2 = CaptureHelper.GDI32.CreateCompatibleBitmap(windowDC, nWidth, nHeight);
			IntPtr hObject = CaptureHelper.GDI32.SelectObject(intPtr, intPtr2);
			CaptureHelper.GDI32.BitBlt(intPtr, 0, 0, nWidth, nHeight, windowDC, 0, 0, 13369376);
			CaptureHelper.GDI32.SelectObject(intPtr, hObject);
			CaptureHelper.GDI32.DeleteDC(intPtr);
			CaptureHelper.User32.ReleaseDC(handle, windowDC);
			Image result = Image.FromHbitmap(intPtr2);
			CaptureHelper.GDI32.DeleteObject(intPtr2);
			return result;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000048B4 File Offset: 0x00002AB4
		public static Bitmap ScaleImage(Image a, double zoomin)
		{
			Bitmap result;
			try
			{
				Bitmap bitmap = CaptureHelper.ResizeImage(a, a.Size.Width + (int)((double)a.Size.Width * zoomin), a.Size.Height + (int)((double)a.Size.Height * zoomin));
				result = bitmap;
			}
			catch (Exception ex)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000492C File Offset: 0x00002B2C
		public static Bitmap ResizeImage(Image image, int width, int height)
		{
			Rectangle destRect = new Rectangle(0, 0, width, height);
			Bitmap bitmap = new Bitmap(width, height);
			bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.CompositingMode = CompositingMode.SourceCopy;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				using (ImageAttributes imageAttributes = new ImageAttributes())
				{
					imageAttributes.SetWrapMode(WrapMode.TileFlipXY);
					graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
				}
			}
			return bitmap;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000049F8 File Offset: 0x00002BF8
		public static void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
		{
			Image image = CaptureHelper.CaptureWindow(handle);
			image.Save(filename, format);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004A18 File Offset: 0x00002C18
		public static void CaptureScreenToFile(string filename, ImageFormat format)
		{
			Image image = CaptureHelper.CaptureScreen();
			image.Save(filename, format);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004A38 File Offset: 0x00002C38
		public static Bitmap CaptureImage(Size size, Point position)
		{
			Bitmap result;
			try
			{
				Bitmap bitmap = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppRgb);
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.CopyFromScreen(position.X + CaptureHelper.X, position.Y + CaptureHelper.Y, 0, 0, size, CopyPixelOperation.SourceCopy);
				result = bitmap;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000085 RID: 133
		[DllImport("gdi32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteObject([In] IntPtr hObject);

		// Token: 0x06000086 RID: 134 RVA: 0x00004AAC File Offset: 0x00002CAC
		public static Bitmap CropImage(Image img, Rectangle cropRect)
		{
			Bitmap image = img as Bitmap;
			Bitmap bitmap = new Bitmap(cropRect.Width, cropRect.Height);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height), cropRect, GraphicsUnit.Pixel);
			}
			return bitmap;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004B20 File Offset: 0x00002D20
		public static Bitmap CropImage(Bitmap img, Rectangle cropRect)
		{
			Bitmap bitmap = new Bitmap(cropRect.Width, cropRect.Height);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.DrawImage(img, new Rectangle(0, 0, bitmap.Width, bitmap.Height), cropRect, GraphicsUnit.Pixel);
			}
			return bitmap;
		}

		// Token: 0x0400006F RID: 111
		public static int X;

		// Token: 0x04000070 RID: 112
		public static int Y;

		// Token: 0x02000029 RID: 41
		private class GDI32
		{
			// Token: 0x0600013A RID: 314
			[DllImport("gdi32.dll")]
			public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjectSource, int nXSrc, int nYSrc, int dwRop);

			// Token: 0x0600013B RID: 315
			[DllImport("gdi32.dll")]
			public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

			// Token: 0x0600013C RID: 316
			[DllImport("gdi32.dll")]
			public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

			// Token: 0x0600013D RID: 317
			[DllImport("gdi32.dll")]
			public static extern bool DeleteDC(IntPtr hDC);

			// Token: 0x0600013E RID: 318
			[DllImport("gdi32.dll")]
			public static extern bool DeleteObject(IntPtr hObject);

			// Token: 0x0600013F RID: 319
			[DllImport("gdi32.dll")]
			public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

			// Token: 0x040001D4 RID: 468
			public const int SRCCOPY = 13369376;
		}

		// Token: 0x0200002A RID: 42
		private class User32
		{
			// Token: 0x06000141 RID: 321
			[DllImport("user32.dll")]
			public static extern IntPtr GetDesktopWindow();

			// Token: 0x06000142 RID: 322
			[DllImport("user32.dll")]
			public static extern IntPtr GetWindowDC(IntPtr hWnd);

			// Token: 0x06000143 RID: 323
			[DllImport("user32.dll")]
			public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

			// Token: 0x06000144 RID: 324
			[DllImport("user32.dll")]
			public static extern IntPtr GetWindowRect(IntPtr hWnd, ref CaptureHelper.User32.RECT rect);

			// Token: 0x02000031 RID: 49
			public struct RECT
			{
				// Token: 0x040001F3 RID: 499
				public int left;

				// Token: 0x040001F4 RID: 500
				public int top;

				// Token: 0x040001F5 RID: 501
				public int right;

				// Token: 0x040001F6 RID: 502
				public int bottom;
			}
		}
	}
}
