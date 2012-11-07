using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class DistrictService : DictionaryService<District>, IDistrictService
    {
        #region ����

        public override string DictionaryName
        {
            get { return "������"; }
        }

        #endregion

        #region �����������

        public DistrictService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region ������

        public override bool IsPossibilityToDelete(District entity)
        {
            return Repository.IsPossibleToDeleteDistrict(entity);
        }

        #endregion
    }
}