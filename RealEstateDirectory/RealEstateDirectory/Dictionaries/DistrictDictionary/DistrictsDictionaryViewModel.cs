using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.DistrictDictionary
{
	[NotifyForAll]
	public class DistrictsDictionaryViewModel : DictionaryWithOnlyNameEntitiesViewModel<DistrictViewModel, District>
	{
		public DistrictsDictionaryViewModel(IServiceLocator serviceLocator, IDistrictService districtService, IMessageService messageService) : base(serviceLocator, districtService, messageService) { }
	}
}