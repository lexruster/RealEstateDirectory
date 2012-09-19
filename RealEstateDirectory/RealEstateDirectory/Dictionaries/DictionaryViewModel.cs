using System;
using System.Windows.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries
{
	[NotifyForAll]
	public abstract class DictionaryViewModel : NotificationObject
	{
		protected DictionaryViewModel(IDataService dataService)
		{
			_DataService = dataService;
			AddCommand = new DelegateCommand(Add, CanAdd);
			ChangeCommand = new DelegateCommand(Change, CanChange);
			DeleteCommand = new DelegateCommand(Delete, CanDelete);
		}

		#region Infrastructure

		public virtual void Initialize()
		{
			InitializeEntities();
		}

		protected readonly IDataService _DataService;

		#endregion

		protected abstract void InitializeEntities();

		public ListCollectionView Entities { get; protected set; }

		protected virtual void Entities_CurrentChanged(object sender, EventArgs eventArgs)
		{
			AddCommand.RaiseCanExecuteChanged();
			DeleteCommand.RaiseCanExecuteChanged();
			ChangeCommand.RaiseCanExecuteChanged();
		}

		public DelegateCommand AddCommand { get; protected set; }
		public DelegateCommand ChangeCommand { get; protected set; }
		public DelegateCommand DeleteCommand { get; protected set; }

		protected abstract void Add();

		protected virtual bool CanAdd()
		{
			return true;
		}

		protected abstract void Change();

		protected virtual bool CanChange()
		{
		    return true;
		}

		protected abstract void Delete();

		protected virtual bool CanDelete()
		{
			return Entities.CurrentItem != null;
		}
	}
}