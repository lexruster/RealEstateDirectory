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
                                  ISewageService sewageService, IMaterialService materialService)
            : base(service, messageService, districtService, realtorService, ownershipService, dealVariantService)
        {
            _ViewsService = viewsService;
            _WaterSupplyService = waterSupplyService;
            _SewageService = sewageService;
            _MaterialService = materialService;
        }

        #endregion

        #region Infrastructure

        private readonly IViewsService _ViewsService;
        private readonly IWaterSupplyService _WaterSupplyService;
        private readonly ISewageService _SewageService;
        private readonly IMaterialService _MaterialService;

        #endregion

        #region Свойства  INotify

        public decimal? PlotSquare { get; set; }
        public int? TotalFloor { get; set; }
        public decimal? HouseSquare { get; set; }
        public ListCollectionView WaterSupply { get; set; }
        public ListCollectionView Sewage { get; set; }
        public bool? HasBathhouse { get; set; }
        public bool? HasGarage { get; set; }
        public bool? HasGas { get; set; }
        public ListCollectionView Material { get; set; }

        #endregion

        #region Свойства

        protected Sewage NullSewage = new Sewage("");
        protected WaterSupply NullWaterSupply = new WaterSupply("");

        #endregion

        #region Перегрузки

        protected override void UpdateValuesFromConcreteModel()
        {
            PlotSquare = DbEntity.PlotSquare;
            TotalFloor = DbEntity.TotalFloor;
            HouseSquare = DbEntity.HouseSquare;
            HasBathhouse = DbEntity.HasBathhouse;
            HasGarage = DbEntity.HasGarage;
            HasGas = DbEntity.HasGas;

            WaterSupply.MoveCurrentTo(DbEntity.WaterSupply);
            Sewage.MoveCurrentTo(DbEntity.Sewage);
            Material.MoveCurrentTo(DbEntity.Material);
        }

        protected override void UpdateConcreteModelFromValues()
        {
            SetHouseValues(DbEntity);
        }

        protected override void CloseDialog()
        {
            _ViewsService.CloseHouseDialog();
        }

        protected override void OpenDialog()
        {
            _ViewsService.OpenHouseDialog(this);
        }

        protected override Domain.Entities.House CreateNewModel()
        {
            var house = new Domain.Entities.House();
            SetHouseValues(house);
            SetRealEstateValues(house);

            return house;
        }

        protected void SetHouseValues(Domain.Entities.House house)
        {
            house.PlotSquare = PlotSquare;
            house.TotalFloor = TotalFloor;
            house.HouseSquare = HouseSquare;
            house.HasBathhouse = HasBathhouse;
            house.HasGarage = HasGarage;
            house.HasGas = HasGas;

            house.WaterSupply = ResolveDictionary<WaterSupply>(WaterSupply);
            house.Sewage = ResolveDictionary<Sewage>(Sewage);
            house.Material = ResolveDictionary<Material>(Material);
        }

        protected override string ChildDataError(string propertyName)
        {
            if (propertyName == PropertySupport.ExtractPropertyName(() => PlotSquare))
            {
                if (PlotSquare < 0)
                    return "Площадь не может быть отрицательной";
            }

            if (propertyName == PropertySupport.ExtractPropertyName(() => HouseSquare))
            {
                if (HouseSquare < 0)
                    return "Площадь дома не может быть отрицательной";
            }

            if (propertyName == PropertySupport.ExtractPropertyName(() => TotalFloor))
            {
                if (TotalFloor < 0)
                    return "Количество этажей дома не может быть отрицательным";
            }

            return null;
        }

        protected override void InitCollection()
        {
            Material = new ListCollectionView((new[] {NullMaterial}).Concat(_MaterialService.GetAll()).ToList());
            WaterSupply = new ListCollectionView((new[] {NullWaterSupply}).Concat(_WaterSupplyService.GetAll()).ToList());
            Sewage = new ListCollectionView((new[] {NullSewage}).Concat(_SewageService.GetAll()).ToList());
        }

        #endregion
    }
}