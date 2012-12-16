using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Misc.Miscellaneous;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.MainFormTabs.Room;
using RealEstateDirectory.Services;
using RealEstateDirectory.Services.Export;

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
									   IConditionService conditionService,
		                               IExcelService excelService, IWordService wordService, IServiceLocator serviceLocator)
		{
			_RealEstateService = service;
			_MessageService = messageService;
			_DistrictService = districtService;
			_RealtorService = realtorService;
			_OwnershipService = ownershipService;
			_DealVariantService = dealVariantService;
			_ConditionService = conditionService;
			_ServiceLocator = serviceLocator;
			_excelService = excelService;
			_wordService = wordService;

			AddCommand = new DelegateCommand(Add);
			ChangeCommand = new DelegateCommand(Change);
			DeleteCommand = new DelegateCommand(Delete);
			UpdateCommand = new DelegateCommand(Update);
			ClearFilterCommand = new DelegateCommand(ClearFilter);
			ApplyFilterCommand = new DelegateCommand(ApplyFilter);
			ExportToExcelCommand = new DelegateCommand(ExportToExcel);
			ExportToWordCommand = new DelegateCommand(ExportToWord);
			ExportToWordSelectedCommand = new DelegateCommand<object>(ExportToWordSelected);

			ChangeInGridCommand = new DelegateCommand<RealEstateViewModel<T>>(ChangeInGrid);
			DeleteInGridCommand = new DelegateCommand<RealEstateViewModel<T>>(DeleteInGrid);
		}

		#endregion

		#region Infrastructure

		protected IRealEstateService<T> _RealEstateService;
		private readonly IMessageService _MessageService;
		private readonly IDistrictService _DistrictService;
		private readonly IRealtorService _RealtorService;
		private readonly IOwnershipService _OwnershipService;
		private readonly IDealVariantService _DealVariantService;
		private readonly IConditionService _ConditionService;
		protected readonly IServiceLocator _ServiceLocator;
		private readonly IExcelService _excelService;
		private readonly IWordService _wordService;

		public void Initialize()
		{
			_RealEstateService.StartSession();
			LoadFiltersData();
			InitFilters();
			LoadData();
			_FilterEnabled = true;
		}

		#endregion

		#region Свойства  INotify

		public RealEstateViewModel<T> CurrentEntity { get; set; }
		public IList<RealEstateViewModel<T>> SelectedEntities { get; set; }
		public ObservableCollection<RealEstateViewModel<T>> Entities { get; set; }
		public string EntityCountString { get; set; }

		#endregion

		#region Фильтры

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

		protected readonly Terrace _AllTerrace = new Terrace("< все >");
		protected readonly Terrace _ExistTerrace = new Terrace("Есть балкон");

		/// <summary>
		/// Для сброса фильтров, чтобы каждый фильтр не вызывал перерисовку
		/// </summary>
		protected bool _FilterEnabled;

		public District FilterDistrict { get; set; }
		public Street FilterStreet { get; set; }
		public Realtor FilterRealtor { get; set; }
		public Ownership FilterOwnership { get; set; }
		public DealVariant FilterDealVariant { get; set; }
		public Terrace FilterTerrace { get; set; }

		public ObservableCollection<District> DistrictList { get; set; }
		public ObservableCollection<Street> StreetList { get; set; }
		public ObservableCollection<Realtor> RealtorList { get; set; }
		public ObservableCollection<Ownership> OwnershipList { get; set; }
		public ObservableCollection<DealVariant> DealVariantList { get; set; }
		public ObservableCollection<Terrace> TerraceList { get; set; }

		public decimal? MinPrice { get; set; }
		public decimal? MaxPrice { get; set; }

		private void LoadFiltersData()
		{
			DistrictList = null;
			StreetList = null;
			RealtorList = null;
			DealVariantList = null;
			OwnershipList = null;

			DistrictList = new ObservableCollection<District>((new[] { _AllDistricts, _NoneDistricts }).Concat(
				_DistrictService.GetAll()).ToList());
			FilterDistrict = _AllDistricts;
			UpdateStreet();

			RealtorList =
				new ObservableCollection<Realtor>((new[] { _AllRealtors, _NoneRealtors }).Concat(_RealtorService.GetAll()).ToList());
			FilterRealtor = _AllRealtors;

			DealVariantList =
				new ObservableCollection<DealVariant>(
					(new[] { _AllDealVariant, _NoneDealVariant }).Concat(_DealVariantService.GetAll()).ToList());
			FilterDealVariant = _AllDealVariant;

			OwnershipList =
				new ObservableCollection<Ownership>(
					(new[] { _AllOwnership, _NoneOwnership }).Concat(_OwnershipService.GetAll()).ToList());
			FilterOwnership = _AllOwnership;

			TerraceList =
				new ObservableCollection<Terrace>(
					(new[] { _AllTerrace, _ExistTerrace }).ToList());
			FilterTerrace = _AllTerrace;

			MinPrice = null;
			MaxPrice = null;

			LoadChildFiltersData();
		}

		protected void ApplyFilter()
		{
			UpdateEntityList();
		}

		protected void ClearFilter()
		{
			_FilterEnabled = false;
			LoadFiltersData();
			_FilterEnabled = true;
			UpdateEntityList();
		}

		#endregion 

		#region Методы

		private void UpdateStreet()
		{
			var district = FilterDistrict;
			if (district != null)
			{
				StreetList = new ObservableCollection<Street>((new[] {_AllStreets, _NoneStreets}).Concat(district.Streets).ToList());
				FilterStreet = _AllStreets;
			}
		}

		private void InitFilters()
		{
			PropertyChanged += (sender, args) =>
				{
					if (_FilterEnabled)
					{
						if (args.PropertyName == PropertySupport.ExtractPropertyName(() => FilterDealVariant)
						    || args.PropertyName == PropertySupport.ExtractPropertyName(() => FilterOwnership)
						    || args.PropertyName == PropertySupport.ExtractPropertyName(() => FilterRealtor)
						    || args.PropertyName == PropertySupport.ExtractPropertyName(() => FilterStreet)
							|| args.PropertyName == PropertySupport.ExtractPropertyName(() => FilterTerrace))
							UpdateEntityList();

						if (args.PropertyName == PropertySupport.ExtractPropertyName(() => FilterDistrict))
						{
							UpdateStreet();
							UpdateEntityList();
						}
					}
				};
		}

		protected void UpdateEntityList()
		{
			var entities = _RealEstateService.GetAll();

			var district = FilterDistrict;
			if (district != null && !Equals(district, _AllDistricts))
				entities = Equals(district, _NoneDistricts)
					           ? entities.Where(room => room.District == null)
					           : entities.Where(room => room.District != null && Equals(room.District, district));

			var street = FilterStreet;
			if (street != null && !Equals(street, _AllStreets))
				entities = Equals(street, _NoneStreets)
					           ? entities.Where(room => room.Street == null)
					           : entities.Where(room => room.Street != null && Equals(room.Street, street));

			var realtor = FilterRealtor;
			if (realtor != null && !Equals(realtor, _AllRealtors))
				entities = Equals(realtor, _NoneRealtors)
					           ? entities.Where(room => room.Realtor == null)
					           : entities.Where(room => room.Realtor != null && Equals(room.Realtor, realtor));

			var dealVariant = FilterDealVariant;
			if (dealVariant != null && !Equals(dealVariant, _AllDealVariant))
				entities = Equals(dealVariant, _NoneDealVariant)
					           ? entities.Where(room => room.DealVariant == null)
					           : entities.Where(room => room.DealVariant != null && Equals(room.DealVariant, dealVariant));

			var ownership = FilterOwnership;
			if (ownership != null && !Equals(ownership, _AllOwnership))
				entities = Equals(ownership, _NoneOwnership)
					           ? entities.Where(room => room.Ownership == null)
					           : entities.Where(room => room.Ownership != null && Equals(room.Ownership, ownership));

			var minPrice = MinPrice ?? -decimal.MaxValue;
			var maxPrice = MaxPrice ?? decimal.MaxValue;
			var needFilter = MinPrice.HasValue && MaxPrice.HasValue;
			entities = needFilter
				           ? entities.Where(room => room.Price.HasValue && room.Price > minPrice && room.Price < maxPrice)
				           : entities;

			entities = UpdateChildFilterData(entities);

			Entities = new ObservableCollection<RealEstateViewModel<T>>(entities.Select(CreateNewViewModel).ToArray());
			EntityCountString = String.Format("Всего: {0} предложений", Entities.Count());
		}

		private void LoadData()
		{
			UpdateEntityList();
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
		public abstract ExportTable GetExportedTable(ExportMode mode);

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
		public ICommand ExportToExcelCommand { get; private set; }
		public ICommand ExportToWordCommand { get; private set; }
		public ICommand ExportToWordSelectedCommand { get; private set; }
		
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
			_FilterEnabled = false;
			LoadFiltersData();
			_FilterEnabled = true;
			UpdateEntityList();
		}
		
		protected void ExportToExcel()
		{
			var data = GetExportedTable(ExportMode.Selected);
			var obj = new ExportObject();
			obj.Tables.Add(data);
			_excelService.ExportToExcel(obj);
		}

		protected void ExportToWordSelected(object selectionObject)
		{
			List<RealEstateViewModel<T>> selection = selectionObject as List<RealEstateViewModel<T>>;
			//var t = ((System.Windows.Controls.SelectedItemCollection)selectionObject).Count;
			//foreach (var t1 in selectionObject as RealEstateViewModel<T>[])
			//{
			//	var yy = t1;
			//}

			if (selection == null || selection.Count == 0)
			{
				_MessageService.ShowMessage("Не выбраны объекты", "Ошибка", image: MessageBoxImage.Error);
			}
			else
			{
				SelectedEntities = selection;
				var data = GetExportedTable(ExportMode.Selected);
				var obj = new ExportObject();
				obj.Tables.Add(data);
				_wordService.ExportToWord(obj);
			}
		}

		protected void ExportToWord()
		{
			var data = GetExportedTable(ExportMode.Filtered);
			var obj = new ExportObject();
			obj.Tables.Add(data);
			_wordService.ExportToWord(obj);
		}

		protected List<RealEstateViewModel<T>> GetCollectionForExport(ExportMode mode)
		{
			var collection = new List<RealEstateViewModel<T>>();
			switch (mode)
			{
				case ExportMode.Selected:
					collection = SelectedEntities.ToList();
					break;

				case ExportMode.All:
					collection = _RealEstateService.GetAll().Select(CreateNewViewModel).ToList();
					break;

				case ExportMode.Filtered:
					collection = Entities.ToList();
					break;
			}
		
			return collection;
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

		#region Helper

		public string GetBaseDictionaryName(BaseDictionary dictionary)
		{
			return dictionary == null ? "" : dictionary.Name;
		}

		#endregion
	}
}