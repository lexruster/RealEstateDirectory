using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Domain.Entities
{
    /// <summary>
    /// Жилые помещения квартирного типа (квартира, комната)
    /// </summary>
    public class Building : RealEstate
	{
        /// <summary>
        /// Этаж
        /// </summary>
        public virtual int? Floor { get; set; }

        /// <summary>
        /// Всего этажей
        /// </summary>
        public virtual int? TotalFloor { get; set; }

        /// <summary>
        /// Общая площадь (жилая для комнат)
        /// </summary>
        public virtual decimal? TotalSquare { get; set; }

        /// <summary>
        /// Материал строения
        /// </summary>
        public virtual Material Material { get; set; }
	}
}
