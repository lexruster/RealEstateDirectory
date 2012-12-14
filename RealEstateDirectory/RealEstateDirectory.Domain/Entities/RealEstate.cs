using System;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities
{
    /// <summary>
    /// Базовый клас для недвижимости
    /// </summary>
    public class RealEstate : Entity<int>
    {
        /// <summary>
        /// Район
        /// </summary>
        public virtual District District { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        public virtual Street Street { get; set; }

        /// <summary>
        /// Номер дома/участка
        /// </summary>
        public virtual string TerritorialNumber { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Есть ли видео к объекту
        /// </summary>
        public virtual bool HasVideo { get; set; }

        /// <summary>
        /// Объявление в ВДВ
        /// </summary>
        public virtual bool SubmitToVDV { get; set; }

        /// <summary>
        /// Объявление в "Домино"
        /// </summary>
        public virtual bool SubmitToDomino { get; set; }

        /// <summary>
        /// Цена (т.р.)
        /// </summary>
        public virtual decimal? Price { get; set; }

        /// <summary>
        /// Риэлтор
        /// </summary>
        public virtual Realtor Realtor { get; set; }

        /// <summary>
        /// Вариант сделки
        /// </summary>
        public virtual DealVariant DealVariant { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// Собственность
        /// </summary>
        public virtual Ownership Ownership { get; set; }

		/// <summary>
		/// Состояние
		/// </summary>
		public virtual Condition Condition { get; set; }

        #region Конструкторы

        public RealEstate()
        {
            CreateDate = DateTime.Now;
        }

        #endregion
    }
}
