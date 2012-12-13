using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class SewageService : DictionaryService<Sewage>, ISewageService
    {
        #region Поля

		public override string DictionaryName
		{
			get { return "Канализация"; }
		}

        #endregion

        #region Конструктор

        public SewageService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region Методы

		public override ValidationResult IsPossibilityToDelete(Sewage entity)
        {
			var result = new ValidationResult();
			if (!Repository.IsPossibleToDeleteSewage(entity))
			{
				result.FailValidation("Элемент уже используется в системе");
			}

			return result;
        }

		public Sewage Create(string name)
		{
			return new Sewage(name);
		}

        #endregion
    }
}