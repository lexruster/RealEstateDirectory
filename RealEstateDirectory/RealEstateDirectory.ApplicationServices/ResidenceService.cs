using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.ApplicationServices
{
    public class ResidenceService : RealEstateService<Residence>, IResidenceService
    {
        #region �����������

        protected ResidenceService(IPersistenceContext persistenceContext, IRealEstateRepository<Residence> repository, IServiceLocator serviceLocator)
            :
                base(persistenceContext, repository,serviceLocator)
        {
        }

        #endregion
    }
}