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

namespace RealEstateDirectory.MainFormTabs.Room
{
    [NotifyForAll]
    public class RoomEditViewModel : RealEstateEditViewModel<Domain.Entities.Room>
    {
        #region Конструктор

        public RoomEditViewModel(IRoomService service, IMessageService messageService,
                                 IDistrictService districtService, IViewsService viewsService,
                                 IRealtorService realtorService, IOwnershipService ownershipService,
                                 IDealVariantService dealVariantService,
                                 ITerraceService terraceService, IMaterialService materialService,
								 ILayoutService layoutService, IFloorLevelService floorLevelService, IConditionService conditionalService)
			: base(service, messageService, districtService, realtorService, ownershipService, dealVariantService, conditionalService)
        {
            _ViewsService = viewsService;
            _TerraceService = terraceService;
            _MaterialService = materialService;
            _LayoutService = layoutService;
            _FloorLevelService = floorLevelService;
        }

        #endregion

        #region Инфраструктура

        private readonly IViewsService _ViewsService;
        private readonly ITerraceService _TerraceService;
        private readonly IMaterialService _MaterialService;
        private readonly ILayoutService _LayoutService;
        private readonly IFloorLevelService _FloorLevelService;

        #endregion

        #region Сущность

		#region Свойства

	    public ListCollectionView Terrace { get; set; }
	    public ListCollectionView Material { get; set; }
	    public ListCollectionView Layout { get; set; }
	    public ListCollectionView FloorLevel { get; set; }

	    public string TotalSquare { get; set; }

		[NotifyProperty(AlsoNotifyFor = new [] { "RoomCount" })]
	    public string TotalRoomCount { get; set; }

		[NotifyProperty(AlsoNotifyFor = new[] { "TotalRoomCount" })]
		public string RoomCount { get; set; }

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

				if (propertyName == PropertySupport.ExtractPropertyName(() => TotalSquare) && !IsValidAndPositiveDecimal(TotalSquare))
					return "Общая площадь введена некорректно";

				if (propertyName == PropertySupport.ExtractPropertyName(() => TotalRoomCount) && !IsValidAndPositiveInt(TotalRoomCount))
					return "Общее количество комнат введено некорректно";

				if (propertyName == PropertySupport.ExtractPropertyName(() => RoomCount) && !IsValidAndPositiveInt(RoomCount))
					return "Количество комнат введено некорректно";

				if (propertyName == PropertySupport.ExtractPropertyName(() => TotalFloor) && !IsValidAndPositiveInt(TotalFloor))
					return "Количество этажей введено некорректно";

				if (propertyName == PropertySupport.ExtractPropertyName(() => Floor) && !IsValidAndPositiveInt(Floor))
					return "Этаж введен некорректно";

				return null;
			}
		}

		protected override IEnumerable<string> ValidableProperties
		{
			get
			{
				foreach (var validatableProperty in base.ValidableProperties)
					yield return validatableProperty;

				yield return PropertySupport.ExtractPropertyName(() => TotalSquare);
				yield return PropertySupport.ExtractPropertyName(() => TotalRoomCount);
				yield return PropertySupport.ExtractPropertyName(() => RoomCount);
				yield return PropertySupport.ExtractPropertyName(() => TotalFloor);
				yield return PropertySupport.ExtractPropertyName(() => Floor);
			}
		}

		#endregion

		#region Взаимодействие с моделью БД


		protected override Domain.Entities.Room CreateNewModel()
		{
			var room = new Domain.Entities.Room();
			UpdateConcreteModelFromValues(room);
			SetRealEstateValues(room);

			return room;
		}

	    protected override void UpdateValuesFromConcreteModel()
	    {
			TotalSquare = DbEntity.TotalSquare.HasValue ? DbEntity.TotalSquare.Value.ToString("0.#") : String.Empty;
			TotalRoomCount = DbEntity.TotalRoomCount.HasValue ? DbEntity.TotalRoomCount.Value.ToString() : String.Empty;
			TotalFloor = DbEntity.TotalFloor.HasValue ? DbEntity.TotalFloor.Value.ToString() : String.Empty;
			RoomCount = DbEntity.RoomCount.HasValue ? DbEntity.RoomCount.Value.ToString() : String.Empty;
			Floor = DbEntity.Floor.HasValue ? DbEntity.Floor.Value.ToString() : String.Empty;
			Terrace.MoveCurrentTo(DbEntity.Terrace);
		    Material.MoveCurrentTo(DbEntity.Material);
		    Layout.MoveCurrentTo(DbEntity.Layout);
		    FloorLevel.MoveCurrentTo(DbEntity.FloorLevel);
	    }

		protected override void UpdateConcreteModelFromValues(Domain.Entities.Room room)
	    {
			room.TotalSquare = String.IsNullOrWhiteSpace(TotalSquare) ? null : new decimal?(Decimal.Parse(TotalSquare));
			room.TotalRoomCount = String.IsNullOrWhiteSpace(TotalRoomCount) ? null : new int?(Int32.Parse(TotalRoomCount));
			room.TotalFloor = String.IsNullOrWhiteSpace(TotalFloor) ? null : new int?(Int32.Parse(TotalFloor));
			room.RoomCount = String.IsNullOrWhiteSpace(RoomCount) ? null : new int?(Int32.Parse(RoomCount));
			room.Floor = String.IsNullOrWhiteSpace(Floor) ? null : new int?(Int32.Parse(Floor));
			room.Terrace = ResolveDictionary<Terrace>(Terrace);
			room.Material = ResolveDictionary<Material>(Material);
			room.Layout = ResolveDictionary<Layout>(Layout);
			room.FloorLevel = ResolveDictionary<FloorLevel>(FloorLevel);
		}

	    #endregion

	    #endregion

	    protected override void InitCollection()
	    {
		    Terrace = new ListCollectionView((new[] {NullTerrace}).Concat(_TerraceService.GetAll()).ToList());
		    Layout = new ListCollectionView((new[] {NullLayout}).Concat(_LayoutService.GetAll()).ToList());
		    Material = new ListCollectionView((new[] {NullMaterial}).Concat(_MaterialService.GetAll()).ToList());
		    FloorLevel = new ListCollectionView((new[] {NullFloorLevel}).Concat(_FloorLevelService.GetAll()).ToList());
	    }

	    protected override void CloseDialog()
        {
            _ViewsService.CloseRoomDialog();
        }

	    protected override void OpenDialog()
        {
            _ViewsService.OpenRoomDialog(this);
        }
    }
}