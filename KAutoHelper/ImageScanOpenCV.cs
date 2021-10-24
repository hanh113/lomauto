using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media;
using Emgu.CV;
using Emgu.CV.Structure;

namespace KAutoHelper
{
	// Token: 0x02000015 RID: 21
	public class ImageScanOpenCV
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00004B8C File Offset: 0x00002D8C
		public static Bitmap GetImage(string path)
		{
			return new Bitmap(path);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004BA4 File Offset: 0x00002DA4
		public static Bitmap Find(string main, string sub, double percent = 0.9)
		{
			Bitmap image = ImageScanOpenCV.GetImage(main);
			Bitmap image2 = ImageScanOpenCV.GetImage(sub);
			return ImageScanOpenCV.Find(main, sub, percent);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004BD0 File Offset: 0x00002DD0
		public static Bitmap Find(Bitmap mainBitmap, Bitmap subBitmap, double percent = 0.9)
		{
			Image<Bgr, byte> image = new Image<Bgr, byte>(mainBitmap);
			Image<Bgr, byte> image2 = new Image<Bgr, byte>(subBitmap);
			Image<Bgr, byte> image3 = image.Copy();
			using (Image<Gray, float> image4 = image.MatchTemplate(image2, (Emgu.CV.CvEnum.TemplateMatchingType)5))
			{
				double[] array;
				double[] array2;
				Point[] array3;
				Point[] array4;
				image4.MinMax(out array, out array2, out array3, out array4);
				bool flag = array2[0] > percent;
				if (flag)
				{
					Rectangle rectangle = new Rectangle(array4[0], image2.Size);
					image3.Draw(rectangle, new Bgr(System.Drawing.Color.Red), 2, (Emgu.CV.CvEnum.LineType)8, 0);
				}
				else
				{
					image3 = null;
				}
			}
			return (image3 == null) ? null : image3.ToBitmap();
		}

		// Token: 0x0600008C RID: 140
		[DllImport("gdi32.dll")]
		public static extern bool DeleteObject(IntPtr hObject);

		// Token: 0x0600008D RID: 141 RVA: 0x00004C7C File Offset: 0x00002E7C
		public static Point? FindOutPoint(Bitmap mainBitmap, Bitmap subBitmap, double percent = 0.9)
		{
			bool flag = subBitmap == null || mainBitmap == null;
			Point? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = subBitmap.Width > mainBitmap.Width || subBitmap.Height > mainBitmap.Height;
				if (flag2)
				{
					result = null;
				}
				else
				{
					Image<Bgr, byte> image = new Image<Bgr, byte>(mainBitmap);
					Image<Bgr, byte> image2 = new Image<Bgr, byte>(subBitmap);
					Point? point = null;
					using (Image<Gray, float> image3 = image.MatchTemplate(image2, (Emgu.CV.CvEnum.TemplateMatchingType)5))
					{
						double[] array;
						double[] array2;
						Point[] array3;
						Point[] array4;
						image3.MinMax(out array, out array2, out array3, out array4);
						bool flag3 = array2[0] > percent;
						if (flag3)
						{
							point = new Point?(array4[0]);
						}
					}
					GC.Collect();
					GC.WaitForPendingFinalizers();
					result = point;
				}
			}
			return result;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004D60 File Offset: 0x00002F60
		public static List<Point> FindOutPoints(Bitmap mainBitmap, Bitmap subBitmap, double percent = 0.9)
		{
			Image<Bgr, byte> image = new Image<Bgr, byte>(mainBitmap);
			Image<Bgr, byte> image2 = new Image<Bgr, byte>(subBitmap);
			List<Point> list = new List<Point>();
			for (;;)
			{
				using (Image<Gray, float> image3 = image.MatchTemplate(image2, (Emgu.CV.CvEnum.TemplateMatchingType)5))
				{
					double[] array;
					double[] array2;
					Point[] array3;
					Point[] array4;
					image3.MinMax(out array, out array2, out array3, out array4);
					bool flag = array2[0] > percent;
					if (!flag)
					{
						break;
					}
					Rectangle rectangle = new Rectangle(array4[0], image2.Size);
					image.Draw(rectangle, new Bgr(System.Drawing.Color.Blue), -1, (Emgu.CV.CvEnum.LineType)8, 0);
					list.Add(array4[0]);
				}
			}
			return list;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004E18 File Offset: 0x00003018
		public static List<Point> FindColor(Bitmap mainBitmap, System.Drawing.Color color)
		{
			int num = color.ToArgb();
			List<Point> list = new List<Point>();
			try
			{
				for (int i = 0; i < mainBitmap.Width; i++)
				{
					for (int j = 0; j < mainBitmap.Height; j++)
					{
						bool flag = num.Equals(mainBitmap.GetPixel(i, j).ToArgb());
						if (flag)
						{
							list.Add(new Point(i, j));
						}
					}
				}
			}
			finally
			{
				if (mainBitmap != null)
				{
					((IDisposable)mainBitmap).Dispose();
				}
			}
			return list;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004EC0 File Offset: 0x000030C0
		public static List<Point> FindColor(Bitmap mainBitmap, string color)
		{
			System.Drawing.Color color2 = (System.Drawing.Color)System.Windows.Media.ColorConverter.ConvertFromString(color);
			return ImageScanOpenCV.FindColor(mainBitmap, color2);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004EE8 File Offset: 0x000030E8
		public static void TestDilate(Bitmap bmp)
		{
			for (;;)
			{
				Image<Gray, byte> image = new Image<Gray, byte>(bmp);
				image.Save("old.png");
				Image<Gray, byte> image2 = new Image<Gray, byte>(image.Width, image.Height, new Gray(255.0)).Sub(image);
				image2.Save("img23.png");
				Image<Gray, byte> image3 = new Image<Gray, byte>(image2.Size);
				Image<Gray, byte> image4 = new Image<Gray, byte>(image2.Size);
				Image<Gray, byte> image5 = new Image<Gray, byte>(image2.Size);
				image5.SetValue(0.0, null);
				CvInvoke.Threshold(image2, image2, 127.0, 255.0, 0);
				Mat structuringElement = CvInvoke.GetStructuringElement(0, new Size(2, 2), new Point(-1, -1));
				CvInvoke.Dilate(image, image3, structuringElement, new Point(-1, -1), 1, (Emgu.CV.CvEnum.BorderType)2, default(MCvScalar));
				image3.CopyTo(image2);
				image5.Bitmap.Save("ele.png");
				image2.Save("img2.png");
				image3.Save("eroded.png");
				image4.Save("temp.png");
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005014 File Offset: 0x00003214
		//vkl
		//public static string RecolizeText(Bitmap img)
		//{
			
		//	return Get_Text_From_Image.Get_Text(img);
		//}

		// Token: 0x06000093 RID: 147 RVA: 0x00005034 File Offset: 0x00003234
		//vkl
		//public static void SplitImageInFolder(string folderPath)
		//{
		//	DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
		//	foreach (FileInfo fileInfo in directoryInfo.GetFiles())
		//	{
		//		Bitmap bitmap = new Bitmap(fileInfo.FullName);
		//		Bitmap image = Get_Text_From_Image.make_new_image(new Image<Gray, byte>(bitmap).ToBitmap());
		//		bitmap.Dispose();
		//		int num = Get_Text_From_Image.split_image(image, Path.GetFileNameWithoutExtension(fileInfo.Name));
		//	}
		//}

		// Token: 0x06000094 RID: 148 RVA: 0x000050A4 File Offset: 0x000032A4
		public static Bitmap ThreshHoldBinary(Bitmap bmp, byte threshold = 190)
		{
			Image<Gray, byte> image = new Image<Gray, byte>(bmp);
			Image<Gray, byte> image2 = image.ThresholdBinary(new Gray((double)threshold), new Gray(255.0));
			return image2.ToBitmap();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000050E0 File Offset: 0x000032E0
		public static Bitmap NotWhiteToTransparentPixelReplacement(Bitmap bmp)
		{
			bmp = ImageScanOpenCV.CreateNonIndexedImage(bmp);
			for (int i = 0; i < bmp.Width; i++)
			{
				for (int j = 0; j < bmp.Height; j++)
				{
					System.Drawing.Color pixel = bmp.GetPixel(i, j);
					bool flag = pixel.R > 200 && pixel.G > 200 && pixel.B > 200;
					if (flag)
					{
						bmp.SetPixel(i, j, System.Drawing.Color.Transparent);
					}
				}
			}
			return bmp;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005178 File Offset: 0x00003378
		public static Bitmap WhiteToBlackPixelReplacement(Bitmap bmp)
		{
			bmp = ImageScanOpenCV.CreateNonIndexedImage(bmp);
			for (int i = 0; i < bmp.Width; i++)
			{
				for (int j = 0; j < bmp.Height; j++)
				{
					System.Drawing.Color pixel = bmp.GetPixel(i, j);
					bool flag = pixel.R > 20 && pixel.G > 230 && pixel.B > 230;
					if (flag)
					{
						bmp.SetPixel(i, j, System.Drawing.Color.Black);
					}
				}
			}
			return bmp;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000520C File Offset: 0x0000340C
		public static Bitmap TransparentToWhitePixelReplacement(Bitmap bmp)
		{
			bmp = ImageScanOpenCV.CreateNonIndexedImage(bmp);
			for (int i = 0; i < bmp.Width; i++)
			{
				for (int j = 0; j < bmp.Height; j++)
				{
					bool flag = bmp.GetPixel(i, j).A >= 1;
					if (flag)
					{
						bmp.SetPixel(i, j, System.Drawing.Color.White);
					}
				}
			}
			return bmp;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005284 File Offset: 0x00003484
		public static Bitmap CreateNonIndexedImage(Image src)
		{
			Bitmap bitmap = new Bitmap(src.Width, src.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.DrawImage(src, 0, 0);
			}
			return bitmap;
		}
	}
}
