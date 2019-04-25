using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace PropertyGridDemo
{
	/// <summary>
	/// Need to notify PropertyGrid by notification
	/// </summary>
	public class Person : INotifyPropertyChanged
	{
		string firstName = "Steve";
		public string FirstName
		{
			get => firstName;
			set
			{
				firstName = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged("second");
			}
		}
		public string LastName { get; set; } = "Nash";

		double[] testlist = { 20.4, 111 };
		public double[] TestList
		{
			get { return testlist; }
			set
			{
				testlist = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged("second");
			}
		}

		[Category("Information")]
		public short Age { get; set; } = 29;

		public Sex sex { get; set; } = Sex.Male;

		[ExpandableObject]
		public Complex com { get; set; } = new Complex();

		[Editor(typeof(PropertyGridEditor), typeof(PropertyGridEditor))]
		public Complex com2 { get; set; } = new Complex();


		public double second { get => TestList[1]; }

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}

	public enum Sex
	{
		Male,
		FeMale
	};

	public class Complex
	{
		public int var1 { get; set; } = 3;
		public string var2 { get; set; } = "???";
	}

}
