using System;
using System.Collections.Generic;
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
                                 ILayoutService layoutService, IFloorLevelService floorLevelService)
            : base(service, messageService, districtService, realtorService, ownershipService, dealVariantService)
        {
            _ViewsService = viewsService;
            _TerraceService = terraceService;
            _MaterialService = materialService;
            _LayoutService = layoutService;
            _FloorLevelService = floorLevelService;
        }

        #endregion

        #region Infrastructure

        private readonly IViewsService _ViewsService;
        private readonly ITerraceService _TerraceService;
        private readonly IMaterialService _MaterialService;
        private readonly ILayoutService _LayoutService;
        private readonly IFloorLevelService _FloorLevelService;

        #endregion

        #region Свойства  INotifi

        public decimal? TotalSquare { get; set; }
        public int? TotalRoomCount { get; set; }
        public int? TotalFloor { get; set; }
        public ListCollectionView Terrace { get; set; }
        public int? RoomCount { get; set; }
        public ListCollectionView Material { get; set; }
        public ListCollectionView Layout { get; set; }
        public ListCollectionView FloorLevel { get; set; }
        public int? Floor { get; set; }

        #endregion

        #region Свойства

        #endregion

        #region Перегрузки

        protected override void UpdateValuesFromConcreteModel()
        {
            TotalSquare = DbEntity.TotalSquare;
            TotalRoomCount = DbEntity.TotalRoomCount;
            TotalFloor = DbEntity.TotalFloor;
            RoomCount = DbEntity.RoomCount;
            Floor = DbEntity.Floor;
            Terrace.MoveCurrentTo(DbEntity.Terrace);
            Material.MoveCurrentTo(DbEntity.Material);
            Layout.MoveCurrentTo(DbEntity.Layout);
            FloorLevel.MoveCurrentTo(DbEntity.FloorLevel);
        }

        protected override void UpdateConcreteModelFromValues()
        {
            SetRoomValues(DbEntity);
        }

        protected override void CloseDialog()
        {
            _ViewsService.CloseRoomDialog();
        }

        protected override void OpenDialog()
        {
            _ViewsService.OpenRoomDialog(this);
        }

        protected override Domain.Entities.Room CreateNewModel()
        {
            var room = new Domain.Entities.Room();
            SetRoomValues(room);
            SetRealEstateValues(room);

            return room;
        }

        protected void SetRoomValues(Domain.Entities.Room room)
        {
            room.TotalSquare = TotalSquare;
            room.TotalRoomCount = TotalRoomCount;
            room.TotalFloor = TotalFloor;
            room.RoomCount = RoomCount;
            room.Floor = Floor;
            room.Terrace = ResolveDictionary<Terrace>(Terrace);
            room.Material = ResolveDictionary<Material>(Material);
            room.Layout = ResolveDictionary<Layout>(Layout);
            room.FloorLevel = ResolveDictionary<FloorLevel>(FloorLevel);
        }

        protected override string ChildDataError(string propertyName)
        {
            
            if (propertyName == PropertySupport.ExtractPropertyName(() => TotalSquare))
            {
                if (TotalSquare < 0)
                    return "Площадь не может быть отрицательной";
            }

            if (propertyName == PropertySupport.ExtractPropertyName(() => TotalRoomCount))
            {
                if (TotalRoomCount < 0)
                    return "Полное число комнат не может быть отрицательным";
                if (TotalRoomCount.HasValue && RoomCount.HasValue && RoomCount > TotalRoomCount)
                    return "Число комнат не может быть больше общего числа комант";
            }

            if (propertyName == PropertySupport.ExtractPropertyName(() => RoomCount))
            {
                if (RoomCount < 0)
                    return "Число комнат не может быть отрицательным";

                if (TotalRoomCount.HasValue && RoomCount.HasValue && RoomCount > TotalRoomCount)
                    return "Число комнат не может быть больше общего числа комант";
            }

            if (propertyName == PropertySupport.ExtractPropertyName(() => Floor))
            {
                if (Floor < 0)
                    return "Этаж не может быть отрицательным";
                if (Floor.HasValue && TotalFloor.HasValue && Floor > TotalFloor)
                    return "Этаж не может быть больше общего числа этажей";
            }

            if (propertyName == PropertySupport.ExtractPropertyName(() => TotalFloor))
            {
                if (TotalFloor < 0)
                    return "Всего этажей не может быть отрицательным";
                if (Floor.HasValue && TotalFloor.HasValue && Floor > TotalFloor)
                    return "Этаж не может быть больше общего числа этажей";
            }

            return null;
        }

        protected override void InitCollection()
        {
            Terrace = new ListCollectionView((new[] {NullTerrace}).Concat(_TerraceService.GetAll()).ToList());
            Layout = new ListCollectionView((new[] {NullLayout}).Concat(_LayoutService.GetAll()).ToList());
            Material = new ListCollectionView((new[] {NullMaterial}).Concat(_MaterialService.GetAll()).ToList());
            FloorLevel = new ListCollectionView((new[] {NullFloorLevel}).Concat(_FloorLevelService.GetAll()).ToList());
        }

        #endregion
    }
}