using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.ApplicationServices
{
    public class FlatService : RealEstateService<Flat>, IFlatService
    {
        #region Конструктор

        protected FlatService(IPersistenceContext persistenceContext, IRealEstateRepository<Flat> repository, IServiceLocator serviceLocator) :
            base(persistenceContext, repository,serviceLocator)
        {
        }

        #endregion
    }
}
