using System.Linq;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class RealtorService : DictionaryService<Realtor>, IRealtorService
    {
        #region Поля
        #endregion

        #region Конструктор

        protected RealtorService(IPersistenceContext persistenceContext, IDictionaryRepository<Realtor> repository, IServiceLocator serviceLocator)
            : base(persistenceContext, repository, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override bool IsPossibilityToDelete(Realtor entity)
        {
            return RealEstateService.GetQueryable().Count(x => x.Realtor == entity) == 0;
        }

        #endregion
    }
}