using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Infrastructure.NHibernate.Repositories;

namespace RealEstateDirectory.Domain.Data.Repository
{
    public class LayoutRepository : RepositoryBaseDictionary<Layout>, ILayoutRepository
    {
    }
}
