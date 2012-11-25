using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Dictionaries.DealVariantDictionary
{
	[NotifyForAll]
	public class DealVariantViewModel : DictionaryWithOnlyNameEntityViewModel<DealVariant>
	{
		public DealVariantViewModel(IDealVariantService dealVariantService) : base(dealVariantService) { }
	}
}