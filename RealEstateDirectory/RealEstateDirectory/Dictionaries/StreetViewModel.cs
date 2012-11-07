using System;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Dictionaries
{
	[NotifyForAll]
	public class StreetViewModel : DictionaryEntityViewModel<Street>
	{
		public StreetViewModel(IStreetService streetService)
		{
			_StreetService = streetService;
		}

		#region Infrastructure

		private readonly IStreetService _StreetService;

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
                string error = null;
                var validation = _StreetService.IsValid(new Street(Name));
                error = validation.IsValid ? null : validation.GetReasons();
                return error;

			}
		}

		public override void SaveToDatabase()
		{
			_StreetService.Save(DbEntity);
		}
	}
}