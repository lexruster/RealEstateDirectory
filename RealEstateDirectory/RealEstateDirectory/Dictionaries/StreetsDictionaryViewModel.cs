using System;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries
{
	public class StreetsDictionaryViewModel : DictionaryViewModel<StreetViewModel, Street>
	{
		public StreetsDictionaryViewModel(IServiceLocator serviceLocator, IStreetService streetService, IMessageService messageService) : base(serviceLocator, messageService)
		{
			_StreetService = streetService;

			PropertyChanged += (sender, args) =>
				{
					if (args.PropertyName == PropertySupport.ExtractPropertyName(() => Name))
						AddCommand.RaiseCanExecuteChanged();
				};
            streetService.StartSession();
		}

		#region Infrastructure

		private readonly IStreetService _StreetService;

		#endregion

		public string Name { get; set; }

		protected override void InitializeEntities()
		{
			_Entities.AddRange(_StreetService.GetAll().Select(CreateNewViewModel));
		}

		protected override bool CanAdd()
		{
			return !String.IsNullOrWhiteSpace(Name) && _Entities.All(model => model.Name != Name);
		}

		protected override Street CreateNewModel()
		{
			return new Street(Name);
		}

		protected override bool IsCanRemove(StreetViewModel entity, out string errorText)
		{
			errorText = null;
			return _StreetService.IsPossibilityToDelete(entity.DbEntity);
		}

		protected override void RemoveEntityFromDatabase(Street entity)
		{
			_StreetService.Delete(entity);
		}
	}
}