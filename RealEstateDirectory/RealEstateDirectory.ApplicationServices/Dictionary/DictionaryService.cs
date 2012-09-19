using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Infrastructure.NHibernate.DbSession;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public abstract class DictionaryService<T> : BaseService<T>, IDictionaryService<T> where T : BaseDictionary
    {
        protected readonly IServiceLocator ServiceLocator;

        #region Поля

        protected IRealEstateService<RealEstate> RealEstateService;
        protected IRealEstateService<Building> BuildingService;
        protected IRealEstateService<Apartment> ApartmentService;
        protected IRealEstateService<Plot> PlotService;
        protected IRealEstateService<House> HouseService;
        protected IRealEstateService<Flat> FlatService;

        #endregion

        #region Конструктор

        protected DictionaryService(IPersistenceContext persistenceContext, IDictionaryRepository<T> repository, IServiceLocator serviceLocator)
            : base(persistenceContext, repository)
        {
            ServiceLocator = serviceLocator;
            RealEstateService = ServiceLocator.GetInstance<IRealEstateService<RealEstate>>();
        }

        #endregion

        #region Методы

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(T entity)
        {
            if (IsPossibilityToDelete(entity))
            {
                using (new DbSession(PersistenceContext))
                {
                    using (var transaction = PersistenceContext.CurrentSession.BeginTransaction())
                    {
                        Repository.Delete(entity);
                        transaction.Commit();
                    }
                }
            }
            throw new Exception("Нельзя удалить элемент. Он уже используется.");
        }

        /// <summary>
        /// Сохранить сущность
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override T Save(T entity)
        {
            using (new DbSession(PersistenceContext))
            {
                if (IsNameUniqueness(entity))
                {
                    using (var transaction = PersistenceContext.CurrentSession.BeginTransaction())
                    {
                        Repository.SaveOrUpdate(entity);
                        transaction.Commit();

                        return entity;
                    }
                }

                throw new Exception("Такой элемент справочника уже есть.");
            }
        }

        /// <summary>
        /// Проверка возможности удалить сущность. При невозможности - выбрасывается исключение.
        /// </summary>
        /// <param name="entity"></param>
        public abstract bool IsPossibilityToDelete(T entity);

        /// <summary>
        /// Проверка наименования на уникальность
        /// </summary>
        /// <param name="entity"></param>
        public bool CheckNameUniqueness(T entity)
        {
            using (new DbSession(PersistenceContext))
            {
                return Repository.AsQueryable().Count(x => x.Name == entity.Name && x.Id != entity.Id) == 0;
            }
        }

        private bool IsNameUniqueness(T entity)
        {
            return Repository.AsQueryable().Count(x => x.Name == entity.Name && x.Id != entity.Id) == 0;
        }

        #endregion
    }
}