using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace LogIn
{
	public class MainViewModel : INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;
		protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		public ICommand LoginPageCommand { get; set; }
		public ICommand RegisterPageCommand { get; set; }

		object m_CurrentPage { get; set; }
		public object CurrentPage => m_CurrentPage;
		private object LoginPage = new UserControlLogin();
		private object RegisterPage = new UserControlRegister();
		public MainViewModel()
		{
			m_CurrentPage = LoginPage;
			LoginPageCommand = new RelayCommand(()=> m_CurrentPage = LoginPage);
			RegisterPageCommand = new RelayCommand(()=>m_CurrentPage = RegisterPage);

		}
	}
}
