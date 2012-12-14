using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
	/// <summary>
	/// Состояние
	/// </summary>
	public class Condition : BaseDictionary
	{
		protected Condition()
		{
		}

		public Condition(string name)
			: base(name)
		{
		}
	}
}