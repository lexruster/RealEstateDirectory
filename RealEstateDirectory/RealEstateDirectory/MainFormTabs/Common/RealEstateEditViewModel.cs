using System;
using System.Linq;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.MainFormTabs.Common
{
	[NotifyForAll]
	public abstract class RealEstateEditViewModel<T> : NotificationObject, IDataErrorInfo
		where T : RealEstate, new()
	{
		public enum EditEndedMode
		{
			Cancel = 1,
			Add = 2,
			Edit = 3
		}

		#region Конструктор

		public RealEstateEditViewModel(IRealEstateService<T> service, IMessageService messageService,
		                               IDistrictService districtService, IRealtorService realtorService,
		                               IOwnershipService ownershipService, IDealVariantService dealVariantService)
		{
			_RealEstateService = service;
			_MessageService = messageService;
			_DistrictService = districtService;
			_RealtorService = realtorService;
			_OwnershipService = ownershipService;
			_DealVariantService = dealVariantService;

			OkCommand = new DelegateCommand(() =>
				{
					var mode = Id == 0 ? EditEndedMode.Add : EditEndedMode.Edit;
					var error = Error;
					if (error == null)
					{
						UpdateModelFromValues();
						SaveToDatabase();
						CloseDialog();
						OnEditEnded(mode, DbEntity);
					}
					else
					{
						_MessageService.ShowMessage(error, "Ошибка", image: MessageBoxImage.Error);
					}
				}, CanOk);

			CancelCommand = new DelegateCommand(() =>
				{
					UpdateValuesFromModel();
					CloseDialog();
					OnEditEnded(EditEndedMode.Cancel, null);
				}, CanCancel);
		}

		#endregion

		#region Infrastructure

		private readonly IRealEstateService<T> _RealEstateService;
		private readonly IMessageService _MessageService;
		private readonly IDistrictService _DistrictService;
		private readonly IRealtorService _RealtorService;
		private readonly IOwnershipService _OwnershipService;
		private readonly IDealVariantService _DealVariantService;

		#endregion

		#region Свойства  INotifi

		public ListCollectionView DistrictList { get; set; }
		public ListCollectionView StreetList { get; set; }
		public ListCollectionView RealtorList { get; set; }
		public ListCollectionView OwnershipList { get; set; }
		public ListCollectionView DealVariantList { get; set; }

		public string TerritorialNumber { get; set; }
		public bool SubmitToVDV { get; set; }
		public bool SubmitToDomino { get; set; }
		public decimal? Price { get; set; }
		public int Id { get; set; }
		public bool HasVideo { get; set; }
		public string Description { get; set; }
		public DateTime CreateDate { get; set; }

		#endregion

		#region Свойства

		public event Action<EditEndedMode, T> EditEnded;
		public T DbEntity;
		protected District NullDistrict = new District("");
		protected Street NullStreet = new Street("");
		protected Realtor NullRealtor = new Realtor("");
		protected DealVariant NullDealVariant = new DealVariant("");
		protected Ownership NullOwnership = new Ownership("");
		protected Terrace NullTerrace = new Terrace("");
		protected Material NullMaterial = new Material("");
		protected Layout NullLayout = new Layout("");
		protected FloorLevel NullFloorLevel = new FloorLevel("");

		#endregion

		#region Методы

		public virtual void BeginEdit(T room)
		{
			DistrictList = new ListCollectionView((new[] {NullDistrict}).Concat(_DistrictService.GetAll()).ToList());
			DistrictList = new ListCollectionView((new[] {NullDistrict}).Concat(_DistrictService.GetAll()).ToList());
			RealtorList = new ListCollectionView((new[] {NullRealtor}).Concat(_RealtorService.GetAll()).ToList());
			OwnershipList = new ListCollectionView((new[] {NullOwnership}).Concat(_OwnershipService.GetAll()).ToList());
			DealVariantList =
				new ListCollectionView((new[] {NullDealVariant}).Concat(_DealVariantService.GetAll()).ToList());
			InitCollection();

			DbEntity = room;
			UpdateValuesFromModel();

			DistrictList.CurrentChanged += (sender, args) =>
				{
					UpdateStreet();
				};

			OpenDialog();
		}

		private void UpdateStreet()
		{
			var district = DistrictList.CurrentItem as District;
			if (district != null)
			{
				StreetList = new ListCollectionView((new[] {NullStreet}).Concat(district.Streets).ToList());
				if (district.Equals(DbEntity.District))
				{
					StreetList.MoveCurrentTo(DbEntity.Street);
				}
			}
		}

		protected void UpdateModelFromValues()
		{
			SetRealEstateValues(DbEntity);
			UpdateConcreteModelFromValues();
		}

		public void LoadViewModel(T entity)
		{
			DbEntity = entity;
			UpdateValuesFromModel();
		}

		public void UpdateValuesFromModel()
		{
			DistrictList.MoveCurrentTo(DbEntity.District);
			if (DbEntity.District != null)
			{
				StreetList = new ListCollectionView((new[] {NullStreet}).Concat(DbEntity.District.Streets).ToList());
				StreetList.MoveCurrentTo(DbEntity.Street);
			}
			Id = DbEntity.Id;
			CreateDate = DbEntity.CreateDate;
			DealVariantList.MoveCurrentTo(DbEntity.DealVariant);
			Description = DbEntity.Description;
			HasVideo = DbEntity.HasVideo;
			Id = DbEntity.Id;
			OwnershipList.MoveCurrentTo(DbEntity.Ownership);
			Price = DbEntity.Price;
			RealtorList.MoveCurrentTo(DbEntity.Realtor);
			SubmitToDomino = DbEntity.SubmitToDomino;
			SubmitToVDV = DbEntity.SubmitToVDV;
			TerritorialNumber = DbEntity.TerritorialNumber;
			UpdateValuesFromConcreteModel();
		}

		protected abstract void UpdateValuesFromConcreteModel();
		protected abstract void UpdateConcreteModelFromValues();
		protected abstract void CloseDialog();
		protected abstract void OpenDialog();

		public void SaveToDatabase()
		{
			_RealEstateService.Save(DbEntity);
			UpdateValuesFromModel();
		}

		protected abstract T CreateNewModel();
		protected abstract string ChildDataError(string propertyName);
		protected abstract void InitCollection();

		public D ResolveDictionary<D>(ListCollectionView listView) where D : BaseDictionary
		{
			D dictionary = listView.CurrentItem as D;
			return ResolveDictionary(dictionary);
		}

		private D ResolveDictionary<D>(D dictionary) where D : BaseDictionary
		{
			return dictionary == null || dictionary.Equals(ResolveNullDictionary(dictionary)) ? null : dictionary;
		}

		private object ResolveNullDictionary<D>(D dictionary) where D : BaseDictionary
		{
			switch (typeof (D).Name)
			{
				case "District":
					return NullDistrict;
					break;
			}
			return null;
		}

		public void OnEditEnded(EditEndedMode mode, T entity)
		{
			Action<EditEndedMode, T> handler = EditEnded;
			if (handler != null) handler(mode, entity);
		}

		public void SetRealEstateValues(T entity)
		{
			entity.District = ResolveDictionary<District>(DistrictList);
			entity.Street = ResolveDictionary<Street>(StreetList);

			entity.DealVariant = ResolveDictionary<DealVariant>(DealVariantList);
			entity.Description = Description;
			entity.HasVideo = HasVideo;
			entity.Ownership = ResolveDictionary<Ownership>(OwnershipList);
			entity.Price = Price;
			entity.Realtor = ResolveDictionary<Realtor>(RealtorList);
			entity.SubmitToDomino = SubmitToDomino;
			entity.SubmitToVDV = SubmitToVDV;
			entity.TerritorialNumber = TerritorialNumber;
		}

		#endregion

		#region Команды

		public DelegateCommand OkCommand { get; protected set; }
		public DelegateCommand CancelCommand { get; protected set; }

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

		#endregion

		#region Перегрузки


		#region IDataErrorInfo

		public string this[string propertyName]
		{
			get
			{
				if (propertyName == PropertySupport.ExtractPropertyName(() => Price))
				{
					if (Price < 0)
						return "Цена не может быть отрицательной";
				}

				var childDataError = ChildDataError(propertyName);
				if (!String.IsNullOrEmpty(childDataError))
				{
					return childDataError;
				}

				return null;
			}
		}

		public string Error
		{
			get
			{
				var entity = CreateNewModel();

				var validation = _RealEstateService.IsValid(entity, Id);
				return validation.IsValid ? null : validation.GetReasons();
			}
		}

		#endregion

		#endregion
	}
}