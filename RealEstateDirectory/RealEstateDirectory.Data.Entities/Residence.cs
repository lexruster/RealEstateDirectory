namespace RealEstateDirectory.Data.Entities
{
	/// <summary>
	/// Помещения.
	/// </summary>
	public class Residence : Building
	{
		/// <summary>
		/// Назначение.
		/// </summary>
		public virtual Destination Destination { get; set; }
	}
}