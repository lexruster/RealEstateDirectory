namespace RealEstateDirectory.Data.Entities
{
	/// <summary>
	/// Жилые помещения квартирного типа (квартира, комната).
	/// </summary>
	public class Apartment : Building
	{
		/// <summary>
		/// Общее число комнат.
		/// </summary>
		public virtual int? TotalRoomCount { get; set; }

		/// <summary>
		/// Планировка помещения.
		/// </summary>
		public virtual Layout Layout { get; set; }

		/// <summary>
		/// Балкон.
		/// </summary>
		public virtual Terrace Terrace { get; set; }

		/// <summary>
		/// Высота этажа.
		/// </summary>
		public virtual FloorLevel FloorLevel { get; set; }
	}
}