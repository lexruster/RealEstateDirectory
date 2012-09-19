using System.Collections.Generic;
using System.Linq;
using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.AbstractApplicationServices.Dictionary
{
    public interface IDictionaryService<T>:IBaseService<T> where T : BaseDictionary
	{
        /// <summary>
        /// Проверка возможности удалить сущность.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool IsPossibilityToDelete(T entity);

        bool CheckNameUniqueness(T entity);
	}
}