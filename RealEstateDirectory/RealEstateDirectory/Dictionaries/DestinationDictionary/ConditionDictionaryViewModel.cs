using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.DestinationDictionary
{
	[NotifyForAll]
	public class DestinationDictionaryViewModel : DictionaryWithOnlyNameEntitiesViewModel<DestinationViewModel, Destination>
	{
		public DestinationDictionaryViewModel(IServiceLocator serviceLocator, IDestinationService service, IMessageService messageService) : base(serviceLocator, service, messageService) { }
	}
}