using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Dictionaries.DistrictDictionary
{
	[NotifyForAll]
	public class DistrictViewModel : DictionaryWithOnlyNameEntityViewModel<District>
	{
		public DistrictViewModel(IDistrictService districtService) : base (districtService) { }
	}
}