using System.Linq;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class OwnershipService : DictionaryService<Ownership>, IOwnershipService
    {
        #region Поля
        #endregion

        #region Конструктор

        protected OwnershipService(IPersistenceContext persistenceContext, IDictionaryRepository<Ownership> repository, IServiceLocator serviceLocator)
            : base(persistenceContext, repository, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override bool IsPossibilityToDelete(Ownership entity)
        {
            return RealEstateService.GetQueryable().Count(x => x.Ownership == entity) == 0;
        }

        #endregion
    }
}