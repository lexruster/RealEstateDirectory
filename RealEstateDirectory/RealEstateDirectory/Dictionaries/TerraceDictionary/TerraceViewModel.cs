using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Dictionaries.TerraceDictionary
{
	[NotifyForAll]
	public class TerraceViewModel : DictionaryWithOnlyNameEntityViewModel<Terrace>
	{
		public TerraceViewModel(ITerraceService service) : base(service) { }
	}
}