using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Domain.Entities
{
	/// <summary>
	/// Помещения
	/// </summary>
	public class Residence : Building
	{
		/// <summary>
		/// Назначение строения
		/// </summary>
		public virtual Destination Destination { get; set; }
	}
}