using System.Collections.Generic;
using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Infrastructure.Repositories
{
    public interface IRepositoryWithTypedId<IdT>
    {
        T Get<T>(IdT id);
        IEnumerable<T> GetAll<T>();
        void SaveOrUpdate<T>(T entity);
        void Delete<T>(T entity);
    }
}