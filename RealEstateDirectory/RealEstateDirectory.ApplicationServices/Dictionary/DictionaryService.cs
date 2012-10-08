using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Infrastructure.NHibernate.DbSession;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public abstract class DictionaryService<T> : IDictionaryService<T>
        where T : BaseDictionary
    {
        protected readonly IPersistenceContext PersistenceContext;
        protected readonly IDictionaryRepository Repository;
        protected readonly IServiceLocator ServiceLocator;
        private static DbSession Session;

        #region Поля

        #endregion

        #region Конструктор

        public DictionaryService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
        {
            PersistenceContext = persistenceContext;
            ServiceLocator = serviceLocator;
            Repository = ServiceLocator.GetInstance<IDictionaryRepository>();
        }

        #endregion

        #region Методы

        public void StartSession()
        {
            Session = new DbSession(ServiceLocator.GetInstance<IPersistenceContext>());
        }

        public void StopSession()
        {
            Session.Dispose();
        }

        public IEnumerable<T> GetAll()
        {
            return Repository.GetAll<T>();
        }

        /// <summary>
        /// Получить по ИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(int id)
        {
            return Repository.Get<T>(id);
        }

        /// <summary>
        /// Сохранить сущность
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual void Save(T entity)
        {
            using (var transaction = PersistenceContext.CurrentSession.BeginTransaction())
            {
                Repository.SaveOrUpdate(entity);

                transaction.Commit();
            }
        }

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            using (var transaction = PersistenceContext.CurrentSession.BeginTransaction())
            {
                Repository.Delete(entity);
                transaction.Commit();
            }
        }

        public virtual bool IsPossibilityToDelete(T entity)
        {
            //Пока удалять можно все
            return true;
        }

        protected bool IsNameUniquenessInner(T entity)
        {
            return Repository.IsNameUniqueness(entity);
        }

        public bool IsValid(T entity)
        {
            //Пока проверяем просто уникальность имени
            return IsNameUniquenessInner(entity);
        }

        #endregion
    }
}