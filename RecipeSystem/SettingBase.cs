using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace RecipeSystem
{
	public class SettingFileBase : INotifyPropertyChanged
	{
		public static string Folder => Path.Combine(Directory.GetCurrentDirectory(), "RecipeFolder");

		protected string fileName { get; set; } //use property field can also raise FileName changed
		[XmlIgnore][DisplayName("檔案名稱")]
		public string FileName => fileName;

		private string fullPath { get { return Path.Combine(Folder, FileName); } }

		protected DateTime createdTime;
		[XmlIgnore][DisplayName("建立時間")]
		public DateTime CreatedTime { get { return createdTime; } }

		protected DateTime lastWriteTime;
		[XmlIgnore]
		[DisplayName("修改時間")]
		public DateTime LastWriteTime { get { return lastWriteTime; } }


		public SettingFileBase() { }
		public SettingFileBase(string fileName)
		{
			Load(fileName);
		}
		public event PropertyChangedEventHandler PropertyChanged;
		protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public virtual void Load()
		{
			Load(fileName);
		}


		public virtual void Load(string filename)
		{
			fileName = filename;

			FileInfo f = new FileInfo(fullPath);

			if (!f.Exists)
			{
				return;
			}
			createdTime = f.CreationTime;
			lastWriteTime = f.LastWriteTime;

			object tempData = Activator.CreateInstance(this.GetType());
			XmlSaveLoad.LoadFile(ref tempData, fullPath);

			if (tempData != null)
			{
				PropertyInfo[] propertyInfos = this.GetType().GetProperties();
				foreach (PropertyInfo pi in propertyInfos)
				{
					if (!Attribute.IsDefined(pi, typeof(XmlIgnoreAttribute)) &&
						pi.GetIndexParameters().Count() == 0 &&
						pi.CanWrite)
						pi.SetValue(this, pi.GetValue(tempData, null), null);
				}
			}
		}

		public void Save()
		{
			XmlSaveLoad.SaveFile(this, fullPath);
		}
		public void SaveAs(string fileName)
		{
			this.fileName = fileName;
			Save();
		}
	}
}
