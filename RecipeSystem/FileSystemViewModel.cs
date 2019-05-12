using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace RecipeSystem
{
	public class FileSystemViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#region Binding Property

		public Package CurrentPackage { get; set; } = new Package();
		public ObservableCollection<FileSystemInfo> Records { get; set; } = new ObservableCollection<FileSystemInfo>();

		public ICommand RefreshCommand { get; set; }
		public RelayCommand<FileInfo> LoadCommand { get; set; }
		public ICommand SaveCommand { get; set; }
		public ICommand SaveAsCommand { get; set; }
		public RelayCommand<FileInfo> DeleteCommand { get; set; }

		#endregion

		public FileSystemViewModel()
		{
			
			RefreshFiles();
			RefreshCommand = new RelayCommand(RefreshFiles);
			LoadCommand = new RelayCommand<FileInfo>(LoadSelected);
			SaveCommand = new RelayCommand(Save);
			SaveAsCommand = new RelayCommand(SaveAs);
			DeleteCommand = new RelayCommand<FileInfo>(DeleteFile);
		}

		~FileSystemViewModel()
		{
		
		}


		private void RefreshFiles()
		{
			Records.Clear();
			string[] files = Directory.GetFiles(SettingFileBase.Folder, "*.zip");
			foreach (var f in files)
			{
				Records.Add(new FileInfo(f));
			}
		}

		private void LoadSelected(FileInfo file)
		{
			var result = Xceed.Wpf.Toolkit.MessageBox.Show("Save Current file?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
				CurrentPackage.Save();

			CurrentPackage.Load(Path.GetFileNameWithoutExtension(file.FullName));
			RefreshFiles();
		}

		private void Save()
		{
			CurrentPackage.Save();
			RefreshFiles();
		}

		private void SaveAs()
		{
			var text = new TextBox { Text = CurrentPackage.PackageName, FontSize=20 };
			var msbox = new Xceed.Wpf.Toolkit.MessageBox
			{
				OkButtonContent = text,
				Text = "Input File Name",
				WindowOpacity = 0.8
			};
			msbox.ShowDialog();
			CurrentPackage.Save(text.Text);
			RefreshFiles();
		}

		private void DeleteFile(FileInfo file)
		{
			File.Delete(Path.Combine(SettingFileBase.Folder, file.Name));
			RefreshFiles();
		}
	}
}
