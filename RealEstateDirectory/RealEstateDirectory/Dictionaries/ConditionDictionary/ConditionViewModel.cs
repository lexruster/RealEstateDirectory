using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Dictionaries.ConditionDictionary
{
	[NotifyForAll]
	public class ConditionViewModel : DictionaryWithOnlyNameEntityViewModel<Condition>
	{
		public ConditionViewModel(IConditionService service) : base(service) { }
	}
}