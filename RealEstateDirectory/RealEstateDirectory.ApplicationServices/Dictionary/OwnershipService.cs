using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class OwnershipService : DictionaryService<Ownership>, IOwnershipService
    {
        #region Поля

		public override string DictionaryName
		{
			get { return "Собственность"; }
		}

        #endregion

        #region Конструктор

        public OwnershipService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region Методы

        

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