using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace DataGridDemo
{
	class MainWindowViewModel : INotifyPropertyChanged
	{
		#region Binding properties

		public ObservableCollection<MessageInfo> MessageCollection { get; set; } = new ObservableCollection<MessageInfo>();
		public ICommand FilterCommand { get; set; }
		public ICommand ResetCommand { get; set; }
		public ICommand GroupCommand { get; set; }
		public RelayCommand<SelectionChangedEventArgs> FilterDateCommand { get; set; }
		public int Count { get => MessageCollection.Count(); }
		public DateTime StartDate { get; set; } = DateTime.Now;

		#endregion

		public MainWindowViewModel()
		{
			MessageCollection.Add(new MessageInfo() { Type = MessageType.Alarm });
			Thread.Sleep(1000);
			MessageCollection.Add(new MessageInfo());
			Thread.Sleep(1000);
			MessageCollection.Add(new MessageInfo());
			FilterCommand = new RelayCommand(FilterCollection);
			ResetCommand = new RelayCommand(ResetCollection);
			GroupCommand = new RelayCommand(GroupCollection);
			FilterDateCommand = new RelayCommand<SelectionChangedEventArgs>(FilterDate);
		}

		#region Command Function

		private void FilterCollection()
		{
			ICollectionView collection = CollectionViewSource.GetDefaultView(MessageCollection);
			collection.Filter = new Predicate<object>(x =>
			{
				return (x as MessageInfo).Type == MessageType.Info;
			});
		}
		private void ResetCollection()
		{
			ICollectionView collection = CollectionViewSource.GetDefaultView(MessageCollection);
			// Reset filter
			collection.Filter = null;
		}
		private void GroupCollection()
		{
			ICollectionView collection = CollectionViewSource.GetDefaultView(MessageCollection);
			collection.GroupDescriptions.Clear();
			string name = nameof(MessageInfo.Type);
			collection.GroupDescriptions.Add(new PropertyGroupDescription(name));
		}
		private void FilterDate(SelectionChangedEventArgs e)
		{
			ICollectionView collection = CollectionViewSource.GetDefaultView(MessageCollection);
			collection.Filter = new Predicate<object>(x =>
			{
				return (x as MessageInfo).Time > StartDate;
			});

		}
		#endregion

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
