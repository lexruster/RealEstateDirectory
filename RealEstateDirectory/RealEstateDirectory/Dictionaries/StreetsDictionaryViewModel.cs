using System;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.Data.Entities;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries
{
	public class StreetsDictionaryViewModel : DictionaryViewModel<StreetViewModel, Street>
	{
		public StreetsDictionaryViewModel(IServiceLocator serviceLocator, IDataService dataService, IMessageService messageService) : base(serviceLocator, dataService, messageService)
		{
			PropertyChanged += (sender, args) =>
				{
					if (args.PropertyName == PropertySupport.ExtractPropertyName(() => Name))
						AddCommand.RaiseCanExecuteChanged();
				};
		}

		public string Name { get; set; }

		protected override void InitializeEntities()
		{
			_Entities.AddRange(_DataService.GetStreets().Select(CreateNewViewModel));
		}

		protected override bool CanAdd()
		{
			return !String.IsNullOrWhiteSpace(Name) && _Entities.All(model => model.Name != Name);
		}

		protected override Street CreateNewModel()
		{
			return new Street {Name = Name};
		}

		protected override bool IsCanRemove(StreetViewModel entity, out string errorText)
		{
			return _DataService.IsCanRemove(entity.DbEntity, out errorText);
		}

		protected override void RemoveEntityFromDatabase(Street entity)
		{
			_DataService.RemoveStreet(entity);
		}
	}
}