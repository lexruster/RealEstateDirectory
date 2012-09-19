using System.Collections.Generic;
using System.Linq;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Infrastructure.NHibernate.DbSession;
using RealEstateDirectory.Infrastructure.Repositories;

namespace RealEstateDirectory.ApplicationServices
{
    public abstract class BaseService<T> : IBaseService<T> where T : Entity<int>
    {
        #region Поля

        protected readonly IPersistenceContext PersistenceContext;
        protected readonly IRepositoryWithTypedId<T, int> Repository;

        #endregion

        #region Конструктор

        protected BaseService(IPersistenceContext persistenceContext, IRepositoryWithTypedId<T, int> repository)
        {
            PersistenceContext = persistenceContext;
            Repository = repository;
        }

        #endregion

        #region Методы


        public IList<T> GetAll()
        {
            return Repository.GetAll();
        }

        public IQueryable<T> GetQueryable()
        {
            return Repository.AsQueryable();
        }

        /// <summary>
        /// Получить по ИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(int id)
        {
            using (new DbSession(PersistenceContext))
            {
                return Repository.Get(id);
            }
        }

        /// <summary>
        /// Сохранить сущность
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual T Save(T entity)
        {
            using (new DbSession(PersistenceContext))
            {
                using (var transaction = PersistenceContext.CurrentSession.BeginTransaction())
                {
                    Repository.SaveOrUpdate(entity);
                    transaction.Commit();

                    return entity;
                }
            }
        }

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
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

        #endregion
    }
}