using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.LayoutDictionary
{
	[NotifyForAll]
	public class LayoutDictionaryViewModel : DictionaryWithOnlyNameEntitiesViewModel<LayoutViewModel, Layout>
	{
		public LayoutDictionaryViewModel(IServiceLocator serviceLocator, ILayoutService service, IMessageService messageService) : base(serviceLocator, service, messageService) { }
	}
}