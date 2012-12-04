using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.DealVariantDictionary
{
	[NotifyForAll]
	public class DealVariantsDictionaryViewModel : DictionaryWithOnlyNameEntitiesViewModel<DealVariantViewModel, DealVariant>
	{
		public DealVariantsDictionaryViewModel(IServiceLocator serviceLocator, IDealVariantService dealVariantService, IMessageService messageService) : base(serviceLocator, dealVariantService, messageService) { }
	}
}