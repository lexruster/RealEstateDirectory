using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Dictionaries.FloorLevelDictionary
{
	[NotifyForAll]
	public class FloorLevelViewModel : DictionaryWithOnlyNameEntityViewModel<FloorLevel>
	{
		public FloorLevelViewModel(IFloorLevelService dealVariantService) : base(dealVariantService) { }
	}
}