using System;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class DealVariantService : DictionaryService<DealVariant>, IDealVariantService
    {
        #region Поля

		public override string DictionaryName
		{
			get { return "Варианты сделок"; }
		}

        #endregion

        #region Конструктор

        public DealVariantService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override ValidationResult IsPossibilityToDelete(DealVariant entity)
        {
	        var result = new ValidationResult();
			if (!Repository.IsPossibleToDeleteDealVariant(entity))
			{
				result.FailValidation("Элемент уже используется в системе");
			}

	        return result;
        }

		public DealVariant Create(string name)
		{
			return new DealVariant(name);
		}

        #endregion
    }
}