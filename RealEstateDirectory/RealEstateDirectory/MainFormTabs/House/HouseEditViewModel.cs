﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.MainFormTabs.Common;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.MainFormTabs.House
{
	[NotifyForAll]
	public class HouseEditViewModel : RealEstateEditViewModel<Domain.Entities.House>
	{
		#region Конструктор

		public HouseEditViewModel(IHouseService service, IMessageService messageService,
			IDistrictService districtService, IViewsService viewsService,
			IRealtorService realtorService, IOwnershipService ownershipService,
			IDealVariantService dealVariantService, IWaterSupplyService waterSupplyService,
			ISewageService sewageService, IMaterialService materialService, IConditionService conditionalService)
			: base(service, messageService, districtService, realtorService, ownershipService, dealVariantService, conditionalService)
		{
			_ViewsService = viewsService;
			_WaterSupplyService = waterSupplyService;
			_SewageService = sewageService;
			_MaterialService = materialService;
		}

		#endregion

		#region Инфраструктура

		private readonly IViewsService _ViewsService;
		private readonly IWaterSupplyService _WaterSupplyService;
		private readonly ISewageService _SewageService;
		private readonly IMaterialService _MaterialService;

		#endregion

		#region Сущность

		#region Свойства

		public ListCollectionView WaterSupply { get; set; }
		public ListCollectionView Sewage { get; set; }
		public ListCollectionView Material { get; set; }

		public string PlotSquare { get; set; }
		public string HouseSquare { get; set; }
		public string TotalFloor { get; set; }
		public bool? HasBathhouse { get; set; }
		public bool? HasGarage { get; set; }
		public bool? HasGas { get; set; }

		#endregion

		#region Валидация

		public override string this[string propertyName]
		{
			get
			{
				var baseResult = base[propertyName];
				if (baseResult != null)
					return baseResult;

				if (propertyName == PropertySupport.ExtractPropertyName(() => PlotSquare) && !IsValidAndPositiveDecimal(PlotSquare))
					return "Площадь участка введена некорректно";

				if (propertyName == PropertySupport.ExtractPropertyName(() => HouseSquare) && !IsValidAndPositiveDecimal(HouseSquare))
					return "Площадь дома введена некорректно";

				if (propertyName == PropertySupport.ExtractPropertyName(() => TotalFloor) && !IsValidAndPositiveInt(TotalFloor))
					return "Количество этажей введено некорректно";

				return null;
			}
		}

		protected override IEnumerable<string> ValidableProperties
		{
			get
			{
				foreach (var validatableProperty in base.ValidableProperties)
					yield return validatableProperty;

				yield return PropertySupport.ExtractPropertyName(() => PlotSquare);
				yield return PropertySupport.ExtractPropertyName(() => HouseSquare);
				yield return PropertySupport.ExtractPropertyName(() => TotalFloor);
			}
		}

		#endregion

		#region Взаимодействие с моделью БД

		protected override Domain.Entities.House CreateNewModel()
		{
			var house = new Domain.Entities.House();
			UpdateConcreteModelFromValues(house);
			SetRealEstateValues(house);

			return house;
		}

		protected override void UpdateValuesFromConcreteModel()
		{
			PlotSquare = DbEntity.PlotSquare.HasValue ? DbEntity.PlotSquare.Value.ToString("0.#") : String.Empty;
			HouseSquare = DbEntity.HouseSquare.HasValue ? DbEntity.HouseSquare.Value.ToString("0.#") : String.Empty;
			TotalFloor = DbEntity.TotalFloor.HasValue ? DbEntity.TotalFloor.Value.ToString() : String.Empty;
		}

		protected override void UpdateConcreteModelFromValues(Domain.Entities.House house)
		{
			house.PlotSquare = String.IsNullOrWhiteSpace(PlotSquare) ? null : new decimal?(Decimal.Parse(PlotSquare));
			house.HouseSquare = String.IsNullOrWhiteSpace(HouseSquare) ? null : new decimal?(Decimal.Parse(HouseSquare));
			house.TotalFloor = String.IsNullOrWhiteSpace(TotalFloor) ? null : new int?(Int32.Parse(TotalFloor));
		}

		#endregion

		#endregion

		#region Свойства

		protected Sewage NullSewage = new Sewage("");
		protected WaterSupply NullWaterSupply = new WaterSupply("");

		#endregion

		#region Перегрузки

		protected override void InitCollection()
		{
			Material = new ListCollectionView((new[] {NullMaterial}).Concat(_MaterialService.GetAll()).ToList());
			WaterSupply = new ListCollectionView((new[] {NullWaterSupply}).Concat(_WaterSupplyService.GetAll()).ToList());
			Sewage = new ListCollectionView((new[] {NullSewage}).Concat(_SewageService.GetAll()).ToList());
		}

		protected override void CloseDialog()
		{
			_ViewsService.CloseHouseDialog();
		}

		protected override void OpenDialog()
		{
			_ViewsService.OpenHouseDialog(this);
		}

		#endregion
	}
}
