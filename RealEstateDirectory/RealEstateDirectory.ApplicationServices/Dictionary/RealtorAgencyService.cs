using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class RealtorAgencyService : DictionaryService<RealtorAgency>, IRealtorAgencyService
    {
        #region Поля

		public override string DictionaryName
		{
			get { return "Риэлторские агентства"; }
		}

        #endregion

        #region Конструктор

		public RealtorAgencyService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region Методы

		public override ValidationResult IsPossibilityToDelete(RealtorAgency entity)
        {
			var result = new ValidationResult();
			if (!Repository.IsPossibleToDeleteRealtorAgency(entity))
			{
				result.FailValidation("Элемент уже используется в системе");
			}

			return result;
        }

        #endregion
    }
}