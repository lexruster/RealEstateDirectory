using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Domain.Entities
{
    /// <summary>
    /// ���
    /// </summary>
    public class House : Plot
	{
        /// <summary>
        /// ������ � ����
        /// </summary>
        public virtual int? TotalFloor { get; set; }

        /// <summary>
        /// ������� ����
        /// </summary>
        public virtual decimal? HouseSquare { get; set; }
	}
}
