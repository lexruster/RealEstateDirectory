using System.Collections.Generic;
using System.Linq;
using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.AbstractApplicationServices.Dictionary
{
    public interface IDictionaryService<T>:IDataEntityBaseService<T> where T : BaseDictionary
	{
	}
}