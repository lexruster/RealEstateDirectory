using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.MaterialDictionary
{
	[NotifyForAll]
	public class MaterialDictionaryViewModel : DictionaryWithOnlyNameEntitiesViewModel<MaterialViewModel, Material>
	{
		public MaterialDictionaryViewModel(IServiceLocator serviceLocator, IMaterialService service, IMessageService messageService) : base(serviceLocator, service, messageService) { }
	}
}