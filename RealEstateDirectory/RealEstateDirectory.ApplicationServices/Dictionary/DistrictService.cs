using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
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

        public override bool IsPossibilityToDelete(District entity)
        {
            return Repository.IsPossibleToDeleteDistrict(entity);
        }

        #endregion
    }
}