using System;
using System.Windows;
using System.Windows.Input;

namespace ADBCapture
{
	// Token: 0x02000002 RID: 2
	public class FrameworkElementExt
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
		{
			FrameworkElement element = (FrameworkElement)depObj;
			bool flag = element != null;
			if (flag)
			{
				bool flag2 = (bool)args.NewValue;
				if (flag2)
				{
					element.MouseDown += FrameworkElementExt.element_MouseDown;
					element.MouseUp += FrameworkElementExt.element_MouseUp;
					element.MouseLeave += FrameworkElementExt.element_MouseLeave;
				}
				else
				{
					element.MouseDown -= FrameworkElementExt.element_MouseDown;
					element.MouseUp -= FrameworkElementExt.element_MouseUp;
					element.MouseLeave -= FrameworkElementExt.element_MouseLeave;
				}
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020FC File Offset: 0x000002FC
		private static void element_MouseLeave(object sender, MouseEventArgs e)
		{
			FrameworkElement element = (FrameworkElement)sender;
			bool flag = element != null;
			if (flag)
			{
				element.SetValue(FrameworkElementExt.IsPressedProperty, false);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002130 File Offset: 0x00000330
		private static void element_MouseUp(object sender, MouseButtonEventArgs e)
		{
			FrameworkElement element = (FrameworkElement)sender;
			bool flag = element != null;
			if (flag)
			{
				element.SetValue(FrameworkElementExt.IsPressedProperty, false);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002164 File Offset: 0x00000364
		private static void element_MouseDown(object sender, MouseButtonEventArgs e)
		{
			FrameworkElement element = (FrameworkElement)sender;
			bool flag = element != null;
			if (flag)
			{
				element.SetValue(FrameworkElementExt.IsPressedProperty, true);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002198 File Offset: 0x00000398
		public static bool GetIsPressed(UIElement element)
		{
			return (bool)element.GetValue(FrameworkElementExt.IsPressedProperty);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021BA File Offset: 0x000003BA
		public static void SetIsPressed(UIElement element, bool val)
		{
			element.SetValue(FrameworkElementExt.IsPressedProperty, val);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021D0 File Offset: 0x000003D0
		public static bool GetAttachIsPressed(UIElement element)
		{
			return (bool)element.GetValue(FrameworkElementExt.AttachIsPressedProperty);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021F2 File Offset: 0x000003F2
		public static void SetAttachIsPressed(UIElement element, bool val)
		{
			element.SetValue(FrameworkElementExt.AttachIsPressedProperty, val);
		}

		// Token: 0x04000001 RID: 1
		public static readonly DependencyProperty IsPressedProperty = DependencyProperty.RegisterAttached("IsPressed", typeof(bool), typeof(FrameworkElementExt), new PropertyMetadata(false));

		// Token: 0x04000002 RID: 2
		public static readonly DependencyProperty AttachIsPressedProperty = DependencyProperty.RegisterAttached("AttachIsPressed", typeof(bool), typeof(FrameworkElementExt), new PropertyMetadata(false, new PropertyChangedCallback(FrameworkElementExt.PropertyChangedCallback)));
	}
}
