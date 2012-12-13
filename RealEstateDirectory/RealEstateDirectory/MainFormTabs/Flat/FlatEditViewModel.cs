using System;
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

namespace RealEstateDirectory.MainFormTabs.Flat
{
    [NotifyForAll]
    public class FlatEditViewModel : RealEstateEditViewModel<Domain.Entities.Flat>
    {
        #region Конструктор

        public FlatEditViewModel(IFlatService service, IMessageService messageService,
                                 IDistrictService districtService, IViewsService viewsService,
                                 IRealtorService realtorService, IOwnershipService ownershipService,
                                 IDealVariantService dealVariantService,
                                 ITerraceService terraceService, IMaterialService materialService,
                                 ILayoutService layoutService, IFloorLevelService floorLevelService, IToiletTypeService toiletTypeService)
            : base(service, messageService, districtService, realtorService, ownershipService, dealVariantService)
        {
            _ViewsService = viewsService;
            _TerraceService = terraceService;
            _MaterialService = materialService;
            _LayoutService = layoutService;
            _FloorLevelService = floorLevelService;
            _ToiletTypeService = toiletTypeService;
        }

	    #endregion

        #region Инфраструктура

        private readonly IViewsService _ViewsService;
        private readonly ITerraceService _TerraceService;
        private readonly IMaterialService _MaterialService;
        private readonly ILayoutService _LayoutService;
        private readonly IFloorLevelService _FloorLevelService;
        private readonly IToiletTypeService _ToiletTypeService;

        #endregion

		#region Сущность

		#region Свойства

		public ListCollectionView Terrace { get; set; }
		public ListCollectionView Material { get; set; }
		public ListCollectionView Layout { get; set; }
		public ListCollectionView FloorLevel { get; set; }
		public ListCollectionView ToiletType { get; set; }

	    public string TotalRoomCount { get; set; }

	    [NotifyProperty(AlsoNotifyFor = new[] { "KitchenSquare", "ResidentialSquare" })]
		public string TotalSquare { get; set; }

	    [NotifyProperty(AlsoNotifyFor = new[] { "TotalSquare", "ResidentialSquare" })]
		public string KitchenSquare { get; set; }

	    [NotifyProperty(AlsoNotifyFor = new[] { "KitchenSquare", "TotalSquare" })]
		public string ResidentialSquare { get; set; }

		[NotifyProperty(AlsoNotifyFor = new[] { "Floor" })]
		public string TotalFloor { get; set; }

		[NotifyProperty(AlsoNotifyFor = new[] { "TotalFloor" })]
		public string Floor { get; set; }

        #endregion

		#region Валидация

		public override string this[string propertyName]
		{
			get
			{
				var baseResult = base[propertyName];
				if (baseResult != null)
					return baseResult;

				if (propertyName == PropertySupport.ExtractPropertyName(() => TotalSquare) && !String.IsNullOrWhiteSpace(TotalSquare))
				{
					decimal totalSquare;
					if (!Decimal.TryParse(TotalSquare, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, NumberFormatInfo.CurrentInfo, out totalSquare))
						return "Общая площадь введена некорректно";
				}

				if (propertyName == PropertySupport.ExtractPropertyName(() => KitchenSquare) && !String.IsNullOrWhiteSpace(KitchenSquare))
				{
					decimal kitchenSquare;
					if (!Decimal.TryParse(KitchenSquare, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, NumberFormatInfo.CurrentInfo, out kitchenSquare))
						return "Площадь кухни введена некорректно";
				}

				if (propertyName == PropertySupport.ExtractPropertyName(() => ResidentialSquare) && !String.IsNullOrWhiteSpace(ResidentialSquare))
				{
					decimal residentialSquare;
					if (!Decimal.TryParse(ResidentialSquare, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, NumberFormatInfo.CurrentInfo, out residentialSquare))
						return "Жилая площадь введена некорректно";
				}

				if ((propertyName == PropertySupport.ExtractPropertyName(() => TotalSquare)
					|| propertyName == PropertySupport.ExtractPropertyName(() => KitchenSquare)
					|| propertyName == PropertySupport.ExtractPropertyName(() => ResidentialSquare))
					&& !String.IsNullOrWhiteSpace(TotalSquare) && !String.IsNullOrWhiteSpace(KitchenSquare) && !String.IsNullOrWhiteSpace(ResidentialSquare))
				{
					var totalSquare = Decimal.Parse(TotalSquare);
					var kitchenSquare = Decimal.Parse(KitchenSquare);
					var residentialSquare = Decimal.Parse(ResidentialSquare);
					if (totalSquare < kitchenSquare + residentialSquare)
						return "Общая площадь не может быть меньше суммы жилой и площади кухни";
				}

				if (propertyName == PropertySupport.ExtractPropertyName(() => TotalRoomCount) && !String.IsNullOrWhiteSpace(TotalRoomCount))
				{
					int totalRoomCount;
					if (!Int32.TryParse(TotalRoomCount, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.CurrentInfo, out totalRoomCount))
						return "Количество комнат введено некорректно";
				}

				if (propertyName == PropertySupport.ExtractPropertyName(() => TotalFloor) && !String.IsNullOrWhiteSpace(TotalFloor))
				{
					int totalFloor;
					if (!Int32.TryParse(TotalFloor, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.CurrentInfo, out totalFloor))
						return "Количество этажей введено некорректно";
				}

				if (propertyName == PropertySupport.ExtractPropertyName(() => Floor) && !String.IsNullOrWhiteSpace(Floor))
				{
					int floor;
					if (!Int32.TryParse(Floor, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.CurrentInfo, out floor))
						return "Этаж введен некорректно";
				}

				if ((propertyName == PropertySupport.ExtractPropertyName(() => TotalFloor)
					|| propertyName == PropertySupport.ExtractPropertyName(() => Floor))
					&& !String.IsNullOrWhiteSpace(TotalFloor) && !String.IsNullOrWhiteSpace(Floor))
				{
					var totalFloor = Int32.Parse(TotalFloor);
					var floor = Int32.Parse(Floor);
					if (floor > totalFloor)
						return "Этаж не может быть больше общего количества этажй";
				}

				return null;
			}
		}

