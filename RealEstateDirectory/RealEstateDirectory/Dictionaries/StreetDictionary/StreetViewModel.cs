using System;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Dictionaries.StreetDictionary
{
	[NotifyForAll]
	public class StreetViewModel : DictionaryEntityViewModel<Street>
	{
		public StreetViewModel(IStreetService service)
		{
			_DictionaryService = service;
		}

		#region Infrastructure

		private readonly IStreetService _DictionaryService;

		#endregion

		public int Id { get; set; }
		public string Name { get; set; }
		public District District { get; set; }

		public override void UpdateValuesFromModel()
		{
			Id = DbEntity.Id;
			Name = DbEntity.Name;
			District = DbEntity.District;
		}

		public override void UpdateModelFromValues()
		{
			DbEntity.Name = Name;
			DbEntity.District = District;
		}

	    public override string this[string columnName]
	    {
	        get
	        {
	            if (columnName == PropertySupport.ExtractPropertyName(() => Name))
	            {
	                if (String.IsNullOrWhiteSpace(Name))
	                    return String.Format("Значение элемента справочника \"{0}\" не может быть пустым.",
	                                         _DictionaryService.DictionaryName);
	            }
	            if (columnName == PropertySupport.ExtractPropertyName(() => District))
	            {
	                if (District == null)
	                    return String.Format("Должен быть указан район.");
	            }
	            return null;
	        }
	    }

	    public override string Error
		{
			get
			{
				var dictElement = new Street(Name) {District = District};
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