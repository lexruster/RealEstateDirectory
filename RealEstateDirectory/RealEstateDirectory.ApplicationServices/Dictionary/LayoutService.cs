using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class LayoutService : DictionaryService<Layout>, ILayoutService
    {
        #region Поля
        #endregion

        #region Конструктор

        public LayoutService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override bool IsPossibilityToDelete(Layout entity)
        {
            return Repository.IsPossibleToDeleteLayout(entity);
        }

        #endregion
    }
}