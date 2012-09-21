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

        protected RealtorService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override bool IsPossibilityToDelete(Realtor entity)
        {
            return Repository.IsPossibleToDeleteRealtor(entity);
        }

        #endregion
    }
}