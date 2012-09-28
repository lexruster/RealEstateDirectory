using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.ApplicationServices
{
    public class ResidenceService : RealEstateService<Residence>, IResidenceService
    {
        #region Конструктор

        public ResidenceService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            :base(persistenceContext, serviceLocator)
        {
        }

        #endregion
    }
}