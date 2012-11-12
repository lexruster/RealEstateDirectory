using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Interfaces;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.Common
{
	[NotifyForAll]
	public abstract class RealEstateListViewModel<TEntityViewModel, TEntity> : NotificationObject, ISessionedViewModel
		where TEntity : RealEstate
		where TEntityViewModel : RealEstateEntityViewModel<TEntity>
	{
		protected RealEstateListViewModel(IServiceLocator serviceLocator, IMessageService messageService)
		{
			_ServiceLocator = serviceLocator;
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
						ClearProperties();
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
			OpenSession();
			InitializeEntities();
			_Entities.CollectionChanging += Entities_CollectionChanging;
		}

		protected readonly IServiceLocator _ServiceLocator;

		protected readonly IMessageService _MessageService;

		#endregion

		protected abstract void InitializeEntities();

		protected EnhancedObservableCollection<TEntityViewModel> _Entities =
			new EnhancedObservableCollection<TEntityViewModel>();

		public abstract string RealEstateListName { get; }

		public ListCollectionView Entities { get; protected set; }

		public DelegateCommand AddCommand { get; protected set; }

		protected abstract bool CanAdd();

		protected abstract void ClearProperties();

		protected abstract TEntity CreateNewModel();

		protected virtual TEntityViewModel CreateNewViewModel(TEntity model)
		{
			var viewModel = _ServiceLocator.GetInstance<TEntityViewModel>();
			viewModel.InitMessageService(_MessageService);
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

		public abstract void OpenSession();

		public abstract void CloseSession();
	}
}