		protected override IEnumerable<string> ValidatableProperties
		{
			get
			{
				foreach (var validatableProperty in base.ValidatableProperties)
					yield return validatableProperty;

				yield return PropertySupport.ExtractPropertyName(() => TotalSquare);
				yield return PropertySupport.ExtractPropertyName(() => ResidentialSquare);
				yield return PropertySupport.ExtractPropertyName(() => KitchenSquare);
				yield return PropertySupport.ExtractPropertyName(() => TotalRoomCount);
				yield return PropertySupport.ExtractPropertyName(() => Floor);
				yield return PropertySupport.ExtractPropertyName(() => TotalFloor);
			}
		}

		#endregion

		#region Взаимодействие с моделью БД

		protected override Domain.Entities.Flat CreateNewModel()
		{
			var flat = new Domain.Entities.Flat();
			UpdateConcreteModelFromValues(flat);
			SetRealEstateValues(flat);

			return flat;
		}

		protected override void UpdateValuesFromConcreteModel()
		{
			TotalSquare = DbEntity.TotalSquare.HasValue ? DbEntity.TotalSquare.Value.ToString("0:0.#") : String.Empty;
			KitchenSquare = DbEntity.KitchenSquare.HasValue ? DbEntity.KitchenSquare.Value.ToString("0:0.#") : String.Empty;
			ResidentialSquare = DbEntity.ResidentialSquare.HasValue ? DbEntity.ResidentialSquare.Value.ToString("0:0.#") : String.Empty;
			TotalRoomCount = DbEntity.TotalRoomCount.HasValue ? DbEntity.TotalRoomCount.Value.ToString() : String.Empty;
			TotalFloor = DbEntity.TotalFloor.HasValue ? DbEntity.TotalFloor.Value.ToString() : String.Empty;
			Floor = DbEntity.Floor.HasValue ? DbEntity.Floor.Value.ToString() : String.Empty;
			Terrace.MoveCurrentTo(DbEntity.Terrace);
			Material.MoveCurrentTo(DbEntity.Material);
			Layout.MoveCurrentTo(DbEntity.Layout);
			FloorLevel.MoveCurrentTo(DbEntity.FloorLevel);
			ToiletType.MoveCurrentTo(DbEntity.ToiletType);
		}

		protected override void UpdateConcreteModelFromValues(Domain.Entities.Flat flat)
		{
			flat.TotalSquare = String.IsNullOrWhiteSpace(TotalSquare) ? null : new decimal?(Decimal.Parse(TotalSquare));
			flat.KitchenSquare = String.IsNullOrWhiteSpace(KitchenSquare) ? null : new decimal?(Decimal.Parse(KitchenSquare));
			flat.ResidentialSquare = String.IsNullOrWhiteSpace(ResidentialSquare) ? null : new decimal?(Decimal.Parse(ResidentialSquare));
			flat.TotalRoomCount = String.IsNullOrWhiteSpace(TotalSquare) ? null : new int?(Int32.Parse(TotalRoomCount));
			flat.TotalFloor = String.IsNullOrWhiteSpace(TotalSquare) ? null : new int?(Int32.Parse(TotalFloor));
			flat.Floor = String.IsNullOrWhiteSpace(TotalSquare) ? null : new int?(Int32.Parse(Floor));
			flat.Terrace = ResolveDictionary<Terrace>(Terrace);
			flat.Material = ResolveDictionary<Material>(Material);
			flat.Layout = ResolveDictionary<Layout>(Layout);
			flat.FloorLevel = ResolveDictionary<FloorLevel>(FloorLevel);
			flat.ToiletType = ResolveDictionary<ToiletType>(ToiletType);
		}

		#endregion

		#endregion

        protected ToiletType NullToiletType = new ToiletType(String.Empty);

		protected override void InitCollection()
		{
			Terrace = new ListCollectionView((new[] { NullTerrace }).Concat(_TerraceService.GetAll()).ToList());
			Layout = new ListCollectionView((new[] { NullLayout }).Concat(_LayoutService.GetAll()).ToList());
			Material = new ListCollectionView((new[] { NullMaterial }).Concat(_MaterialService.GetAll()).ToList());
			FloorLevel = new ListCollectionView((new[] { NullFloorLevel }).Concat(_FloorLevelService.GetAll()).ToList());
			ToiletType = new ListCollectionView((new[] { NullToiletType }).Concat(_ToiletTypeService.GetAll()).ToList());
		}

        protected override void CloseDialog()
        {
            _ViewsService.CloseFlatDialog();
        }

        protected override void OpenDialog()
        {
            _ViewsService.OpenFlatDialog(this);
        }
    }
}