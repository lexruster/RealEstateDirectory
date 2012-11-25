using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.AbstractApplicationServices.Dictionary
{
	public interface IDictionaryWithOnlyNameEntitiesService<T> : IDictionaryService<T> where T : BaseDictionary
	{
		T Create(string name);
	}
}