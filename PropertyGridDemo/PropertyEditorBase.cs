using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace PropertyGridDemo
{
	/// <summary>
	/// Custom editor show in PropertyGrid to popup
	/// </summary>
	public abstract class PropertyEditorBase : ITypeEditor
	{
		/// <summary>
		/// Provide property info
		/// </summary>
		protected PropertyItem m_Property;

		public virtual FrameworkElement ResolveEditor(PropertyItem propertyItem)
		{
			m_Property = propertyItem;

			// how to present in PropertyGrid
			TextBox textBox = new TextBox();

			Button btn = new Button
			{
				Content = "...",
				Width = 15
			};
			btn.Click += CreatePopup;
			DockPanel.SetDock(btn, Dock.Right);

			DockPanel panel = new DockPanel();
			panel.Children.Add(btn);
			panel.Children.Add(textBox);

			UserControl control = new UserControl
			{
				Content = panel
			};

			//create the binding from the bound property item to the editor
			var _binding = new Binding("Value")
			{
				Source = propertyItem,
				ValidatesOnExceptions = true,
				ValidatesOnDataErrors = true,
				Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay
			}; 

			//bind to the Value property of the PropertyItem
			BindingOperations.SetBinding(textBox, TextBox.TextProperty, _binding);
			return control;
		}

		public abstract void CreatePopup(object sender, RoutedEventArgs e);
	}
}
