using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class OwnershipService : DictionaryService<Ownership>, IOwnershipService
    {
        #region ����

		public override string DictionaryName
		{
			get { return "�������������"; }
		}

        #endregion

        #region �����������

        public OwnershipService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region ������

        

        public override bool IsPossibilityToDelete(Ownership entity)
        {
            return Repository.IsPossibleToDeleteOwnership(entity);
        }

		public Ownership Create(string name)
		{
			return new Ownership(name);
		}

        #endregion
    }
}