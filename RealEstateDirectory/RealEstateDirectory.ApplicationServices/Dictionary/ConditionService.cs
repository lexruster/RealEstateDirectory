using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
	public class ConditionService : DictionaryService<Condition>, IConditionService
    {
        #region Поля

		public override string DictionaryName
		{
			get { return "Состояние"; }
		}

        #endregion

        #region Конструктор

		public ConditionService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region Методы

		public override ValidationResult IsPossibilityToDelete(Condition entity)
        {
			var result = new ValidationResult();
			if (!Repository.IsPossibleToDeleteCondition(entity))
			{
				result.FailValidation("Элемент уже используется в системе");
			}

			return result;
        }

		public Condition Create(string name)
		{
			return new Condition(name);
		}

        #endregion
    }
}