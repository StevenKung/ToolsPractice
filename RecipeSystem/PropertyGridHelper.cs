using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace RecipeSystem
{
	public class PropertyGridHelper : DependencyObject
	{

		public static bool GetIsReadOnly(DependencyObject obj)
		{
			return (bool)obj.GetValue(IsReadOnlyProperty);
		}

		public static void SetIsReadOnly(DependencyObject obj, bool value)
		{
			obj.SetValue(IsReadOnlyProperty, value);
		}

		// Using a DependencyProperty as the backing store for IsReadOnly.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty IsReadOnlyProperty =
			DependencyProperty.RegisterAttached("IsReadOnly", typeof(bool), typeof(PropertyGridHelper), new UIPropertyMetadata(false, OnReadOnlyChanged));


		private static void OnReadOnlyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			var progrid = sender as PropertyGrid;
			if ((bool)args.NewValue == true)
			{
				progrid.PreparePropertyItem += Progrid_PreparePropertyItem;
				foreach (var item in progrid.Properties)
				{
					var prop = item as PropertyItem;
					prop.IsEnabled = false;
				}
			}

			else
			{
				progrid.PreparePropertyItem -= Progrid_PreparePropertyItem;
				foreach (var item in progrid.Properties)
				{
					var prop = item as PropertyItem;
					prop.IsEnabled = true;
				}
			}


		}
		private static void Progrid_PreparePropertyItem(object sender, PropertyItemEventArgs e)
		{
			var progrid = sender as PropertyGrid;
			var index = progrid.Properties.Cast<object>().ToList().FindIndex(x => (x as PropertyItem) == e.PropertyItem);
			if (index != 0)
				return;
			foreach (var item in progrid.Properties)
			{
				var prop = item as PropertyItem;
				prop.IsEnabled = false;
			}
		}

	}
}
