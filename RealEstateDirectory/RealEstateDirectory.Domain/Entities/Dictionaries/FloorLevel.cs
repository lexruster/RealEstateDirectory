using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    /// <summary>
    /// Высота этажа
    /// </summary>
	public class FloorLevel : BaseDictionary
	{
		protected FloorLevel()
		{
		}

        public FloorLevel(string name)
            : base(name)
		{
		}
	}
}