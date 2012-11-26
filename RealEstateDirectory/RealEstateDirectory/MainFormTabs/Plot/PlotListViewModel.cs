using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.MainFormTabs.Common;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.MainFormTabs.Plot
{
	[NotifyForAll]
    public class PlotListViewModel : RealEstateListViewModel<Domain.Entities.Plot>
	{
        public PlotListViewModel(IPlotService service, IMessageService messageService,
		                         IDistrictService districtService, IRealtorService realtorService,
		                         IOwnershipService ownershipService, IDealVariantService dealVariantService,
		                         IServiceLocator serviceLocator)
			: base(service, messageService, districtService, realtorService, ownershipService, dealVariantService, serviceLocator
				)
		{
		}

		protected override RealEstateViewModel<Domain.Entities.Plot> GetViewEntityViewModel()
		{
			return _ServiceLocator.GetInstance<PlotViewModel>();
		}

		protected override RealEstateEditViewModel<Domain.Entities.Plot> GetEditEntityViewModel()
		{
			return _ServiceLocator.GetInstance<PlotEditViewModel>();
		}

		protected override IEnumerable<Domain.Entities.Plot> UpdateChildFilterData(IEnumerable<Domain.Entities.Plot> entities)
		{
			return entities;
		}

		protected override void LoadChildFiltersData()
		{
		}
	}
}