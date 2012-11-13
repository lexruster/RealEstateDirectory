using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Domain.Entities
{
    /// <summary>
    /// ��������
    /// </summary>
	public class Flat : Apartment
	{
        /// <summary>
        /// ���������� ����
        /// </summary>
        public virtual ToiletType ToiletType { get; set; }

        /// <summary>
        /// ����� �������
        /// </summary>
        public virtual decimal? ResidentialSquare { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        public virtual decimal? KitchenSquare { get; set; }
	}
}
