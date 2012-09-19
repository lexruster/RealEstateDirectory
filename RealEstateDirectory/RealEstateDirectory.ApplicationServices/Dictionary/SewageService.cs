using System.Linq;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class SewageService : DictionaryService<Sewage>, ISewageService
    {
        #region Поля
        #endregion

        #region Конструктор

        protected SewageService(IPersistenceContext persistenceContext, IDictionaryRepository<Sewage> repository, IServiceLocator serviceLocator)
            : base(persistenceContext, repository, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override bool IsPossibilityToDelete(Sewage entity)
        {
            return HouseService.GetQueryable().Count(x => x.Sewage == entity) == 0;
        }

        #endregion
    }
}