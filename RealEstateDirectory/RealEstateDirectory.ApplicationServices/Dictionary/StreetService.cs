using System.Linq;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class StreetService : DictionaryService<Street>, IStreetService
    {
        #region Поля
        #endregion

        #region Конструктор

        protected StreetService(IPersistenceContext persistenceContext, IDictionaryRepository<Street> repository, IServiceLocator serviceLocator)
            : base(persistenceContext, repository, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override bool IsPossibilityToDelete(Street entity)
        {
            return RealEstateService.GetQueryable().Count(x => x.Street == entity) == 0;
        }

        #endregion
    }
}