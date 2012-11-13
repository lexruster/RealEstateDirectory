using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using RealEstateDirectory.DataAccess;

namespace RealEstateDirectory.Infrastructure.NHibernate.PersistenceContext
{

    public class PersistenceContext : IPersistenceContext, IDisposable
    {
        private readonly Configuration _configuration;
        private readonly ISessionFactory _sessionFactory;
        
        public Configuration Configuration { get { return _configuration; } }
        public ISessionFactory SessionFactory { get { return _sessionFactory; } }

        public PersistenceContext(Configuration configuration)
        {
            _configuration = configuration;
            _sessionFactory = _configuration.BuildSessionFactory();
        }

        public ISession CurrentSession
        {
            get
            {
                if (!CurrentSessionContext.HasBind(SessionFactory))
                {
                    OnContextualSessionIsNotFound();
                }
                var contextualSession = SessionFactory.GetCurrentSession();
                if (contextualSession == null)
                {
                    OnContextualSessionIsNotFound();
                }
                return contextualSession;
            }
        }

        public void Dispose()
        {
            SessionFactory.Dispose();
        }

        private static void OnContextualSessionIsNotFound()
        {
            throw new InvalidOperationException("Контекстуальная сессия потеряна. Откройте сессию БД перед работой с БД.");
        }
    }
}