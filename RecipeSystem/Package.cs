using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSystem
{
	public class Package : INotifyPropertyChanged
	{
		private string nameRecord => Path.Combine(SettingFileBase.Folder, "PackageName.txt");
		public RecipeSetting Recipe { get; set; } = new RecipeSetting("Recipe.xml");
		public ProcessSetting Process { get; set; } = new ProcessSetting("Process.xml");

		public string PackageName { get; set; } = "DefaultPack";
		public Package()
		{
			PackageName = File.ReadLines(nameRecord).First();
		}
		 ~Package()
		{
			File.WriteAllLines(nameRecord, new string[] { PackageName });
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void Load(string packName)
		{
			PackageName = packName;

			ZipHelper.UnZip(PackageName + ".zip");

			Recipe.Load();
			Process.Load();
		}

		public void Save(string name = "")
		{
			if (name != string.Empty)
				PackageName = name;

			Recipe.Save();
			Process.Save();
			ZipHelper.Zip(PackageName + ".zip");
		}

	}
}
