using System;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class DealVariantService: DictionaryService<DealVariant> , IDealVariantService
    {
        #region Поля
        #endregion

        #region Конструктор

        protected DealVariantService(IPersistenceContext persistenceContext, IDictionaryRepository<DealVariant> repository, IServiceLocator serviceLocator)
            : base(persistenceContext, repository, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override bool IsPossibilityToDelete(DealVariant entity)
        {
            return RealEstateService.GetQueryable().Count(x => x.DealVariant == entity) == 0;
        }

        #endregion
    }
}