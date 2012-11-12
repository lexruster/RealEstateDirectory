using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Infrastructure.NHibernate.DbSession;

namespace RealEstateDirectory.ApplicationServices
{
    public abstract class RealEstateService<T> : IRealEstateService<T> where T : RealEstate
    {
        protected readonly IPersistenceContext PersistenceContext;
        protected readonly IRealEstateRepository Repository;
        protected readonly IServiceLocator ServiceLocator;
        private static DbSession Session;

        #region Поля

		public abstract string RealEstateName { get; }

        #endregion

        #region Конструктор

        public RealEstateService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
        {
            PersistenceContext = persistenceContext;
            ServiceLocator = serviceLocator;
            Repository = ServiceLocator.GetInstance<IRealEstateRepository>();
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

        public virtual IEnumerable<T> GetAll()
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
        public void Delete(T entity)
        {
            using (var transaction = PersistenceContext.CurrentSession.BeginTransaction())
            {
                Repository.Delete(entity);
                transaction.Commit();
            }
        }

        public bool IsPossibilityToDelete(T entity)
        {
            //Пока удалять можно все
            return true;
        }

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="entity"></param>
        public ValidationResult IsValid(T entity, int id = 0)
        {
            //Пока вводить можно все
            return new ValidationResult();
        }

        #endregion

	    
    }
}