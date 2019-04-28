using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RecipeSystem
{
	public  class DataGridHeaderHelper : DependencyObject
	{
		public static DependencyProperty ItemTypeProperty = DependencyProperty.RegisterAttached(
	   "ItemType",
		typeof(Type),
		typeof(DataGridHeaderHelper),
		new PropertyMetadata(OnItemTypeChanged));
		public static void SetItemType(DependencyObject obj, Type value)
		{
			obj.SetValue(ItemTypeProperty, value);
		}
		public static Type GetItemType(DependencyObject obj)
		{
			return (Type)obj.GetValue(ItemTypeProperty);
		}
		private static void OnItemTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			var dataGrid = sender as DataGrid;
			if (args.NewValue != null)
				dataGrid.AutoGeneratingColumn += dataGrid_AutoGeneratingColumn;
			else
				dataGrid.AutoGeneratingColumn -= dataGrid_AutoGeneratingColumn;
		}
		static void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			var type = GetItemType(sender as DataGrid);
			var displayAttribute = type.GetProperty(e.PropertyName).GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() as DisplayNameAttribute;
			if (displayAttribute != null)
				e.Column.Header = displayAttribute.DisplayName;
		}
	}
}
