using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace RecipeSystem
{
	/// <summary>
	/// Support FieldNameAttribute feature
	/// </summary>
	public class AdvanceEnumEditor : EnumComboBoxEditor
	{
		private IValueConverter converter;
		protected override IEnumerable CreateItemsSource(PropertyItem propertyItem)
		{
			return base.CreateItemsSource(propertyItem).Cast<object>().Select(x => converter.Convert(x, propertyItem.PropertyType, null, null)); ;
		}
		protected override void ResolveValueBinding(PropertyItem propertyItem)
		{
			converter = new FieldToStringConverter(propertyItem.PropertyType);
			base.ResolveValueBinding(propertyItem);
		}

		protected override IValueConverter CreateValueConverter()
		{
			return converter;
		}
	}

}
