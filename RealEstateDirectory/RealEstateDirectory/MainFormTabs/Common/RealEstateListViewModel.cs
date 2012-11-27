using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.MainFormTabs.Room;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.MainFormTabs.Common
{
	[NotifyForAll]
	public abstract class RealEstateListViewModel<T> : NotificationObject
		where T : RealEstate, new()
	{

		#region Конструктор

		public RealEstateListViewModel(IRealEstateService<T> service, IMessageService messageService,
		                               IDistrictService districtService, IRealtorService realtorService,
		                               IOwnershipService ownershipService, IDealVariantService dealVariantService,
		                               IServiceLocator serviceLocator)
		{
			_RealEstateService = service;
			_MessageService = messageService;
			_DistrictService = districtService;
			_RealtorService = realtorService;
			_OwnershipService = ownershipService;
			_DealVariantService = dealVariantService;
			_ServiceLocator = serviceLocator;

			AddCommand = new DelegateCommand(Add);
			ChangeCommand = new DelegateCommand(Change);
			DeleteCommand = new DelegateCommand(Delete);
			UpdateCommand = new DelegateCommand(Update);
			ClearFilterCommand = new DelegateCommand(ClearFilter);
			ApplyFilterCommand = new DelegateCommand(ApplyFilter);

			ChangeInGridCommand = new DelegateCommand<RealEstateViewModel<T>>(ChangeInGrid);
			DeleteInGridCommand = new DelegateCommand<RealEstateViewModel<T>>(DeleteInGrid);
		}

		#endregion

		#region Infrastructure

		private readonly IRealEstateService<T> _RealEstateService;
		private readonly IMessageService _MessageService;
		private readonly IDistrictService _DistrictService;
		private readonly IRealtorService _RealtorService;
		private readonly IOwnershipService _OwnershipService;
		private readonly IDealVariantService _DealVariantService;
		protected readonly IServiceLocator _ServiceLocator;

		public void Initialize()
		{
			_RealEstateService.StartSession();
			LoadFiltersData();
			LoadData();
		}

		#endregion

		#region Свойства  INotifi

		public RealEstateViewModel<T> CurrentEntity { get; set; }
		public ObservableCollection<RealEstateViewModel<T>> Entities { get; set; }

		public ListCollectionView DistrictList { get; set; }
		public ListCollectionView StreetList { get; set; }
		public ListCollectionView RealtorList { get; set; }
		public ListCollectionView OwnershipList { get; set; }
		public ListCollectionView DealVariantList { get; set; }

		public decimal? MinPrice { get; set; }
		public decimal? MaxPrice { get; set; }

		public string TerritorialNumber { get; set; }
		public bool SubmitToVDV { get; set; }
		public bool SubmitToDomino { get; set; }
		public decimal? Price { get; set; }
		public int Id { get; set; }
		public bool HasVideo { get; set; }
		public string Description { get; set; }
		public DateTime CreateDate { get; set; }
		public string EntityCountString { get; set; }

		#endregion

		#region Свойства

		protected readonly District _AllDistricts = new District("< все >");
		protected readonly District _NoneDistricts = new District("< не указано >");

		protected readonly Street _AllStreets = new Street("< все >");
		protected readonly Street _NoneStreets = new Street("< не указано >");

		protected readonly Realtor _AllRealtors = new Realtor("< все >");
		protected readonly Realtor _NoneRealtors = new Realtor("< не указано >");

		protected readonly DealVariant _AllDealVariant = new DealVariant("< все >");
		protected readonly DealVariant _NoneDealVariant = new DealVariant("< не указано >");

		protected readonly Ownership _AllOwnership = new Ownership("< все >");
		protected readonly Ownership _NoneOwnership = new Ownership("< не указано >");

		#endregion

		#region Методы

		private void UpdateStreet()
		{
			var district = DistrictList.CurrentItem as District;
			if (district != null)
			{
				StreetList = new ListCollectionView((new[] {_AllStreets, _NoneStreets}).Concat(district.Streets).ToList());
				StreetList.CurrentChanged += (sender, args) =>
					{
						UpdateFilter();
					};
			}
		}

		private void LoadFiltersData()
		{
			DistrictList = null;
			StreetList = null;
			RealtorList = null;
			DealVariantList = null;
			OwnershipList = null;

			DistrictList = new ListCollectionView((new[] {_AllDistricts, _NoneDistricts}).Concat(
				_DistrictService.GetAll()).ToList());

			DistrictList.CurrentChanged += (sender, args) =>
				{
					UpdateStreet();
					UpdateFilter();
				};
			UpdateStreet();

			RealtorList = new ListCollectionView((new[] {_AllRealtors, _NoneRealtors}).Concat(_RealtorService.GetAll()).ToList());
			RealtorList.CurrentChanged += (sender, args) =>
				{
					UpdateFilter();
				};

			DealVariantList =
				new ListCollectionView((new[] {_AllDealVariant, _NoneDealVariant}).Concat(_DealVariantService.GetAll()).ToList());
			DealVariantList.CurrentChanged += (sender, args) =>
				{
					UpdateFilter();
				};
			OwnershipList =
				new ListCollectionView((new[] {_AllOwnership, _NoneOwnership}).Concat(_OwnershipService.GetAll()).ToList());
			OwnershipList.CurrentChanged += (sender, args) =>
				{
					UpdateFilter();
				};
			
			MinPrice = null;
			MaxPrice = null;

			LoadChildFiltersData();
		}

		protected void UpdateFilter()
		{
			var entities = _RealEstateService.GetAll();

			var district = ResolveDictionary<District>(DistrictList);
			if (district != null && district != _AllDistricts)
				entities = district == _NoneDistricts
					           ? entities.Where(room => room.District == null)
					           : entities.Where(room => room.District != null && room.District == district);

			var realtor = ResolveDictionary<Realtor>(RealtorList);
			if (realtor != null && realtor != _AllRealtors)
				entities = realtor == _NoneRealtors
					           ? entities.Where(room => room.Realtor == null)
					           : entities.Where(room => room.Realtor != null && room.Realtor == realtor);

			var dealVariant = ResolveDictionary<DealVariant>(DealVariantList);
			if (dealVariant != null && dealVariant != _AllDealVariant)
				entities = dealVariant == _NoneDealVariant
							   ? entities.Where(room => room.DealVariant == null)
							   : entities.Where(room => room.DealVariant != null && room.DealVariant == dealVariant);

			var ownership = ResolveDictionary<Ownership>(OwnershipList);
			if (ownership != null && ownership != _AllOwnership)
				entities = ownership == _NoneOwnership
							   ? entities.Where(room => room.Ownership == null)
							   : entities.Where(room => room.Ownership != null && room.Ownership == ownership);

			var minPrice = MinPrice ?? -decimal.MaxValue;
			var maxPrice = MaxPrice ?? decimal.MaxValue;
			var needFilter = MinPrice.HasValue && MaxPrice.HasValue;
			entities = needFilter ? entities.Where(room => room.Price.HasValue && room.Price > minPrice && room.Price < maxPrice) : entities;

			entities = UpdateChildFilterData(entities);

			
			Entities = new ObservableCollection<RealEstateViewModel<T>>(entities.Select(CreateNewViewModel).ToArray());
            EntityCountString = String.Format("Всего: {0} предложений", Entities.Count());
		}

		private void LoadData()
		{
			UpdateFilter();
		}

		public D ResolveDictionary<D>(ListCollectionView listView) where D : BaseDictionary
		{
			D dictionary = listView.CurrentItem as D;
			return ResolveDictionary(dictionary);
		}

		private D ResolveDictionary<D>(D dictionary) where D : BaseDictionary
		{
			return dictionary == null ? null : dictionary;
		}

		protected RealEstateViewModel<T> CreateNewViewModel(T model)
		{
			RealEstateViewModel<T> viewModel = GetViewEntityViewModel();
			viewModel.LoadViewModel(model);
			return viewModel;
		}

		protected abstract RealEstateViewModel<T> GetViewEntityViewModel();
		protected abstract RealEstateEditViewModel<T> GetEditEntityViewModel();
		protected abstract IEnumerable<T> UpdateChildFilterData(IEnumerable<T> entities);
		protected abstract void LoadChildFiltersData();

		#endregion

		#region Команды

		public ICommand AddCommand { get; private set; }
		public ICommand ChangeCommand { get; private set; }
		public ICommand ChangeInGridCommand { get; private set; }
		public ICommand DeleteInGridCommand { get; private set; }
		public ICommand DeleteCommand { get; private set; }
		public ICommand UpdateCommand { get; private set; }
		public ICommand ApplyFilterCommand { get; private set; }
		public ICommand ClearFilterCommand { get; private set; }

		protected void Add()
		{
			var roomViewModel = GetEditEntityViewModel();
			roomViewModel.EditEnded += RoomViewModelOnEditEnded;
			roomViewModel.BeginEdit(new T());
		}


		protected void ChangeInGrid(RealEstateViewModel<T> viewModel)
		{
			CurrentEntity = viewModel;
			Change();
		}

		protected void Change()
		{
			if (CurrentEntity != null)
			{
				var roomViewModel = GetEditEntityViewModel();

				roomViewModel.EditEnded += RoomViewModelOnEditEnded;
				roomViewModel.BeginEdit(CurrentEntity.DbEntity);
			}
		}

		protected void DeleteInGrid(RealEstateViewModel<T> viewModel)
		{
			CurrentEntity = viewModel;
			Delete();
		}

		protected void Delete()
		{
			if (CurrentEntity != null)
			{
				string confirmMessage =
					String.Format("Вы уверены что хотите удалить недвижимость по адресу {0}, {1}, {2} риэлтора {3} {4}",
					              CurrentEntity.District != null ? CurrentEntity.District.Name : "",
					              CurrentEntity.Street != null ? CurrentEntity.Street.Name : "",
					              CurrentEntity.TerritorialNumber,
					              CurrentEntity.Realtor.Name,
					              CurrentEntity.Realtor.Phone);

				if (_MessageService.ShowConfirm(confirmMessage, "Подтвердите действие"))
				{
					_RealEstateService.Delete(CurrentEntity.DbEntity);
					Entities.Remove(CurrentEntity);
				}
			}
		}

		protected void Update()
		{
			LoadData();
		}

		protected void ApplyFilter()
		{
			UpdateFilter();
		}

		protected void ClearFilter()
		{
			LoadFiltersData();
			LoadData();
		}

		#endregion

		#region Методы проверки команд

		protected bool CanOk()
		{
			return true;
		}

		protected bool CanCancel()
		{
			return true;
		}

		private void RoomViewModelOnEditEnded(RealEstateEditViewModel<T>.EditEndedMode editEndedMode, T entity)
		{
			if (editEndedMode == RealEstateEditViewModel<T>.EditEndedMode.Add)
			{
				Entities.Add(CreateNewViewModel(entity));
			}
			if (editEndedMode == RealEstateEditViewModel<T>.EditEndedMode.Edit)
			{
				CurrentEntity.LoadViewModel(entity);
			}
		}

		#endregion
	}
}