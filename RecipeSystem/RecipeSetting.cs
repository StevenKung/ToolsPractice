using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace RecipeSystem
{
	[Serializable]
	public class RecipeSetting : SettingFileBase
	{
		public double[] Offsets { get; set; } = { 1, 2, 3.5 };

		[Editor(typeof(AdvanceEnumEditor), typeof(AdvanceEnumEditor))]
		public FeedMode Mode { get; set; } = FeedMode.DISABLE;

		public TrackingSetting Tracking { get; set; } = new TrackingSetting();

		public RecipeSetting() : base() { }
		public RecipeSetting(string fileName) : base(fileName) { }
		
	}

	[Serializable]
	[ExpandableObject]
	public class TrackingSetting
	{
		public long Distance { get; set; } = 1000;
		public bool IsEnable { get; set; } = false;
		public double Delay { get; set; } = 1;

		
	}

	public enum FeedMode
	{
		[FieldNameAttribute("Feed Material In")]
		FEEDIN,

		[FieldNameAttribute("Feed Material Out")]
		FEEDOUT,

		[FieldNameAttribute("Disable this module")]
		DISABLE
	};







}
