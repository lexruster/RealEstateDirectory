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

namespace RealEstateDirectory.MainFormTabs.Plot
{
    [NotifyForAll]
    public class PlotEditViewModel : RealEstateEditViewModel<Domain.Entities.Plot>
    {
        #region Конструктор

        public PlotEditViewModel(IPlotService service, IMessageService messageService,
                                 IDistrictService districtService, IViewsService viewsService,
                                 IRealtorService realtorService, IOwnershipService ownershipService,
                                 IDealVariantService dealVariantService)
            : base(service, messageService, districtService, realtorService, ownershipService, dealVariantService)
        {
            _ViewsService = viewsService;
        }

        #endregion

        #region Infrastructure

        private readonly IViewsService _ViewsService;

        #endregion

        #region Свойства  INotify

        public decimal? PlotSquare { get; set; }

        #endregion

        #region Свойства

        #endregion

        #region Перегрузки

        protected override void UpdateValuesFromConcreteModel()
        {
            PlotSquare = DbEntity.PlotSquare;
        }

        protected override void UpdateConcreteModelFromValues()
        {
            SetPlotValues(DbEntity);
        }

        protected override void CloseDialog()
        {
            _ViewsService.ClosePlotDialog();
        }

        protected override void OpenDialog()
        {
            _ViewsService.OpenPlotDialog(this);
        }

        protected override Domain.Entities.Plot CreateNewModel()
        {
            var plot = new Domain.Entities.Plot();
            SetPlotValues(plot);
            SetRealEstateValues(plot);

            return plot;
        }

        protected void SetPlotValues(Domain.Entities.Plot plot)
        {
            plot.PlotSquare = PlotSquare;
        }

        protected override string ChildDataError(string propertyName)
        {

            if (propertyName == PropertySupport.ExtractPropertyName(() => PlotSquare))
            {
                if (PlotSquare < 0)
                    return "Площадь не может быть отрицательной";
            }

            return null;
        }

        protected override void InitCollection()
        {
        }

        #endregion
    }
}