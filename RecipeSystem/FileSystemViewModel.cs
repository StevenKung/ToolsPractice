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

		public SettingFileBase CurrentFile { get; set; }
		public SettingFileBase SelectedFile { get; set; }
		public ObservableCollection<SettingFileBase> Records { get; set; } = new ObservableCollection<SettingFileBase>();

		public ICommand RefreshCommand { get; set; }
		public ICommand LoadCommand { get; set; }
		public ICommand SaveCommand { get; set; }
		public ICommand SaveAsCommand { get; set; }
		public ICommand DeleteCommand { get; set; }

		#endregion

		public FileSystemViewModel()
		{
			using (StreamReader reader = new StreamReader(Path.Combine(SettingFileBase.Folder, "Current.txt")))
			{
				CurrentFile = new RecipeSetting(reader.ReadLine());
			}

			RefreshFiles();
			RefreshCommand = new RelayCommand(RefreshFiles);
			LoadCommand = new RelayCommand(LoadSelected,()=>SelectedFile!=null);
			SaveCommand = new RelayCommand(Save);
			SaveAsCommand = new RelayCommand(SaveAs);
			DeleteCommand = new RelayCommand(DeleteFile,()=>SelectedFile!=null);
		}

		~FileSystemViewModel()
		{
			CurrentFile.Save();
			using (StreamWriter writer = new StreamWriter(Path.Combine(SettingFileBase.Folder, "Current.txt")))
				writer.WriteLine(CurrentFile.FileName);
		}


		private void RefreshFiles()
		{
			Records.Clear();
			string[] files = Directory.GetFiles(SettingFileBase.Folder, "*.xml");
			foreach (var f in files)
			{
				Records.Add(new RecipeSetting(Path.GetFileName(f)));
			}
		}

		private void LoadSelected()
		{
			var result = Xceed.Wpf.Toolkit.MessageBox.Show("Save Current file?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
				CurrentFile.Save();

			CurrentFile.Load(SelectedFile.FileName);
			RefreshFiles();
		}

		private void Save()
		{
			CurrentFile.Save();
			RefreshFiles();
		}

		private void SaveAs()
		{
			var text = new TextBox { Text = CurrentFile.FileName, FontSize=20 };
			var msbox = new Xceed.Wpf.Toolkit.MessageBox
			{
				OkButtonContent = text,
				Text = "Input File Name",
				WindowOpacity = 0.8
			};
			msbox.ShowDialog();
			CurrentFile.SaveAs(text.Text);
			RefreshFiles();
		}

		private void DeleteFile()
		{
			File.Delete(Path.Combine(SettingFileBase.Folder, SelectedFile.FileName));
			RefreshFiles();
		}
	}
}
