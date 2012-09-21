using System;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.Data.Entities;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries
{
	[NotifyForAll]
	public class StreetViewModel : DictionaryEntityViewModel<Street>
	{
		protected override void This_PropertyChanged(string propertyName)
		{
			if (propertyName == PropertySupport.ExtractPropertyName(() => Name))
				DbEntity.Name = Name;
		}

		public override void Initialize(Street entity)
		{
			DbEntity = entity;
			Name = entity.Name;
		}

		public string Name { get; set; }
	}
}