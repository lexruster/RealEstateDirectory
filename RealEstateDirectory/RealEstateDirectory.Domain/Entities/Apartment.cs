using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Domain.Entities
{
    /// <summary>
    /// ����� ��������� ����������� ���� (��������, �������)
    /// </summary>
	public class Apartment : Building
	{
        /// <summary>
        /// ����� ����� ������
        /// </summary>
        public virtual int? TotalRoomCount { get; set; }

        /// <summary>
        /// ���������� ���������
        /// </summary>
        public virtual Layout Layout { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public virtual Terrace Terrace { get; set; }

        /// <summary>
        /// ������ �����
        /// </summary>
        public virtual FloorLevel FloorLevel { get; set; }
	}
}
