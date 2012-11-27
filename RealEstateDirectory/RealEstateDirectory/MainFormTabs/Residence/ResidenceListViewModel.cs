using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.MainFormTabs.Common;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.MainFormTabs.Residence
{
	public class ResidenceListViewModel : RealEstateListViewModel<Domain.Entities.Residence>
	{
		public ResidenceListViewModel(IResidenceService residenceService, IMessageService messageService,
		                              IDistrictService districtService, IRealtorService realtorService,
		                              IOwnershipService ownershipService, IDealVariantService dealVariantService,
									  IServiceLocator serviceLocator)
			: base(residenceService, messageService, districtService, realtorService, ownershipService, dealVariantService, serviceLocator)
		{
			
		}

		protected override RealEstateViewModel<Domain.Entities.Residence> GetViewEntityViewModel()
		{
			return _ServiceLocator.GetInstance<ResidenceViewModel>();
		}

		protected override RealEstateEditViewModel<Domain.Entities.Residence> GetEditEntityViewModel()
		{
			return _ServiceLocator.GetInstance<ResidenceEditViewModel>();
		}

		protected override IEnumerable<Domain.Entities.Residence> UpdateChildFilterData(IEnumerable<Domain.Entities.Residence> entities)
		{
			return entities;
		}

		protected override void LoadChildFiltersData()
		{
			
		}
	}
}