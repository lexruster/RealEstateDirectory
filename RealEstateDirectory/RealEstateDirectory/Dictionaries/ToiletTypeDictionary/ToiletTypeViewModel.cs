using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Dictionaries.ToiletTypeDictionary
{
	[NotifyForAll]
	public class ToiletTypeViewModel : DictionaryWithOnlyNameEntityViewModel<ToiletType>
	{
		public ToiletTypeViewModel(IToiletTypeService service) : base(service) { }
	}
}