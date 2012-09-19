using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Infrastructure.NHibernate.DbSession;

namespace RealEstateDirectory.ApplicationServices
{
    public class RealEstateService<T> : BaseService<T>, IRealEstateService<T> where T : RealEstate
    {
        protected readonly IServiceLocator ServiceLocator;

        #region Поля

        #endregion

        #region Конструктор

        protected RealEstateService(IPersistenceContext persistenceContext, IRealEstateRepository<T> repository, IServiceLocator serviceLocator) :
            base(persistenceContext, repository)
        {
            ServiceLocator = serviceLocator;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(T entity)
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