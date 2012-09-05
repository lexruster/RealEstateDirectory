using System.Data;
using NHibernate;
using RealEstateDirectory.Infrastructure.NHibernate.SessionManager;
using RealEstateDirectory.Infrastructure.UnitOfWork;

namespace RealEstateDirectory.Infrastructure.NHibernate.UnitOfWork
{
    internal class NHibernateUnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;
        private ITransaction _transaction;

        public NHibernateUnitOfWork(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _session = SessionProvider.CurrentSession;
            _transaction = _session.BeginTransaction(isolationLevel);
        }

        #region IUnitOfWork Members

        public void Dispose()
        {
            if (!_transaction.WasCommitted && !_transaction.WasRolledBack)
            {
                _transaction.Rollback();
            }
            _transaction.Dispose();
            _transaction = null;
        }

        public void Commit()
        {
            _transaction.Commit();
            // TODO: подумать, надо ли очищать сессию для получения более актуальных данных из БД, а не из сессии
            // session.Clear();
        }

        #endregion
    }
}