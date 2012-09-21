using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries
{
	[NotifyForAll]
	public abstract class DictionaryViewModel<TEntityViewModel, TEntity> : NotificationObject
		where TEntity : class
		where TEntityViewModel : DictionaryEntityViewModel<TEntity>
	{
		protected DictionaryViewModel(IServiceLocator serviceLocator, IDataService dataService, IMessageService messageService)
		{
			_ServiceLocator = serviceLocator;
			_DataService = dataService;
			_MessageService = messageService;
		}

		#region Infrastructure

		public virtual void Initialize()
		{
			InitializeEntities();
			_Entities.CollectionChanging += Entities_CollectionChanging;
		}

		protected readonly IServiceLocator _ServiceLocator;

		protected readonly IDataService _DataService;

		protected readonly IMessageService _MessageService;

		#endregion

		protected abstract void InitializeEntities();

		protected DataObservableCollection<TEntityViewModel> _Entities;

		private void Entities_CollectionChanging(object sender, CollectionChangingEventArgs<TEntityViewModel> e)
		{
			switch (e.Action)
			{
				case CollectionChangeAction.Add:
					string validateError;
					if (!IsCorrect(e.Item, out validateError))
					{
						_MessageService.ShowMessage(validateError, "Ошибка", image: MessageBoxImage.Error);
						e.Cancel = true;
					}
					else
						AssociateWithModel(e.Item);
					break;
				case CollectionChangeAction.Remove:
					string cantRemoveError;
					if (!IsCanRemove(e.Item, out cantRemoveError))
					{
						_MessageService.ShowMessage(cantRemoveError, "Ошибка", image: MessageBoxImage.Error);
						e.Cancel = true;
					}
					else
						RemoveEntity(e.Item);
					break;
			}
		}

		protected abstract void AssociateWithModel(TEntityViewModel entity);

		protected abstract bool IsCorrect(TEntityViewModel entity, out string errorText);

		protected abstract bool IsCanRemove(TEntityViewModel entity, out string errorText);

		protected abstract void RemoveEntity(TEntityViewModel entity);

		public ListCollectionView Entities { get; protected set; }

		protected virtual void Entities_CurrentChanged(object sender, EventArgs eventArgs)
		{
			AddCommand.RaiseCanExecuteChanged();
			DeleteCommand.RaiseCanExecuteChanged();
			ChangeCommand.RaiseCanExecuteChanged();
		}

		public DelegateCommand AddCommand { get; protected set; }
		public DelegateCommand<TEntityViewModel> ChangeCommand { get; protected set; }
		public DelegateCommand DeleteCommand { get; protected set; }

		protected abstract void Add();

		protected abstract bool CanAdd();

		protected virtual void Change(TEntityViewModel entity)
		{
			string validateError;
			if (!entity.CanSaveChange(out validateError))
			{
				_MessageService.ShowMessage(validateError, "Ошибка", image: MessageBoxImage.Error);
				Entities.CancelEdit();
			}
			else
				Entities.CommitEdit();
		}

		protected virtual bool CanChange(TEntityViewModel entity)
		{
			return entity.CanSaveChange();
		}

		protected abstract void Delete();

		protected virtual bool CanDelete()
		{
			return Entities.CurrentItem != null;
		}
	}
}