using System;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities
{
    /// <summary>
    /// ������� ���� ��� ������������
    /// </summary>
    public class RealEstate : Entity<int>
    {
        /// <summary>
        /// �����
        /// </summary>
        public virtual District District { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        public virtual Street Street { get; set; }

        /// <summary>
        /// ����� ����/�������
        /// </summary>
        public virtual string TerritorialNumber { get; set; }

        /// <summary>
        /// �����������
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// ���� �� ����� � �������
        /// </summary>
        public virtual bool HasVideo { get; set; }

        /// <summary>
        /// ���������� � ���
        /// </summary>
        public virtual bool SubmitToVDV { get; set; }

        /// <summary>
        /// ���������� � "������"
        /// </summary>
        public virtual bool SubmitToDomino { get; set; }

        /// <summary>
        /// ���� (�.�.)
        /// </summary>
        public virtual decimal? Price { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        public virtual Realtor Realtor { get; set; }

        /// <summary>
        /// ������� ������
        /// </summary>
        public virtual DealVariant DealVariant { get; set; }

        /// <summary>
        /// ���� ��������
        /// </summary>
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// �������������
        /// </summary>
        public virtual Ownership Ownership { get; set; }

		/// <summary>
		/// ���������
		/// </summary>
		public virtual Condition Condition { get; set; }

        #region ������������

        public RealEstate()
        {
            CreateDate = DateTime.Now;
        }

        #endregion
    }
}
