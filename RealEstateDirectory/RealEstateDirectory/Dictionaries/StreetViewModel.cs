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
		public StreetViewModel(IDataService dataService)
		{
			_DataService = dataService;
		}

		#region Infrastructure

		private readonly IDataService _DataService;

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
						return "Имя улицы не должно быть пустым";
				}
				return null;
			}
		}

		public override string Error
		{
			get
			{
				string errorMessage;
				_DataService.IsCorrect(new Street {Name = Name}, out errorMessage);
				return errorMessage;
			}
		}

		public override void SaveToDatabase()
		{
			_DataService.SaveStreet(DbEntity);
		}
	}
}