using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LogIn
{
	public class AccountManager
	{
		string path = @"UserList.xml";
		List<UserData> Registers = new List<UserData>();
		private static AccountManager instance = new AccountManager();
		private AccountManager()
		{
			if (!File.Exists(path))
			{
				var f = File.Create(path);
				XmlSerializer s = new XmlSerializer(Registers.GetType(), new XmlRootAttribute(nameof(Registers)));
				s.Serialize(f, Registers);
				f.Close();
			}
		}

		public static AccountManager GetInstance()
		{
			return instance;
		}

		public UserData Varify(string account, string password)
		{
			// retrieve registered from file
			using (StreamReader strem = new StreamReader(path))
			{
				XmlSerializer serializer = new XmlSerializer(Registers.GetType(), new XmlRootAttribute(nameof(Registers)));
				Registers = serializer.Deserialize(strem) as List<UserData>;
			}
			var user = Registers.Find(u => u.Account == account);
			if (user != null && user.Password == password)
				return user;

			return null;
		}

		public void Register(UserData user)
		{
			XDocument doc = XDocument.Load(path);
			XElement root = new XElement(nameof(UserData));
			root.Add(new XElement(nameof(UserData.Account), user.Account));
			root.Add(new XElement(nameof(UserData.Password), user.Password));
			root.Add(new XElement(nameof(UserData.Level), user.Level));
			doc.Element(nameof(Registers)).Add(root);
			doc.Save(path);
		}
	}









	public class UserData
	{
		public string Account { get; set; }
		public string Password { get; set; }
		public accessLevelEnum Level { get; set; }
		public UserData() { }
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
