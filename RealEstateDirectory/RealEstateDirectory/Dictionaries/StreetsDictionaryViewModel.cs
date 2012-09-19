using System;
using RealEstateDirectory.Data.Entities;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries
{
	public class StreetsDictionaryViewModel : DictionaryViewModel<Street>
	{
		public StreetsDictionaryViewModel(IDataService dataService)
			: base(dataService)
		{
		}

		protected override void Entities_CurrentChanged(object sender, EventArgs eventArgs)
		{
			base.Entities_CurrentChanged(sender, eventArgs);

			var current = Entities.CurrentItem as Street;
			Name = current == null ? String.Empty : current.Name;
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