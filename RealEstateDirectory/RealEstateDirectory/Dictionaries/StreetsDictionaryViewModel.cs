using System;
using System.Linq;
using RealEstateDirectory.Data.Entities;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries
{
	public class StreetsDictionaryViewModel : DictionaryViewModel<StreetViewModel>
	{
		public StreetsDictionaryViewModel(IDataService dataService, IMessageService messageService) : base(dataService, messageService) { }

		protected override void InitializeEntities()
		{
			_Entities = new DataObservableCollection<StreetViewModel>();
			_Entities.AddRange(_DataService.GetStreets().Select(street =>
				{
					var viewModel = _ServiceLocator.GetInstance<StreetViewModel>();
					viewModel.Initialize(street);
					return viewModel;
				}));
		}

		protected override void InitEntity(StreetViewModel entity)
		{
			entity.Initialize(_DataService.AddNewStreet());
		}

		protected override bool IsCorrect(StreetViewModel entity, out string errorText)
		{
			return _DataService.IsCorrect(entity.DbEntity, out errorText);
		}

		protected override void RemoveEntity(StreetViewModel entity)
		{
			_DataService.RemoveStreet(entity.DbEntity);
		}

		protected override void Entities_CurrentChanged(object sender, EventArgs eventArgs)
		{
			base.Entities_CurrentChanged(sender, eventArgs);

			var current = Entities.CurrentItem as Street;
		}

		protected override void Add()
		{
			_DataEntities.Add(new Street {Name = Name});
		}

		protected override void Change()
		{
			((Street) Entities.CurrentItem).Name = Name;
		}

		protected override void Delete()
		{
			_DataEntities.Remove((Street) Entities.CurrentItem);
		}
	}
}