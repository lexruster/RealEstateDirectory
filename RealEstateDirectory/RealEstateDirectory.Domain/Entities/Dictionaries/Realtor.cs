using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    public class Realtor : BaseDictionary
	{
        /// <summary>
        /// �������
        /// </summary>
        public virtual string Phone { get; set; }

         #region ������������

        protected Realtor()
        {
        }

		public Realtor(string name)
			: base(name)
		{
		}

		public Realtor(string name, string phone)
			: base(name)
        {
            Phone = phone;
        }

        #endregion
	}
}