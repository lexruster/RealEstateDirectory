namespace RealEstateDirectory.Data.Entities
{
	/// <summary>
	/// Квартира.
	/// </summary>
	public class Flat : Apartment
	{
		/// <summary>
		/// Санузел.
		/// </summary>
		public virtual ToiletType ToiletType { get; set; }

		/// <summary>
		/// Жилая площадь.
		/// </summary>
		public virtual decimal? ResidentialSquare { get; set; }

		/// <summary>
		/// Кухня.
		/// </summary>
		public virtual decimal? KitchenSquare { get; set; }
	}
}