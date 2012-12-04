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

namespace RealEstateDirectory.MainFormTabs.House
{
	[NotifyForAll]
    public class HouseListViewModel : RealEstateListViewModel<Domain.Entities.House>
	{
        public HouseListViewModel(IHouseService service, IMessageService messageService,
		                         IDistrictService districtService, IRealtorService realtorService,
		                         IOwnershipService ownershipService, IDealVariantService dealVariantService,
		                         IServiceLocator serviceLocator)
			: base(service, messageService, districtService, realtorService, ownershipService, dealVariantService, serviceLocator
				)
		{
		}

		public int? RoomCount { get; set; }

        protected override RealEstateViewModel<Domain.Entities.House> GetViewEntityViewModel()
		{
            return _ServiceLocator.GetInstance<HouseViewModel>();
		}

        protected override RealEstateEditViewModel<Domain.Entities.House> GetEditEntityViewModel()
		{
            return _ServiceLocator.GetInstance<HouseEditViewModel>();
		}

        protected override IEnumerable<Domain.Entities.House> UpdateChildFilterData(IEnumerable<Domain.Entities.House> entities)
		{
			return entities;
		}

		protected override void LoadChildFiltersData()
		{
		}
	}
}