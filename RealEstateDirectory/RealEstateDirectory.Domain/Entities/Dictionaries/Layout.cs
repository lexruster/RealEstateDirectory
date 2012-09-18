using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    /// <summary>
    /// Планировка помещения
    /// </summary>
	public class Layout : BaseDictionary
	{
		protected Layout()
		{
		}

		public Layout(string name) : base(name)
		{
		}
	}
}