using System;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Dictionaries
{
	[NotifyForAll]
	public class DealVariantViewModel : DictionaryEntityViewModel<DealVariant>
	{
		public DealVariantViewModel(IDealVariantService dealVariantService)
		{
			_DealVariantService = dealVariantService;
		}

		#region Infrastructure

		private readonly IDealVariantService _DealVariantService;

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
						return "Наименование варианта сделки не должно быть пустым";
				}
				return null;
			}
		}

		public override string Error
		{
			get
			{
				return _DealVariantService.IsValid(new DealVariant(Name)) ? null : "Наименование варианта сделки некорректно";
			}
		}

		public override void SaveToDatabase()
		{
			_DealVariantService.Save(DbEntity);
		}
	}
}