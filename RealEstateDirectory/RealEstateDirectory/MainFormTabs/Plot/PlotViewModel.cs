using System;
using NotifyPropertyWeaver;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.MainFormTabs.Common;

namespace RealEstateDirectory.MainFormTabs.Plot
{
    [NotifyForAll]
    public class PlotViewModel : RealEstateViewModel<Domain.Entities.Plot>
    {
        #region Свойства  INotify

        public decimal? PlotSquare { get; set; }

        #endregion

        #region Свойства

        public string PlotSquareString
        {
            get { return String.Format("{0:0.0}", PlotSquare); }
        }

        #endregion

        #region Методы

        protected override void UpdateValuesFromConcreteModel()
        {
            PlotSquare = DbEntity.PlotSquare;
        }

        #endregion
    }
}