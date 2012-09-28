using System.Collections.Generic;
using System.Linq;
using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.AbstractApplicationServices
{
    public interface IDataEntityBaseService<T> where T : Entity<int>
    {
        /// <summary>
        /// Получить все
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

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
        void Save(T entity);

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// Проверить корректность новой/измененной сущности
        /// </summary>
        /// <param name="entity"></param>
        bool IsValid(T entity);

        /// <summary>
        /// Проверка возможности удалить сущность.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool IsPossibilityToDelete(T entity);
	}
}