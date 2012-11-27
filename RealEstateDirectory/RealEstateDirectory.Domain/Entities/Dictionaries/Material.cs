using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    /// <summary>
    /// Материал строения
    /// </summary>
    public class Material : BaseDictionary
    {
        protected Material()
        {
        }

        public Material(string name)
            : base(name)
        {
        }
    }
}