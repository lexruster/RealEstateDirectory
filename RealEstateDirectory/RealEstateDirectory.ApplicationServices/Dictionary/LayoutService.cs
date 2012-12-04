using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Common;
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

		public override ValidationResult IsPossibilityToDelete(Layout entity)
        {
			var result = new ValidationResult();
			if (!Repository.IsPossibleToDeleteLayout(entity))
			{
				result.FailValidation("Элемент уже используется в системе");
			}

			return result;
        }

		public Layout Create(string name)
		{
			return new Layout(name);
		}

        #endregion
    }
}