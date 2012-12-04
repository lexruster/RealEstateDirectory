using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Dictionaries.MaterialDictionary
{
	[NotifyForAll]
	public class MaterialViewModel : DictionaryWithOnlyNameEntityViewModel<Material>
	{
		public MaterialViewModel(IMaterialService service) : base(service) { }
	}
}