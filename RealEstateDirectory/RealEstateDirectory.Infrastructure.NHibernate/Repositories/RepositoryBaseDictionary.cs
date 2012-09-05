using System.Linq;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Infrastructure.Repositories;

namespace RealEstateDirectory.Infrastructure.NHibernate.Repositories
{
    /// <summary>
    /// ������� ����� ��� ������������
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