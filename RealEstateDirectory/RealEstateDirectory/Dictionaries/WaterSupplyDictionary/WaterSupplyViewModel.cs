using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Dictionaries.WaterSupplyDictionary
{
	[NotifyForAll]
	public class WaterSupplyViewModel : DictionaryWithOnlyNameEntityViewModel<WaterSupply>
	{
		public WaterSupplyViewModel(IWaterSupplyService service) : base(service) { }
	}
}