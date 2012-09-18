using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    /// <summary>
    /// Район, регион
    /// </summary>
    public class District : BaseDictionary
    {
        protected District()
        {
        }

        public District(string name)
            : base(name)
        {
        }
    }
}