using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.TerraceDictionary
{
	[NotifyForAll]
	public class TerraceDictionaryViewModel : DictionaryWithOnlyNameEntitiesViewModel<TerraceViewModel, Terrace>
	{
		public TerraceDictionaryViewModel(IServiceLocator serviceLocator, ITerraceService service, IMessageService messageService) : base(serviceLocator, service, messageService) { }
	}
}