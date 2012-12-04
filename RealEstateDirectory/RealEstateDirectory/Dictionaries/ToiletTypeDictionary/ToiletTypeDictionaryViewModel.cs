using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.ToiletTypeDictionary
{
	[NotifyForAll]
	public class ToiletTypeDictionaryViewModel : DictionaryWithOnlyNameEntitiesViewModel<ToiletTypeViewModel, ToiletType>
	{
		public ToiletTypeDictionaryViewModel(IServiceLocator serviceLocator, IToiletTypeService service, IMessageService messageService) : base(serviceLocator, service, messageService) { }
	}
}