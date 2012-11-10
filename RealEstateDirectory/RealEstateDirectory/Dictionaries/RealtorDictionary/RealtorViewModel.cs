using System;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Dictionaries.RealtorDictionary
{
	[NotifyForAll]
	public class RealtorViewModel : DictionaryEntityViewModel<Realtor>
	{
		public RealtorViewModel(IRealtorService service)
		{
			_DictionaryService = service;
		}

		#region Infrastructure

		private readonly IRealtorService _DictionaryService;

		#endregion

		public int Id { get; set; }

		public string Name { get; set; }

		public override void UpdateValuesFromModel()
		{
			Id = DbEntity.Id;
			Name = DbEntity.Name;
		}

		public override void UpdateModelFromValues()
		{
			DbEntity.Name = Name;
		}

		public override string this[string columnName]
		{
			get
			{
				if (columnName == PropertySupport.ExtractPropertyName(() => Name))
				{
					if (String.IsNullOrWhiteSpace(Name))
						return String.Format("Значение элемента справочника \"{0}\" не может быть пустым.", _DictionaryService.DictionaryName);
				}
				return null;
			}
		}

		public override string Error
		{
			get
			{
				var dictElement = new Realtor(Name);
				var validation = _DictionaryService.IsValid(dictElement);
				return validation.IsValid ? null : validation.GetReasons();
			}
		}

		public override void SaveToDatabase()
		{
			_DictionaryService.Save(DbEntity);
		}
	}
}