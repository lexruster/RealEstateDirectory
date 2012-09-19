using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.ApplicationServices
{
    public class HouseService : RealEstateService<House>, IHouseService
    {
        #region Конструктор

        protected HouseService(IPersistenceContext persistenceContext, IRealEstateRepository<House> repository, IServiceLocator serviceLocator) :
            base(persistenceContext, repository,serviceLocator)
        {
        }

        #endregion
    }
}