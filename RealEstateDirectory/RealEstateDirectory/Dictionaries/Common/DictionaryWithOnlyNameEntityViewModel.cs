using System;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Dictionaries.Common
{
	[NotifyForAll]
	public class DictionaryWithOnlyNameEntityViewModel<T> : DictionaryEntityViewModel<T> where T : BaseDictionary
	{
		public DictionaryWithOnlyNameEntityViewModel(IDictionaryWithOnlyNameEntitiesService<T> dictionaryService)
		{
			_DictionaryService = dictionaryService;
		}

		#region Infrastructure

		private readonly IDictionaryWithOnlyNameEntitiesService<T> _DictionaryService;

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
				var dictElement = _DictionaryService.Create(Name);
				var validation = _DictionaryService.IsValid(dictElement, Id);
				return validation.IsValid ? null : validation.GetReasons();
			}
		}

		public override void SaveToDatabase()
		{
			_DictionaryService.Save(DbEntity);
			UpdateValuesFromModel();
		}
	}
}