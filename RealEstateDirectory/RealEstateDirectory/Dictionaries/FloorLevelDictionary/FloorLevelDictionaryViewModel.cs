using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.FloorLevelDictionary
{
	[NotifyForAll]
	public class FloorLevelDictionaryViewModel : DictionaryWithOnlyNameEntitiesViewModel<FloorLevelViewModel, FloorLevel>
	{
		public FloorLevelDictionaryViewModel(IServiceLocator serviceLocator, IFloorLevelService dealVariantService, IMessageService messageService) : base(serviceLocator, dealVariantService, messageService) { }
	}
}