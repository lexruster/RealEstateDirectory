using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
	/// <summary>
	/// ����������/��� ���������
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