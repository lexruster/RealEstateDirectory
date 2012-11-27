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

namespace RealEstateDirectory.MainFormTabs.Flat
{
	[NotifyForAll]
	public class FlatListViewModel : RealEstateListViewModel<Domain.Entities.Flat>
	{
        public FlatListViewModel(IFlatService service, IMessageService messageService,
		                         IDistrictService districtService, IRealtorService realtorService,
		                         IOwnershipService ownershipService, IDealVariantService dealVariantService,
		                         IServiceLocator serviceLocator)
			: base(service, messageService, districtService, realtorService, ownershipService, dealVariantService, serviceLocator)
		{
		}

        public int? TotalRoomCount { get; set; }

		protected override RealEstateViewModel<Domain.Entities.Flat> GetViewEntityViewModel()
		{
			return _ServiceLocator.GetInstance<FlatViewModel>();
		}

		protected override RealEstateEditViewModel<Domain.Entities.Flat> GetEditEntityViewModel()
		{
			return _ServiceLocator.GetInstance<FlatEditViewModel>();
		}

		protected override IEnumerable<Domain.Entities.Flat> UpdateChildFilterData(IEnumerable<Domain.Entities.Flat> entities)
		{
            return TotalRoomCount.HasValue ? entities.Where(room => room.TotalRoomCount == TotalRoomCount.Value) : entities;
		}

		protected override void LoadChildFiltersData()
		{
            TotalRoomCount = null;
		}
	}
}