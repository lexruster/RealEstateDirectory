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

        public FloorLevelService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region ������

        public override string DictionaryName
        {
            get { return "������ ��������"; }
        }

        public override bool IsPossibilityToDelete(FloorLevel entity)
        {
            return Repository.IsPossibleToDeleteFloorLevel(entity);
        }

        #endregion
    }
}