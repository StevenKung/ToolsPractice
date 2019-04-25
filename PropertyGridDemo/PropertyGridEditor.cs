using System.Windows;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace PropertyGridDemo
{
	/// <summary>
	/// Custom popup windows in propertygrid
	/// </summary>
	public class PropertyGridEditor : PropertyEditorBase
	{
		public override void CreatePopup(object sender, RoutedEventArgs e)
		{
			Window win = new Window();
			PropertyGrid grid = new PropertyGrid();
			grid.SelectedObject = m_Property.Value;
			win.Content = grid;
			win.ShowDialog();
		}
	}
}
