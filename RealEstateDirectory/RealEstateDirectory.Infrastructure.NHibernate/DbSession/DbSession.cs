using System;
using NHibernate;
using NHibernate.Context;
using RealEstateDirectory.DataAccess;

namespace RealEstateDirectory.Infrastructure.NHibernate.DbSession
{
    public class DbSession :IDisposable
    {
        private readonly ISessionFactory _sessionFactory;

        public DbSession(IPersistenceContext persistentContext)
        {
            _sessionFactory = persistentContext.SessionFactory;
            CurrentSessionContext.Bind(_sessionFactory.OpenSession());
        }

        public void Dispose()
        {
            var session = CurrentSessionContext.Unbind(_sessionFactory);
            if (session != null && session.IsOpen)
            {
                try
                {
                    if (session.Transaction != null && session.Transaction.IsActive)
                    {
                        session.Transaction.Rollback();
                    }
                }
                finally
                {
                    session.Dispose();
                }
            }
        }
    }
}