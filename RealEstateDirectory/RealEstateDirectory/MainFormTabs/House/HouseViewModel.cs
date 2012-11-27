using System;
using NotifyPropertyWeaver;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.MainFormTabs.Common;

namespace RealEstateDirectory.MainFormTabs.House
{
    [NotifyForAll]
    public class HouseViewModel : RealEstateViewModel<Domain.Entities.House>
    {
        #region Свойства  INotify

        public decimal? PlotSquare { get; set; }
        public  int? TotalFloor { get; set; }
        public  decimal? HouseSquare { get; set; }
        public  WaterSupply WaterSupply { get; set; }
        public  Sewage Sewage { get; set; }
        public  bool? HasBathhouse { get; set; }
        public  bool? HasGarage { get; set; }
        public  bool? HasGas { get; set; }
        public  Material Material { get; set; }

        #endregion

        #region Свойства

        public string PlotSquareString
        {
            get { return String.Format("{0:0.0}", PlotSquare); }
        }

        public string HouseSquareString
        {
            get { return String.Format("{0:0.0}", HouseSquare); }
        }

        #endregion

        #region Методы

        protected override void UpdateValuesFromConcreteModel()
        {
            PlotSquare = DbEntity.PlotSquare;
            TotalFloor = DbEntity.TotalFloor;
            HouseSquare = DbEntity.HouseSquare;
            WaterSupply = DbEntity.WaterSupply;
            Sewage = DbEntity.Sewage;
            HasBathhouse = DbEntity.HasBathhouse;
            HasGarage = DbEntity.HasGarage;
            HasGas = DbEntity.HasGas;
            Material = DbEntity.Material;
        }

        #endregion
    }
}