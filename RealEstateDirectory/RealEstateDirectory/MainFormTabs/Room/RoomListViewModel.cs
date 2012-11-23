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

namespace RealEstateDirectory.MainFormTabs.Room
{
	[NotifyForAll]
	public class RoomListViewModel : RealEstateListViewModel<Domain.Entities.Room>
	{
		public RoomListViewModel(IRoomService service, IMessageService messageService,
		                         IDistrictService districtService, IRealtorService realtorService,
		                         IOwnershipService ownershipService, IDealVariantService dealVariantService,
		                         IServiceLocator serviceLocator)
			: base(service, messageService, districtService, realtorService, ownershipService, dealVariantService, serviceLocator
				)
		{
		}

		public int? RoomCount { get; set; }

		protected override RealEstateViewModel<Domain.Entities.Room> GetViewEntityViewModel()
		{
			return _ServiceLocator.GetInstance<RoomViewModel>();
		}

		protected override RealEstateEditViewModel<Domain.Entities.Room> GetEditEntityViewModel()
		{
			return _ServiceLocator.GetInstance<RoomEditViewModel>();
		}

		protected override IEnumerable<Domain.Entities.Room> UpdateChildFilterData(IEnumerable<Domain.Entities.Room> entities)
		{
			return RoomCount.HasValue ? entities.Where(room => room.RoomCount == RoomCount.Value) : entities;
		}

		protected override void LoadChildFiltersData()
		{
			RoomCount = null;
		}
	}
}