using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.OwnershipDictionary
{
	[NotifyForAll]
	public class OwnershipDictionaryViewModel : DictionaryWithOnlyNameEntitiesViewModel<OwnershipViewModel, Ownership>
	{
		public OwnershipDictionaryViewModel(IServiceLocator serviceLocator, IOwnershipService service, IMessageService messageService) : base(serviceLocator, service, messageService) { }
	}
}