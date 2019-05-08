using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LogIn
{
	public class MainViewModel
	{
		object m_CurrentPage;
		public object CurrentPage => m_CurrentPage;
		public MainViewModel()
		{
			m_CurrentPage = new LoginViewModel();
		}
	}
}
