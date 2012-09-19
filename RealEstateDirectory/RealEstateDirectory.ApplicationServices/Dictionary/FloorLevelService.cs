using System.Linq;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class FloorLevelService : DictionaryService<FloorLevel>, IFloorLevelService
    {
        #region ����
        #endregion

        #region �����������

        protected FloorLevelService(IPersistenceContext persistenceContext, IDictionaryRepository<FloorLevel> repository, IServiceLocator serviceLocator)
            : base(persistenceContext, repository,serviceLocator)
        {
        }

        #endregion

        #region ������

        public override bool IsPossibilityToDelete(FloorLevel entity)
        {
            return ApartmentService.GetQueryable().Count(x => x.FloorLevel == entity) == 0;
        }

        #endregion
    }
}