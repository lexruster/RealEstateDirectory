using System.Collections.Generic;
using System.Linq;

namespace RealEstateDirectory.Infrastructure.Repositories
{
    public interface IRepositoryWithTypedId<T, IdT>
    {
        T Get(IdT id);
        IList<T> GetAll();
        IQueryable<T> AsQueryable();
        T SaveOrUpdate(T entity);
        void Delete(T entity);
    }
}