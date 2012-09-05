using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Infrastructure.Repositories
{
    public interface IRepositoryBase<T> : IRepositoryWithTypedId<T, int>
        where T : Entity<int>
    {
    }
}
