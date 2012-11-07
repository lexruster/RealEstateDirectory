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

        public TerraceService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override string DictionaryName
        {
            get { return "Балкон"; }
        }

        public override bool IsPossibilityToDelete(Terrace entity)
        {
            return Repository.IsPossibleToDeleteTerrace(entity);
        }

        #endregion
    }
}