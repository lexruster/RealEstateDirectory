using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class RealtorService : DictionaryService<Realtor>, IRealtorService
    {
        #region Поля

		public override string DictionaryName
		{
			get { return "Риэлторы"; }
		}

        #endregion

        #region Конструктор

        public RealtorService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region Методы

		public override ValidationResult IsPossibilityToDelete(Realtor entity)
        {
			var result = new ValidationResult();
			if (!Repository.IsPossibleToDeleteRealtor(entity))
			{
				result.FailValidation("Элемент уже используется в системе");
			}

			return result;
        }

        #endregion
    }
}