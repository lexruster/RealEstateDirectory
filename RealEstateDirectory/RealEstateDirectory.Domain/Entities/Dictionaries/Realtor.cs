using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    public class Realtor : BaseDictionary
	{
        /// <summary>
        /// Телефон
        /// </summary>
        public virtual string Phone { get; set; }

         #region Конструкторы

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