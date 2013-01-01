using System;
using System.Collections.Generic;
using System.Globalization;
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
					var mode = _Id == 0 ? EditEndedMode.Add : EditEndedMode.Edit;
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

		#region Свойства сущности

		private int _Id;

		public ListCollectionView District { get; set; }
		public ListCollectionView Street { get; set; }
		public ListCollectionView Realtor { get; set; }
		public ListCollectionView Ownership { get; set; }
		public ListCollectionView DealVariant { get; set; }
		public ListCollectionView Condition { get; set; }

		public string TerritorialNumber { get; set; }
		public string Price { get; set; }
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

			District.CurrentChanged += (sender, args) => UpdateStreet();
      
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
			UpdateConcreteModelFromValues(DbEntity);
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
			_Id = DbEntity.Id;
			CreateDate = DbEntity.CreateDate;
			DealVariant.MoveCurrentTo(DbEntity.DealVariant);
			Condition.MoveCurrentTo(DbEntity.Condition);
			Description = DbEntity.Description;
			HasVideo = DbEntity.HasVideo;
			Ownership.MoveCurrentTo(DbEntity.Ownership);
			Price = DbEntity.Price.HasValue ? DbEntity.Price.Value.ToString(NumberFormatInfo.CurrentInfo) : null;
			Realtor.MoveCurrentTo(DbEntity.Realtor);
			TerritorialNumber = DbEntity.TerritorialNumber;
			UpdateValuesFromConcreteModel();
		}

		protected abstract void UpdateValuesFromConcreteModel();
		protected abstract void UpdateConcreteModelFromValues(T databaseModel);
		protected abstract void CloseDialog();
		protected abstract void OpenDialog();

		public void SaveToDatabase()
		{
			_RealEstateService.Save(DbEntity);
			UpdateValuesFromModel();
		}

		protected abstract T CreateNewModel();
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
			entity.Price = String.IsNullOrWhiteSpace(Price) ? null : new decimal?(Decimal.Parse(Price));
			entity.Realtor = ResolveDictionary<Realtor>(Realtor);
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
            return !String.IsNullOrEmpty(Error);
		}

		protected bool CanCancel()
		{
			return true;
		}

		#endregion

		#region Валидация

		public virtual string this[string propertyName]
		{
			get
			{
				if (propertyName == PropertySupport.ExtractPropertyName(() => Price) && !String.IsNullOrWhiteSpace(Price))
				{
					decimal price;
					if (!Decimal.TryParse(Price, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, NumberFormatInfo.CurrentInfo, out price))
						return "Цена указана некорректно";
				}

				if (propertyName == PropertySupport.ExtractPropertyName(() => CurrentRealtor) && CurrentRealtor == null)
                        return "Риэлтор должен быть указан";

				if (propertyName == PropertySupport.ExtractPropertyName(() => CurrentDistrict) && CurrentDistrict == null)
                        return "Район должен быть указан";

				return null;
			}
		}

		protected virtual IEnumerable<string> ValidatableProperties
        {
			get
                {
				yield return PropertySupport.ExtractPropertyName(() => Price);
				yield return PropertySupport.ExtractPropertyName(() => CurrentRealtor);
				yield return PropertySupport.ExtractPropertyName(() => CurrentDistrict);
                }
            }

		public virtual string Error
        {
			get
            {
				var validateResult = String.Join(Environment.NewLine, ValidatableProperties.Select(propertyName => this[propertyName]).Where(propertyError => propertyError != null));

                if(!String.IsNullOrEmpty(validateResult))
                    return validateResult;

				var entity = CreateNewModel();
				var validation = _RealEstateService.IsValid(entity, _Id);
				return validation.IsValid ? null : validation.GetReasons();
			}
		}

		#endregion
	}
}