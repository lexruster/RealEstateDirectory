using System.Collections.Generic;
using RealEstateDirectory.AbstractApplicationServices.Common;
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
        /// Проверить корректность новой/измененной сущности.
        /// Проверить корректность измененной можно без изменения, надо создать новую сузность, но указать в другом поле id старой.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id">id если надо проверить изменение сущности</param>
        ValidationResult IsValid(T entity, int id = 0);

        /// <summary>
        /// Проверка возможности удалить сущность.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
		ValidationResult IsPossibilityToDelete(T entity);

        void StartSession();
        void StopSession();
    }
}