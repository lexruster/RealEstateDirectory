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

        protected StreetService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override bool IsPossibilityToDelete(Street entity)
        {
            return Repository.IsPossibleToDeleteStreet(entity);
        }

        #endregion
    }
}