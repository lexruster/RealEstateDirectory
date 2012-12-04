using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.SewageDictionary
{
	[NotifyForAll]
	public class SewageDictionaryViewModel : DictionaryWithOnlyNameEntitiesViewModel<SewageViewModel, Sewage>
	{
		public SewageDictionaryViewModel(IServiceLocator serviceLocator, ISewageService service, IMessageService messageService) : base(serviceLocator, service, messageService) { }
	}
}