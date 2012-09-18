using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    /// <summary>
    /// Канализация
    /// </summary>
    public class Sewage : BaseDictionary
    {
        protected Sewage()
        {
        }

        public Sewage(string name)
            : base(name)
        {
        }
    }
}