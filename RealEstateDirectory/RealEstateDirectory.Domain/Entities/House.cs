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

        /// <summary>
        /// Снабжение водой
        /// </summary>
        public virtual WaterSupply WaterSupply { get; set; }

        /// <summary>
        /// Канализация
        /// </summary>
        public virtual Sewage Sewage { get; set; }

        /// <summary>
        /// Снабжение водой
        /// </summary>
        public virtual bool? HasBathhouse { get; set; }

        /// <summary>
        /// Снабжение водой
        /// </summary>
        public virtual bool? HasGarage { get; set; }

        /// <summary>
        /// Снабжение водой
        /// </summary>
        public virtual bool? HasGas{ get; set; }

        /// <summary>
        /// Материал дома
        /// </summary>
        public virtual Material Material { get; set; }
	}
}
