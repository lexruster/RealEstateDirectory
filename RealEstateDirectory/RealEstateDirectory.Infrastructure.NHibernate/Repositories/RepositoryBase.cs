using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Infrastructure.Repositories;

namespace RealEstateDirectory.Infrastructure.NHibernate.Repositories
{
    /// <summary>
    /// Базовый класс для репозиториев
    /// </summary>
    public class RepositoryBase<T> : RepositoryWithTypedIdBase<T, int>, IRepositoryBase<T>
        where T : Entity<int>
    {
    }
}

    