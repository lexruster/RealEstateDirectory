namespace RealEstateDirectory.Data.Entities
{
	/// <summary>
	/// Земельный участок.
	/// </summary>
	public class Plot : RealEstate
	{
		/// <summary>
		/// Площадь участка
		/// </summary>
		public virtual decimal? PlotSquare { get; set; }
	}
}