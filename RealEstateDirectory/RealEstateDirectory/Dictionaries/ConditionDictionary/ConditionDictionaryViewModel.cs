using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.ConditionDictionary
{
	[NotifyForAll]
	public class ConditionDictionaryViewModel : DictionaryWithOnlyNameEntitiesViewModel<ConditionViewModel, Condition>
	{
		public ConditionDictionaryViewModel(IServiceLocator serviceLocator, IConditionService service, IMessageService messageService) : base(serviceLocator, service, messageService) { }
	}
}