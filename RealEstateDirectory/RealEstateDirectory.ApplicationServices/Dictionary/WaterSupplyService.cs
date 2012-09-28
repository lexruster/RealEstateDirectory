using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class WaterSupplyService : DictionaryService<WaterSupply>, IWaterSupplyService
    {
        #region Поля
        #endregion

        #region Конструктор

        public WaterSupplyService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override bool IsPossibilityToDelete(WaterSupply entity)
        {
            return Repository.IsPossibleToDeleteWaterSupply(entity);
        }

        #endregion
    }
}