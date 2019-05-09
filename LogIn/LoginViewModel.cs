using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace LogIn
{
	public class LoginViewModel : INotifyPropertyChanged
	{
		public UserData m_CurrentUser { get; set; }
		public UserData CurrentUser => m_CurrentUser;
		public ICommand LoginCommand { get; set; }
		public LoginViewModel()
		{
			LoginCommand = new RelayCommand<UserData>(Login);
		}
		private void Login(UserData user)
		{
			var m_CurrentUser = AccountManager.GetInstance().Varify(user.Account, user.Password);
		}
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
