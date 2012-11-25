using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    public class Street : BaseDictionary
    {
        /// <summary>
        /// Район
        /// </summary>
        public virtual District District { get; set; }

        protected Street()
        {
        }

        public Street(string name)
            : base(name)
        {
        }
    }
}