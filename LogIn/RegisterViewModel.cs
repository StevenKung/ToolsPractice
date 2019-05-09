using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace LogIn
{
	public class RegisterViewModel
	{
		public UserData User {get;set;} = new UserData("","",accessLevelEnum.END_USER);
		public ICommand RegisterCommand { get; set; }
		public EventHandler RegisteredEvent = (sender, e) => { };
		public RegisterViewModel()
		{
			RegisterCommand = new RelayCommand(Register);
		}

		void Register()
		{
			AccountManager.GetInstance().Register(User);
			RegisteredEvent?.Invoke(this,EventArgs.Empty);
		}

	}
}
