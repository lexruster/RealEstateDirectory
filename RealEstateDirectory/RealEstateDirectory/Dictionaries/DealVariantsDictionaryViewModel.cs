using System;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries
{
	public class DealVariantsDictionaryViewModel : DictionaryViewModel<DealVariantViewModel, DealVariant>
	{
		public DealVariantsDictionaryViewModel(IServiceLocator serviceLocator, IDealVariantService dealVariantService, IMessageService messageService) : base(serviceLocator, messageService)
		{
			_DealVariantService = dealVariantService;

			PropertyChanged += (sender, args) =>
				{
					if (args.PropertyName == PropertySupport.ExtractPropertyName(() => Name))
						AddCommand.RaiseCanExecuteChanged();
				};
			dealVariantService.StartSession();
		}

		#region Infrastructure

		private readonly IDealVariantService _DealVariantService;

		#endregion

		public string Name { get; set; }

		protected override void InitializeEntities()
		{
			_Entities.AddRange(_DealVariantService.GetAll().Select(CreateNewViewModel));
		}

		protected override bool CanAdd()
		{
			return !String.IsNullOrWhiteSpace(Name) && _Entities.All(model => model.Name != Name);
		}

		protected override DealVariant CreateNewModel()
		{
			return new DealVariant(Name);
		}

		protected override bool IsCanRemove(DealVariantViewModel entity, out string errorText)
		{
			errorText = null;
			return _DealVariantService.IsPossibilityToDelete(entity.DbEntity);
		}

		protected override void RemoveEntityFromDatabase(DealVariant entity)
		{
			_DealVariantService.Delete(entity);
		}
	}
}