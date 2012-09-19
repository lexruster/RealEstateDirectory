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

        /// <summary>
        /// ��������� �����
        /// </summary>
        public virtual WaterSupply WaterSupply { get; set; }

        /// <summary>
        /// �����������
        /// </summary>
        public virtual Sewage Sewage { get; set; }

        /// <summary>
        /// ��������� �����
        /// </summary>
        public virtual bool? HasBathhouse { get; set; }

        /// <summary>
        /// ��������� �����
        /// </summary>
        public virtual bool? HasGarage { get; set; }

        /// <summary>
        /// ��������� �����
        /// </summary>
        public virtual bool? HasGas{ get; set; }

        /// <summary>
        /// �������� ����
        /// </summary>
        public virtual Material Material { get; set; }
	}
}
