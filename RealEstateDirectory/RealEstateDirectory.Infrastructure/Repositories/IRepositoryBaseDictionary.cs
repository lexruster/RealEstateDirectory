using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Infrastructure.Repositories
{
    public interface IRepositoryBaseDictionary<T> : IRepositoryWithTypedId<T, int>
        where T : BaseDictionary
    {
        /// <summary>
        /// Получение элемента по наименованию
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        T Get(string name);
    }
}