using System.Collections.Generic;
using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
	/// <summary>
	/// �����, ������
	/// </summary>
	public class District : BaseDictionary
	{
		/// <summary>
		/// ����� ������
		/// </summary>
		public virtual IList<Street> Streets { get; set; }

#warning �� �������� ������
		protected District()
		{
		}

		public District(string name)
			: base(name)
		{
			Streets = new List<Street>();
		}

		public virtual void AddStreet(Street street)
		{
			street.District = this;
			Streets.Add(street);
		}
	}
}