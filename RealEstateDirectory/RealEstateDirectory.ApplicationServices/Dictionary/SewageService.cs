using System.Linq;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class SewageService : DictionaryService<Sewage>, ISewageService
    {
        #region ����
        #endregion

        #region �����������

        protected SewageService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region ������

        public override bool IsPossibilityToDelete(Sewage entity)
        {
            return Repository.IsPossibleToDeleteSewage(entity);
        }

        #endregion
    }
}