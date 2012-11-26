using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    public class RealtorAgency : BaseDictionary
	{
        /// <summary>
        /// Контакты
        /// </summary>
        public virtual string Contacts { get; set; }

		/// <summary>
		/// Директор
		/// </summary>
		public virtual string Director { get; set; }

		/// <summary>
		/// Адрес
		/// </summary>
		public virtual string Address { get; set; }

		/// <summary>
		/// Комментарий
		/// </summary>
		public virtual string Description { get; set; }

         #region Конструкторы

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