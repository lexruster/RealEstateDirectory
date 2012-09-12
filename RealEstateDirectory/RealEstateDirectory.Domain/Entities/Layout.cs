using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities
{
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