using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
	/// <summary>
	/// Назначение/Вид помещения
	/// </summary>
	public class Destination : BaseDictionary
	{
		protected Destination()
		{
		}

		public Destination(string name)
			: base(name)
		{
		}
	}
}