namespace RealEstateDirectory.Domain.Entities
{
    /// <summary>
    /// Земельный участок
    /// </summary>
    public class Plot : RealEstate
	{
        /// <summary>
        /// Жилая площадь
        /// </summary>
        public virtual decimal? PlotSquare { get; set; }
	}
}
