using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection;
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
using Condition = RealEstateDirectory.Domain.Entities.Dictionaries.Condition;

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
									   IOwnershipService ownershipService, IDealVariantService dealVariantService, IConditionService conditionService)
		{
			_RealEstateService = service;
			_MessageService = messageService;
			_DistrictService = districtService;
			_RealtorService = realtorService;
			_OwnershipService = ownershipService;
			_DealVariantService = dealVariantService;
			_ConditionService = conditionService;
			PropertyChanged += (sender, args) =>
            {
                OkCommand.RaiseCanExecuteChanged();
            };

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
		private readonly IConditionService _ConditionService;

		#endregion

		#region Свойства  INotify

		public ListCollectionView District { get; set; }
		public ListCollectionView Street { get; set; }
		public ListCollectionView Realtor { get; set; }
		public ListCollectionView Ownership { get; set; }
		public ListCollectionView DealVariant { get; set; }
		public ListCollectionView Condition { get; set; }

		public string TerritorialNumber { get; set; }
		public bool SubmitToVDV { get; set; }
		public bool SubmitToDomino { get; set; }
		public decimal? Price { get; set; }
		public int Id { get; set; }
		public bool HasVideo { get; set; }
		public string Description { get; set; }
		public DateTime CreateDate { get; set; }

        //Костыли для валидации
        public District CurrentDistrict { get; set; }
        public Realtor CurrentRealtor { get; set; }

		#endregion

		#region Свойства

		public event Action<EditEndedMode, T> EditEnded;
		public T DbEntity;
		//protected District NullDistrict = new District("");
		protected Street NullStreet = new Street("");
		//protected Realtor NullRealtor = new Realtor("");
		protected DealVariant NullDealVariant = new DealVariant("");
		protected Condition NullCondition = new Condition("");
		protected Ownership NullOwnership = new Ownership("");
		protected Terrace NullTerrace = new Terrace("");
		protected Material NullMaterial = new Material("");
		protected Destination NullDestination = new Destination("");
		protected Layout NullLayout = new Layout("");
		protected FloorLevel NullFloorLevel = new FloorLevel("");

		#endregion

		#region Методы

		public virtual void BeginEdit(T room)
		{
			District = new ListCollectionView(_DistrictService.GetAll().ToList());
            Street = new ListCollectionView((new[] { NullStreet }).ToList());
			Realtor = new ListCollectionView(_RealtorService.GetAll().ToList());
			Ownership = new ListCollectionView((new[] {NullOwnership}).Concat(_OwnershipService.GetAll()).ToList());
			DealVariant = new ListCollectionView((new[] {NullDealVariant}).Concat(_DealVariantService.GetAll()).ToList());
			Condition = new ListCollectionView((new[] { NullCondition }).Concat(_ConditionService.GetAll()).ToList());
			InitCollection();

			DbEntity = room;
			UpdateValuesFromModel();

			District.CurrentChanged += (sender, args) =>
				{
					UpdateStreet();
				};
      
		    OkCommand.RaiseCanExecuteChanged();
			OpenDialog();
		}

		private void UpdateStreet()
		{
			var district = District.CurrentItem as District;
			if (district != null)
			{
				Street = new ListCollectionView((new[] {NullStreet}).Concat(district.Streets).ToList());
				if (district.Equals(DbEntity.District))
				{
					Street.MoveCurrentTo(DbEntity.Street);
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
			District.MoveCurrentTo(DbEntity.District);
			if (DbEntity.District != null)
			{
				Street = new ListCollectionView((new[] {NullStreet}).Concat(DbEntity.District.Streets).ToList());
				Street.MoveCurrentTo(DbEntity.Street);
			}
			Id = DbEntity.Id;
			CreateDate = DbEntity.CreateDate;
			DealVariant.MoveCurrentTo(DbEntity.DealVariant);
			Condition.MoveCurrentTo(DbEntity.Condition);
			Description = DbEntity.Description;
			HasVideo = DbEntity.HasVideo;
			Id = DbEntity.Id;
			Ownership.MoveCurrentTo(DbEntity.Ownership);
			Price = DbEntity.Price;
			Realtor.MoveCurrentTo(DbEntity.Realtor);
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
			return dictionary == null || String.IsNullOrEmpty(dictionary.Name) ? null : dictionary;
		}

		public void OnEditEnded(EditEndedMode mode, T entity)
		{
			Action<EditEndedMode, T> handler = EditEnded;
			if (handler != null) handler(mode, entity);
		}

		public void SetRealEstateValues(T entity)
		{
			entity.District = ResolveDictionary<District>(District);
			entity.Street = ResolveDictionary<Street>(Street);

			entity.DealVariant = ResolveDictionary<DealVariant>(DealVariant);
			entity.Condition = ResolveDictionary<Condition>(Condition);
			entity.Description = Description;
			entity.HasVideo = HasVideo;
			entity.Ownership = ResolveDictionary<Ownership>(Ownership);
			entity.Price = Price;
			entity.Realtor = ResolveDictionary<Realtor>(Realtor);
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
            return IsValid();
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

                if (propertyName == PropertySupport.ExtractPropertyName(() => CurrentRealtor))
                {
                    if (CurrentRealtor == null)
                        return "Риэлтор должен быть указан";
                }

                if (propertyName == PropertySupport.ExtractPropertyName(() => CurrentDistrict))
                {
                    if (CurrentDistrict == null)
                        return "Район должен быть указан";
                }

				var childDataError = ChildDataError(propertyName);
				if (!String.IsNullOrEmpty(childDataError))
				{
					return childDataError;
				}

				return null;
			}
		}

        public string ValidateAll()
        {
            var reasons = new List<string>();
            var trtr = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var prop in this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var curStr = this[prop.Name];
                if (!String.IsNullOrEmpty(curStr))
                {
                    reasons.Add(curStr);
                }
            }

            return string.Join("; ", reasons);
        }

        public bool IsValid()
        {
            if(String.IsNullOrEmpty(ValidateAll()))
            {
                return true;
            }
            return false;
        }

		public string Error
		{
			get
			{
			    var validateResult = ValidateAll();
                if(!String.IsNullOrEmpty(validateResult))
                {
                    return validateResult;
                }

				var entity = CreateNewModel();
				var validation = _RealEstateService.IsValid(entity, Id);
				return validation.IsValid ? null : validation.GetReasons();
			}
		}

		#endregion

		#endregion
	}
}