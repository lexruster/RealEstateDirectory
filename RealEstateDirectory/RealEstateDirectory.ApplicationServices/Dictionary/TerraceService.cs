using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class TerraceService : DictionaryService<Terrace>, ITerraceService
    {
        #region ����
        #endregion

        #region �����������

        public TerraceService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region ������

        public override string DictionaryName
        {
            get { return "������"; }
        }

        public override bool IsPossibilityToDelete(Terrace entity)
        {
            return Repository.IsPossibleToDeleteTerrace(entity);
        }

        #endregion
    }
}