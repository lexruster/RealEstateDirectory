using System.Collections.Generic;
using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    /// <summary>
    /// Район, регион
    /// </summary>
    public class District : BaseDictionary
    {
        /// <summary>
        /// Улицы района
        /// </summary>
        public virtual IList<Street> Streets { get; set; }

        protected District()
        {
        }

        public District(string name)
            : base(name)
        {
        }
    }
}