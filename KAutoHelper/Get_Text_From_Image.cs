using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;

namespace KAutoHelper
{
	// Token: 0x02000016 RID: 22
	public class Get_Text_From_Image
	{
		private static int saisot = 5;

		// Token: 0x0400019D RID: 413
		private static int red = 217;

		// Token: 0x0400019E RID: 414
		private static int collor_Byte_Start = 160;

		// Token: 0x0400019F RID: 415
		private static string path_langue = "C:\\";

		// Token: 0x040001A0 RID: 416
		private static string TempFolder = "image_temp";

		// Token: 0x040001A1 RID: 417
		private static string StandarFolder = "image_standand";

		// Token: 0x040001A2 RID: 418
		private static List<Color> TemplateColors = new List<Color>
		{
			Color.FromArgb(255, 0, 0, 0)
		};
		// Token: 0x0600009A RID: 154 RVA: 0x000052E0 File Offset: 0x000034E0
		public static void information(string Path_Langue)
		{
			Get_Text_From_Image.path_langue = Path_Langue;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000052EC File Offset: 0x000034EC
		public static string Get_Text(Bitmap Bm_image_sour)
		{
			Bitmap image = (Bitmap)Bm_image_sour.Clone();
			Bm_image_sour.Dispose();
			int cout_picture = Get_Text_From_Image.split_image(image, "");
			return Get_Text_From_Image.Get_Text(cout_picture);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000532C File Offset: 0x0000352C
		public static Bitmap make_new_image(Bitmap Bm_image_sour)
		{
			Get_Text_From_Image a1 = new Get_Text_From_Image();
			a1.Bm_image_sour = Bm_image_sour;
			a1._width = Bm_image_sour.Width;
			CS$<> 8__locals1._height = CS$<> 8__locals1.Bm_image_sour.Height;
			CS$<> 8__locals1.Bm_image = new Bitmap(CS$<> 8__locals1._width, CS$<> 8__locals1._height);
			int num = 230;
			for (int i = Get_Text_From_Image.collor_Byte_Start; i < num; i++)
			{
				Get_Text_From_Image.red = i;
				Get_Text_From_Image.< make_new_image > g__Get_List_Point | 9_0(ref CS$<> 8__locals1);
			}
			var b = Bm_image_sour.Bm_image;
			return CS$<> 8__locals1.Bm_image;

			//Get_Text_From_Image.<>c__DisplayClass9_0 CS$<>8__locals1;
			//CS$<>8__locals1.Bm_image_sour = Bm_image_sour;
			//CS$<>8__locals1._width = CS$<>8__locals1.Bm_image_sour.Width;
			//CS$<>8__locals1._height = CS$<>8__locals1.Bm_image_sour.Height;
			//CS$<>8__locals1.Bm_image = new Bitmap(CS$<>8__locals1._width, CS$<>8__locals1._height);
			//int num = 230;
			//for (int i = Get_Text_From_Image.collor_Byte_Start; i < num; i++)
			//{
			//	Get_Text_From_Image.red = i;
			//	Get_Text_From_Image.<make_new_image>g__Get_List_Point|9_0(ref CS$<>8__locals1);
			//}
			//return CS$<>8__locals1.Bm_image;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000053B4 File Offset: 0x000035B4
		public static int split_image(Bitmap image, string name = "")
		{
			Get_Text_From_Image.<>c__DisplayClass10_0 CS$<>8__locals1;
			CS$<>8__locals1.image = image;
			CS$<>8__locals1.name = name;
			CS$<>8__locals1.image.Save("aaa.png");
			CS$<>8__locals1.cout_picture = 0;
			bool flag = false;
			CS$<>8__locals1.width_start = 0;
			CS$<>8__locals1.width_stop = 0;
			CS$<>8__locals1._height_top = 200;
			CS$<>8__locals1._height_bottom = 0;
			int width = CS$<>8__locals1.image.Width;
			int height = CS$<>8__locals1.image.Height;
			for (int i = 0; i < width; i++)
			{
				int num = 0;
				for (int j = 0; j < height; j++)
				{
					bool flag2 = CS$<>8__locals1.image.GetPixel(i, j).Name != "ff000000";
					if (flag2)
					{
						num++;
						bool flag3 = CS$<>8__locals1._height_top > j;
						if (flag3)
						{
							CS$<>8__locals1._height_top = j;
						}
						bool flag4 = CS$<>8__locals1._height_bottom < j;
						if (flag4)
						{
							CS$<>8__locals1._height_bottom = j;
						}
					}
				}
				bool flag5 = num > 1 && !flag;
				if (flag5)
				{
					CS$<>8__locals1.width_start = i - 1;
					flag = true;
				}
				bool flag6 = num < 1 && flag;
				if (flag6)
				{
					CS$<>8__locals1.width_stop = i + 1;
					flag = false;
					Get_Text_From_Image.<split_image>g__save_image_splip|10_0(ref CS$<>8__locals1);
					int cout_picture = CS$<>8__locals1.cout_picture;
					CS$<>8__locals1.cout_picture = cout_picture + 1;
					CS$<>8__locals1._height_top = 200;
					CS$<>8__locals1._height_bottom = 0;
				}
			}
			return CS$<>8__locals1.cout_picture;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005544 File Offset: 0x00003744
		protected static string Get_Text(int cout_picture)
		{
			string text = "";
			List<string> list = new List<string>
			{
				"0",
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9"
			};
			for (int i = 0; i < cout_picture; i++)
			{
				List<double> list2 = new List<double>();
				for (int j = 0; j < list.Count; j++)
				{
					try
					{
						string str = list[j];
						double num = 0.0;
						string path = Get_Text_From_Image.StandarFolder + "\\" + str;
						DirectoryInfo directoryInfo = new DirectoryInfo(path);
						foreach (FileInfo fileInfo in directoryInfo.GetFiles())
						{
							string fullName = fileInfo.FullName;
							Bitmap bitmap = new Bitmap(fullName);
							string filename = Get_Text_From_Image.TempFolder + "\\" + i.ToString() + ".jpg";
							Bitmap bitmap2 = new Bitmap(filename);
							double num2 = Get_Text_From_Image.Image_Equal(bitmap2, bitmap);
							bitmap.Dispose();
							bitmap2.Dispose();
							bool flag = num2 > num;
							if (flag)
							{
								num = num2;
							}
						}
						list2.Add(num);
					}
					catch
					{
					}
				}
				int index = 0;
				double num3 = 0.0;
				for (int l = 0; l < list.Count; l++)
				{
					bool flag2 = num3 < list2[l];
					if (flag2)
					{
						num3 = list2[l];
						index = l;
					}
				}
				text += list[index];
			}
			return text;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00005768 File Offset: 0x00003968
		public static double Image_Equal(Bitmap main, Bitmap standand)
		{
			double num = 0.0;
			double num2 = 0.0;
			Bitmap bitmap = new Bitmap(main, new Size(standand.Width, standand.Height));
			for (int i = 0; i < standand.Width; i++)
			{
				for (int j = 0; j < standand.Height; j++)
				{
					num += 1.0;
					bool flag = bitmap.GetPixel(i, j).Equals(standand.GetPixel(i, j));
					if (flag)
					{
						num2 += 1.0;
					}
				}
			}
			return num2 / num;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005828 File Offset: 0x00003A28
		protected static void check_folder_exists(string path)
		{
			bool flag = Directory.Exists(path);
			bool flag2 = !flag;
			if (flag2)
			{
				Directory.CreateDirectory(path);
			}
		}

		internal static void <make_new_image> Get_List_Point|9_0(ref Get_Text_From_Image.<>c__DisplayClass9_0 A_0)
		{
			for (int i = 0; i < A_0._width; i++)
			{
				for (int j = 0; j < A_0._height; j++)
				{
					Color pixel = A_0.Bm_image_sour.GetPixel(i, j);
					bool flag = Get_Text_From_Image.<make_new_image>g__Check_sailenh_Color|9_1(pixel, Get_Text_From_Image.TemplateColors, Get_Text_From_Image.saisot);
					if (flag)
					{
						try
						{
							A_0.Bm_image.SetPixel(i, j, Color.Black);
						}
						catch (Exception)
						{
						}
					}
				}
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00005944 File Offset: 0x00003B44
		[CompilerGenerated]
		internal static bool <make_new_image>g__Check_sailenh_Color|9_1(Color indexColor, List<Color> templateColor, int sailech)
		{
			bool result = false;
			foreach (Color color in templateColor)
			{
				bool flag = (int)indexColor.R + sailech >= (int)color.R && (int)indexColor.R - sailech <= (int)color.R && (int)indexColor.G + sailech >= (int)color.G && (int)indexColor.G - sailech <= (int)color.G && (int)indexColor.B + sailech >= (int)color.B && (int)indexColor.B - sailech <= (int)color.B;
				if (flag)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005A20 File Offset: 0x00003C20
		[CompilerGenerated]
		internal static void <split_image>g__save_image_splip|10_0(ref Get_Text_From_Image.<>c__DisplayClass10_0 A_0)
		{
			int num = A_0.width_stop - A_0.width_start;
			int num2 = A_0._height_bottom - A_0._height_top;
			Bitmap bitmap = new Bitmap(num, num2);
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					try
					{
						Color pixel = A_0.image.GetPixel(A_0.width_start + i, A_0._height_top + j);
						bitmap.SetPixel(i, j, pixel);
					}
					catch
					{
					}
				}
			}
			string tempFolder = Get_Text_From_Image.TempFolder;
			Get_Text_From_Image.check_folder_exists(tempFolder);
			string filename = string.Concat(new string[]
			{
				tempFolder,
				"\\",
				A_0.name,
				A_0.cout_picture.ToString(),
				".jpg"
			});
			bitmap.Save(filename);
			bitmap.Dispose();
		}

		// Token: 0x0400019C RID: 412

	}
}
