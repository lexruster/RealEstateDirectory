using System.Collections.Generic;
using System.Linq;
using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.AbstractApplicationServices
{
    public interface IBaseService<T> where T : Entity<int>
    {
        /// <summary>
        /// Получить все
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll();

        IQueryable<T> GetQueryable();

        /// <summary>
        /// Получить по ИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// Сохранить сущность
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Save(T entity);

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
	}
}