using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Infrastructure.NHibernate.Repositories;
using RealEstateDirectory.Infrastructure.Repositories;

namespace RealEstateDirectory.Domain.Data.Repository
{
    public class DictionaryRepository<T> : RepositoryBaseDictionary<T>, IDictionaryRepository<T> where T : BaseDictionary
    {
        public DictionaryRepository(IPersistenceContext persistentContext)
            : base(persistentContext)
        {
            
        }
    }
}
