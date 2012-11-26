using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    /// <summary>
    /// Водоснабжение
    /// </summary>
    public class WaterSupply : BaseDictionary
    {
        protected WaterSupply()
        {
        }

        public WaterSupply(string name)
            : base(name)
        {
        }
    }
}