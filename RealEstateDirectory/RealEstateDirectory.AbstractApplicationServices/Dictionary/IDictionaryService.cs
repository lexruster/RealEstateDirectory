using System.Collections.Generic;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.AbstractApplicationServices.Dictionary
{
    public interface IDictionaryService<T>:IDataEntityBaseService<T> where T : BaseDictionary
    {
    }
}