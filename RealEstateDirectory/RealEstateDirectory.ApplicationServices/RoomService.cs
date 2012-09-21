using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.ApplicationServices
{
    public class RoomService : RealEstateService<Room>, IRoomService
    {
        #region Конструктор

        protected RoomService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator) :
            base(persistenceContext, serviceLocator)
        {
        }

        #endregion
    }
}