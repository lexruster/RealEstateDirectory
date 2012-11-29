using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class DistrictService : DictionaryService<District>, IDistrictService
    {
        #region Поля

        public override string DictionaryName
        {
            get { return "Районы"; }
        }

        #endregion

        #region Конструктор

        public DistrictService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region Методы

		public override ValidationResult IsPossibilityToDelete(District entity)
        {
			var result = new ValidationResult();
			if (!Repository.IsPossibleToDeleteDistrict(entity))
			{
				result.FailValidation("Элемент уже используется в системе");
			}

			return result;
        }

	    public District Create(string name)
	    {
		    return new District(name);
	    }

	    #endregion
    }
}