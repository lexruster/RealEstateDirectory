using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Infrastructure.Repositories;

namespace RealEstateDirectory.Infrastructure.NHibernate.Repositories
{
    public class RepositoryWithTypedIdBase<TId> : IRepositoryWithTypedId<TId>
    {
        protected readonly IPersistenceContext PersistenceContext;

        /// <summary>
        /// Текущая сессия NHibernate
        /// </summary>
        protected ISession CurrentSession
        {
            get { return PersistenceContext.CurrentSession; }
        }

        public RepositoryWithTypedIdBase(IPersistenceContext persistenceContext)
        {
            PersistenceContext = persistenceContext;
        }

        public T Get<T>(TId id)
        {
            return CurrentSession.Get<T>(id);
        }

        public IEnumerable<T> GetAll<T>()
        {
            return CurrentSession.Query<T>();
        }

        public void SaveOrUpdate<T>(T entity)
        {
            CurrentSession.SaveOrUpdate(entity);
            CurrentSession.Flush();
        }

        public void Delete<T>(T entity)
        {
            CurrentSession.Delete(entity);
            CurrentSession.Flush();
        }
    }
}