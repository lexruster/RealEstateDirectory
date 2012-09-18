using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Infrastructure.NHibernate.Repositories;
using RealEstateDirectory.Infrastructure.Repositories;

namespace RealEstateDirectory.Domain.Data.Repository
{
    public class RealEstateRepository<T> : RepositoryWithTypedIdBase<T, int>, IRealEstateRepository<T>
        where T : RealEstate
    {
        public RealEstateRepository(IPersistenceContext persistentContext)
            : base(persistentContext)
        {

        }
    }
}