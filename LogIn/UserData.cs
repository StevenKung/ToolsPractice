using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogIn
{
	public class UserData
	{
		public string Account { get; set; }
		public string Password { get; set; }
		public accessLevelEnum Level { get; set; }
		public UserData(string account, string password, accessLevelEnum level)
		{
			Account = account;
			Password = password;
			Level = level;
		}
	}

	public enum accessLevelEnum
	{
		DEVELOPER = 0x100,
		SERVICE = 0x10,
		END_USER = 0x1
	}
}
