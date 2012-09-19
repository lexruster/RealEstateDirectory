using System.Linq;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class TerraceService : DictionaryService<Terrace>, ITerraceService
    {
        #region Поля
        #endregion

        #region Конструктор

        protected TerraceService(IPersistenceContext persistenceContext, IDictionaryRepository<Terrace> repository, IServiceLocator serviceLocator)
            : base(persistenceContext, repository, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override bool IsPossibilityToDelete(Terrace entity)
        {
            return ApartmentService.GetQueryable().Count(x => x.Terrace == entity) == 0;
        }

        #endregion
    }
}