using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Infrastructure.NHibernate.Repositories;

namespace RealEstateDirectory.Domain.Data.Repository
{
    public class LayoutRepository : RepositoryBaseDictionary<Layout>, ILayoutRepository
    {
        public LayoutRepository(IPersistenceContext persistentContext):base(persistentContext)
        {
            
        }
    }
}
