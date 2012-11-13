using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Domain.Entities
{
    /// <summary>
    /// ����� ��������� ����������� ���� (��������, �������)
    /// </summary>
    public class Building : RealEstate
	{
        /// <summary>
        /// ����
        /// </summary>
        public virtual int? Floor { get; set; }

        /// <summary>
        /// ����� ������
        /// </summary>
        public virtual int? TotalFloor { get; set; }

        /// <summary>
        /// ����� ������� (����� ��� ������)
        /// </summary>
        public virtual decimal? TotalSquare { get; set; }

        /// <summary>
        /// �������� ��������
        /// </summary>
        public virtual Material Material { get; set; }
	}
}
