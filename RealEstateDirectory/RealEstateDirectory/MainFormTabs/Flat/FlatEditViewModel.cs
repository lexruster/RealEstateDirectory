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

        #region Infrastructure

        private readonly IViewsService _ViewsService;
        private readonly ITerraceService _TerraceService;
        private readonly IMaterialService _MaterialService;
        private readonly ILayoutService _LayoutService;
        private readonly IFloorLevelService _FloorLevelService;
        private readonly IToiletTypeService _ToiletTypeService;

        #endregion

        #region Свойства  INotifi

        public decimal? TotalSquare { get; set; }
        public decimal? KitchenSquare { get; set; }
        public decimal? ResidentialSquare { get; set; }
        public int? TotalRoomCount { get; set; }
        public int? TotalFloor { get; set; }
        public ListCollectionView Terrace { get; set; }
        public ListCollectionView Material { get; set; }
        public ListCollectionView Layout { get; set; }
        public ListCollectionView FloorLevel { get; set; }
        public ListCollectionView ToiletType { get; set; }
        public int? Floor { get; set; }

        #endregion

        #region Свойства

        protected ToiletType NullToiletType = new ToiletType("");

        #endregion

        #region Перегрузки

        protected override void UpdateValuesFromConcreteModel()
        {
            TotalSquare = DbEntity.TotalSquare;
            KitchenSquare= DbEntity.KitchenSquare;
            ResidentialSquare = DbEntity.ResidentialSquare;
            TotalRoomCount = DbEntity.TotalRoomCount;
            TotalFloor = DbEntity.TotalFloor;
            Floor = DbEntity.Floor;
            Terrace.MoveCurrentTo(DbEntity.Terrace);
            Material.MoveCurrentTo(DbEntity.Material);
            Layout.MoveCurrentTo(DbEntity.Layout);
            FloorLevel.MoveCurrentTo(DbEntity.FloorLevel);
            ToiletType.MoveCurrentTo(DbEntity.ToiletType);
        }

        protected override void UpdateConcreteModelFromValues()
        {
            SetRoomValues(DbEntity);
        }

        protected override void CloseDialog()
        {
            _ViewsService.CloseFlatDialog();
        }

        protected override void OpenDialog()
        {
            _ViewsService.OpenFlatDialog(this);
        }

        protected override Domain.Entities.Flat CreateNewModel()
        {
            var flat = new Domain.Entities.Flat();
            SetRoomValues(flat);
            SetRealEstateValues(flat);

            return flat;
        }

        protected void SetRoomValues(Domain.Entities.Flat flat)
        {
            flat.TotalSquare = TotalSquare;
            flat.KitchenSquare = KitchenSquare;
            flat.ResidentialSquare = ResidentialSquare;
            flat.TotalRoomCount = TotalRoomCount;
            flat.TotalFloor = TotalFloor;
            flat.Floor = Floor;
            flat.Terrace = ResolveDictionary<Terrace>(Terrace);
            flat.Material = ResolveDictionary<Material>(Material);
            flat.Layout = ResolveDictionary<Layout>(Layout);
            flat.FloorLevel = ResolveDictionary<FloorLevel>(FloorLevel);
            flat.ToiletType = ResolveDictionary<ToiletType>(ToiletType);
        }

        protected override string ChildDataError(string propertyName)
        {
            
            if (propertyName == PropertySupport.ExtractPropertyName(() => TotalSquare))
            {
                if (TotalSquare < 0)
                    return "Площадь не может быть отрицательной";
            }

            if (propertyName == PropertySupport.ExtractPropertyName(() => TotalSquare))
            {
                if (ResidentialSquare < 0)
                    return "Площадь не может быть отрицательной";
            }

            if (propertyName == PropertySupport.ExtractPropertyName(() => TotalSquare))
            {
                if (KitchenSquare < 0)
                    return "Площадь не может быть отрицательной";
            }

            if (propertyName == PropertySupport.ExtractPropertyName(() => TotalRoomCount))
            {
                if (TotalRoomCount < 0)
                    return "Число комнат не может быть отрицательным";
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
            ToiletType = new ListCollectionView((new[] { NullToiletType }).Concat(_ToiletTypeService.GetAll()).ToList());
        }

        #endregion
    }
}