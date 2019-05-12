using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;


namespace RecipeSystem
{
	public class ZipHelper
	{
		public static void UnZip(string fileName)
		{
			foreach (string name in GetSettingFiles())
				File.Delete(name);

			//unZip file
			ZipFile.ExtractToDirectory(Path.Combine(SettingFileBase.Folder,fileName), SettingFileBase.Folder);
		}

		/// <summary>
		/// Return fullPath in setting folder
		/// </summary>
		/// <returns></returns>
		private static List<string> GetSettingFiles()
		{
			return Directory.GetFiles(SettingFileBase.Folder).ToList().FindAll(x => x.EndsWith(".xml"));
		}

		public static void Zip(string fileName)
		{
			//delete exist same name zip file
			string fullpath = Path.Combine(SettingFileBase.Folder, fileName);
			//will not exception even file is not existed
			File.Delete(fullpath);
			
			using (ZipArchive archive = ZipFile.Open(fullpath, ZipArchiveMode.Create))
			{
				foreach (string name in GetSettingFiles())
					archive.CreateEntryFromFile(name, Path.GetFileName(name));
			}
		}

	}
}
