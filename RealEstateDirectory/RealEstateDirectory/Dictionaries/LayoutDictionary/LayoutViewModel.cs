using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Dictionaries.LayoutDictionary
{
	[NotifyForAll]
	public class LayoutViewModel : DictionaryWithOnlyNameEntityViewModel<Layout>
	{
		public LayoutViewModel(ILayoutService service) : base(service) { }
	}
}