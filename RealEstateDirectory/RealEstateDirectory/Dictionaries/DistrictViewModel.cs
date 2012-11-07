using System;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Dictionaries
{
	[NotifyForAll]
	public class DistrictViewModel : DictionaryEntityViewModel<District>
	{
		public DistrictViewModel(IDistrictService districtService)
		{
			_DistrictService = districtService;
		}

		#region Infrastructure

		private readonly IDistrictService _DistrictService;

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
						return "Наименование района не должно быть пустым";
				}
				return null;
			}
		}

		public override string Error
		{
			get
			{
                string error = null;
                var validation = _DistrictService.IsValid(new District(Name));
                error = validation.IsValid ? null : validation.GetReasons();
                return error;
			}
		}

		public override void SaveToDatabase()
		{
			_DistrictService.Save(DbEntity);
		}
	}
}