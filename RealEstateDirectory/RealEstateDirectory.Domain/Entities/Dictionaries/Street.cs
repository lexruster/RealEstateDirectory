using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    public class Street : BaseDictionary
    {
        protected Street()
        {
        }

        public Street(string name)
            : base(name)
        {
        }
    }
}