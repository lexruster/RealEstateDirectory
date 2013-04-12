namespace RealEstateDirectory.Data.Entities
{
	/// <summary>
	/// Улица.
	/// </summary>
	public class Street : BaseDictionary
	{
		/// <summary>
		/// Район
		/// </summary>
		public virtual District District { get; set; }
	}
}