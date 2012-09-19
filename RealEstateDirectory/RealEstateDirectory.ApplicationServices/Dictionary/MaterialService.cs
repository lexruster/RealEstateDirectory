using System.Linq;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class MaterialService : DictionaryService<Material>, IMaterialService
    {
        #region Поля
        #endregion

        #region Конструктор

        protected MaterialService(IPersistenceContext persistenceContext, IDictionaryRepository<Material> repository, IServiceLocator serviceLocator)
            : base(persistenceContext, repository, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override bool IsPossibilityToDelete(Material entity)
        {
            return (HouseService.GetQueryable().Count(x => x.Material == entity) == 0) && ((BuildingService.GetQueryable().Count(x => x.Material == entity) == 0));
        }

        #endregion
    }
}