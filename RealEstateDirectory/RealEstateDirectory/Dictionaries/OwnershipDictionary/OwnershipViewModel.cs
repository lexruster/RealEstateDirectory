using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Dictionaries.OwnershipDictionary
{
	[NotifyForAll]
	public class OwnershipViewModel : DictionaryWithOnlyNameEntityViewModel<Ownership>
	{
		public OwnershipViewModel(IOwnershipService service) : base(service) { }
	}
}