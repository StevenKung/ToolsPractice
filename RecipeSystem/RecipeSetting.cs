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
		[ExpandableObject]
		public double[] Offsets { get; set; } = { 1, 2, 3.5 };

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


	[Editor(typeof(AdvanceEnumEditor), typeof(AdvanceEnumEditor))]
	public enum FeedMode
	{
		[FieldName("Feed Material In")]
		FEEDIN,

		[FieldName("Feed Material Out")]
		FEEDOUT,

		[FieldName("Disable this module")]
		DISABLE
	};


	public class ProcessSetting : SettingFileBase
	{
		public short Interval { get; set; } = 3;
		public short TotalDefect { get; set; } = 10;
		public short SingleDefect { get; set; } = 3;
		public ColletShape Shape { get; set; } = ColletShape.CIRCLE;

		public ProcessSetting() : base() { }
		public ProcessSetting(string fileName) : base(fileName) { }
	}

	[Editor(typeof(AdvanceEnumEditor), typeof(AdvanceEnumEditor))]
	public enum ColletShape
	{
		[FieldName("圓形")]
		CIRCLE,
		[FieldName("方形")]
		RECTANGLE,
		[FieldName("任意形狀")]
		PATTERN
	}





}
