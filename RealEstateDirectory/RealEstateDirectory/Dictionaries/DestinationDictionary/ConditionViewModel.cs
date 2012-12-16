using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Dictionaries.DestinationDictionary
{
	[NotifyForAll]
	public class DestinationViewModel : DictionaryWithOnlyNameEntityViewModel<Destination>
	{
		public DestinationViewModel(IDestinationService service) : base(service) { }
	}
}