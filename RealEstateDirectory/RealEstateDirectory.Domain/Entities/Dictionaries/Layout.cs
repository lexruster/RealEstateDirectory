using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    /// <summary>
    /// ���������� ���������
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