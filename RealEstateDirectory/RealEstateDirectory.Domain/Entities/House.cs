using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Domain.Entities
{
    /// <summary>
    /// Дом
    /// </summary>
    public class House : Plot
	{
        /// <summary>
        /// Этажей в доме
        /// </summary>
        public virtual int? TotalFloor { get; set; }

        /// <summary>
        /// Площадь дома
        /// </summary>
        public virtual decimal? HouseSquare { get; set; }
	}
}
