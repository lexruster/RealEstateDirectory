using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Domain.Entities
{
	/// <summary>
	/// ���������
	/// </summary>
	public class Residence : Building
	{
		/// <summary>
		/// ���������� ��������
		/// </summary>
		public virtual Destination Destination { get; set; }
	}
}