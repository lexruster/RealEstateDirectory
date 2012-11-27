using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    /// <summary>
    /// ������ �����
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