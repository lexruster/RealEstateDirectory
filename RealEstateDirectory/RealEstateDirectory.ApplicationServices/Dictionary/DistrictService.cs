using System.Linq;
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
        #endregion

        #region Конструктор

        protected DistrictService(IPersistenceContext persistenceContext, IDictionaryRepository<District> repository, IServiceLocator serviceLocator)
            : base(persistenceContext, repository, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override bool IsPossibilityToDelete(District entity)
        {
            return RealEstateService.GetQueryable().Count(x => x.District == entity) == 0;
        }

        #endregion
    }
}