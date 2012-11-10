using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.WaterSupplyDictionary
{
	[NotifyForAll]
	public class WaterSupplyDictionaryViewModel : DictionaryWithOnlyNameEntitiesViewModel<WaterSupplyViewModel, WaterSupply>
	{
		public WaterSupplyDictionaryViewModel(IServiceLocator serviceLocator, IWaterSupplyService service, IMessageService messageService) : base(serviceLocator, service, messageService) { }
	}
}