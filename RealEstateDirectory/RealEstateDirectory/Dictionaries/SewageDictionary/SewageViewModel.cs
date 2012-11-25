using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Dictionaries.SewageDictionary
{
	[NotifyForAll]
	public class SewageViewModel : DictionaryWithOnlyNameEntityViewModel<Sewage>
	{
		public SewageViewModel(ISewageService service) : base(service) { }
	}
}