using System;
using Microsoft.Practices.ServiceLocation;
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

        #endregion

        #region Конструктор

        public DealVariantService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override string DictionaryName
        {
            get { return "Варианты сделок"; }
        }

        public override bool IsPossibilityToDelete(DealVariant entity)
        {
            return Repository.IsPossibleToDeleteDealVariant(entity);
        }

        #endregion
    }
}