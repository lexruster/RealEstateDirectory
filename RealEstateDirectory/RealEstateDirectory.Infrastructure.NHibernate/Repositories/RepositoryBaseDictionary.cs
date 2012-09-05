using System.Linq;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Infrastructure.Repositories;

namespace RealEstateDirectory.Infrastructure.NHibernate.Repositories
{
    /// <summary>
    /// Базовый класс для репозиториев
    /// </summary>
    public class RepositoryBaseDictionary<T> : RepositoryWithTypedIdBase<T, int>, IRepositoryBaseDictionary<T>
        where T : BaseDictionary
    {
        public T Get(string name)
        {
            return AsQueryable().FirstOrDefault(x => x.Name == name);
        }
    }
}