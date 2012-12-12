using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    public class RealtorAgency : BaseDictionary
	{
        /// <summary>
        /// ��������
        /// </summary>
        public virtual string Contacts { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public virtual string Director { get; set; }

		/// <summary>
		/// �����
		/// </summary>
		public virtual string Address { get; set; }

		/// <summary>
		/// �����������
		/// </summary>
		public virtual string Description { get; set; }

         #region ������������

        protected RealtorAgency()
        {
        }

		public RealtorAgency(string name)
			: base(name)
		{
		}

        #endregion
	}
}