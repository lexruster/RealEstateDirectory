using System;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Dictionaries.RealtorAgencyDictionary
{
	[NotifyForAll]
	public class RealtorAgencyViewModel : DictionaryEntityViewModel<RealtorAgency>
	{
		public RealtorAgencyViewModel(IRealtorAgencyService service)
		{
			_DictionaryService = service;
		}

		#region Infrastructure

		private readonly IRealtorAgencyService _DictionaryService;

		#endregion

		public int Id { get; set; }
		public string Name { get; set; }
		public string Director { get; set; }
		public string Description { get; set; }
		public string Contacts { get; set; }
		public string Address { get; set; }

		public override void UpdateValuesFromModel()
		{
			Id = DbEntity.Id;
			Name = DbEntity.Name;
			Address = DbEntity.Address;
			Contacts = DbEntity.Contacts;
			Description = DbEntity.Description;
			Director = DbEntity.Director;
		}

		public override void UpdateModelFromValues()
		{
			DbEntity.Name = Name;
			DbEntity.Address = Address;
			DbEntity.Contacts = Contacts;
			DbEntity.Description = Description;
			DbEntity.Director = Director;
		}

		public override string this[string columnName]
	    {
	        get
	        {
	            if (columnName == PropertySupport.ExtractPropertyName(() => Name))
	            {
	                if (String.IsNullOrWhiteSpace(Name))
	                    return String.Format("Агентство должно иметь название.");
	            }
	            return null;
	        }
	    }

	    public override string Error
		{
			get
			{
				var dictElement = new RealtorAgency(Name);
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