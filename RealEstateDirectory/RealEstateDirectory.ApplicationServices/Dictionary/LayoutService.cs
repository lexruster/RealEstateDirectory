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

		public override string DictionaryName
		{
			get { return "Планировка"; }
		}

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

		public Layout Create(string name)
		{
			return new Layout(name);
		}

        #endregion
    }
}