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

        protected HouseService(IPersistenceContext persistenceContext,  IServiceLocator serviceLocator) :
            base(persistenceContext, serviceLocator)
        {
        }

        #endregion
    }
}