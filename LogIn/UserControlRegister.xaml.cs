using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogIn
{
	/// <summary>
	/// UserControlRegister.xaml 的互動邏輯
	/// </summary>
	public partial class UserControlRegister : UserControl
	{
		public UserControlRegister()
		{
			InitializeComponent();
			(DataContext as RegisterViewModel).RegisteredEvent += ClearText;
		}
		private void ClearText(object sender, EventArgs e)
		{
			AccountTextBox.Text = string.Empty;
			PasswordTextBox.Text = string.Empty;
			LevelBox.SelectedValue = accessLevelEnum.END_USER;
			BindingOperations.GetBindingExpression(AccountTextBox, TextBox.TextProperty).UpdateSource();
			BindingOperations.GetBindingExpression(PasswordTextBox, TextBox.TextProperty).UpdateSource();
			BindingOperations.GetBindingExpression(LevelBox, ComboBox.SelectedValueProperty).UpdateSource();
		}
	}
}
