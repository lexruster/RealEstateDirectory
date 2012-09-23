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

			Entities = new ListCollectionView(_Entities);

			AddCommand = new DelegateCommand(() =>
				{
					var viewModel = CreateNewViewModel(CreateNewModel());
					var error = viewModel.Error;
					if (error == null)
					{
						viewModel.SaveToDatabase();
						_Entities.Add(viewModel);
					}
					else
					{
						_MessageService.ShowMessage(error, "Ошибка", image: MessageBoxImage.Error);
					}
					AddCommand.RaiseCanExecuteChanged();
				}, CanAdd);
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

		protected DataObservableCollection<TEntityViewModel> _Entities = new DataObservableCollection<TEntityViewModel>();

		public ListCollectionView Entities { get; protected set; }

		public DelegateCommand AddCommand { get; protected set; }

		protected abstract bool CanAdd();

		protected abstract TEntity CreateNewModel();

		protected virtual TEntityViewModel CreateNewViewModel(TEntity model)
		{
			var viewModel = _ServiceLocator.GetInstance<TEntityViewModel>();
			viewModel.DbEntity = model;
			viewModel.UpdateValuesFromModel();
			return viewModel;
		}

		protected abstract bool IsCanRemove(TEntityViewModel entity, out string errorText);

		protected abstract void RemoveEntityFromDatabase(TEntity entity);

		private void Entities_CollectionChanging(object sender, CollectionChangingEventArgs<TEntityViewModel> e)
		{
			switch (e.Action)
			{
				case CollectionChangeAction.Remove:
					string cantRemoveError;
					if (!IsCanRemove(e.Item, out cantRemoveError))
					{
						_MessageService.ShowMessage(cantRemoveError, "Ошибка", image: MessageBoxImage.Error);
						e.Cancel = true;
					}
					else
					{
						RemoveEntityFromDatabase(e.Item.DbEntity);
					}
					break;
			}
		}
	}
}