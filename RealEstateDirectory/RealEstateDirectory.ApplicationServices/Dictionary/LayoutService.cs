using System.Linq;
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

        protected LayoutService(IPersistenceContext persistenceContext, IDictionaryRepository<Layout> repository, IServiceLocator serviceLocator)
            : base(persistenceContext, repository, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override bool IsPossibilityToDelete(Layout entity)
        {
            return ApartmentService.GetQueryable().Count(x => x.Layout == entity) == 0;
        }

        #endregion
    }
}