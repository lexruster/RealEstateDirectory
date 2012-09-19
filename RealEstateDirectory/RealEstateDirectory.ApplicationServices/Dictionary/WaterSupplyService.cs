using System.Linq;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class WaterSupplyService : DictionaryService<WaterSupply>, IWaterSupplyService
    {
        #region ����
        #endregion

        #region �����������

        protected WaterSupplyService(IPersistenceContext persistenceContext, IDictionaryRepository<WaterSupply> repository, IServiceLocator serviceLocator)
            : base(persistenceContext, repository, serviceLocator)
        {
        }

        #endregion

        #region ������

        public override bool IsPossibilityToDelete(WaterSupply entity)
        {
            return HouseService.GetQueryable().Count(x => x.WaterSupply == entity) == 0;
        }

        #endregion
    }
}