using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Domain.Entities
{
    /// <summary>
    /// Квартира
    /// </summary>
	public class Flat : Apartment
	{
        /// <summary>
        /// Санитарный узел
        /// </summary>
        public virtual ToiletType ToiletType { get; set; }

        /// <summary>
        /// Общая площадь
        /// </summary>
        public virtual decimal? ResidentialSquare { get; set; }

        /// <summary>
        /// Кухня
        /// </summary>
        public virtual decimal? KitchenSquare { get; set; }
	}
}
