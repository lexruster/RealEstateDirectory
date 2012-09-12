using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Infrastructure.Repositories;

namespace RealEstateDirectory.Infrastructure.NHibernate.Repositories
{
    public class RepositoryWithTypedIdBase<T, TId> : IRepositoryWithTypedId<T, TId>
        where T : Entity<TId>
    {
        protected readonly IPersistenceContext PersistenceContext;

        /// <summary>
        /// ������� ������ NHibernate
        /// </summary>
        protected ISession CurrentSession
        {
            get { return PersistenceContext.CurrentSession; }
        }

        public RepositoryWithTypedIdBase(IPersistenceContext persistenceContext)
        {
            PersistenceContext = persistenceContext;
            PersistenceContext = persistenceContext;
        }

        public T Get(TId id)
        {
            return CurrentSession.Get<T>(id);
        }

        public IList<T> GetAll()
        {
            return CurrentSession.CreateCriteria(typeof(T)).List<T>();
        }

        public IQueryable<T> AsQueryable()
        {
            return CurrentSession.Query<T>();
        }

        public T SaveOrUpdate(T entity)
        {
            CurrentSession.SaveOrUpdate(entity);

            return entity;
        }

        public void Delete(T entity)
        {
            CurrentSession.Delete(entity);
        }
    }
}