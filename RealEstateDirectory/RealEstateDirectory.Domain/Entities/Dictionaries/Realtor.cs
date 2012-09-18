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

        protected Realtor(string name)
        {
        }

        protected Realtor(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }

        #endregion
	}
}