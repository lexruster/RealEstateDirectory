using System;

namespace RealEstateDirectory.Data.Entities
{
	/// <summary>
	/// Базовый класс для недвижимости.
	/// </summary>
	public class RealEstate : BaseEntity
	{
		/// <summary>
		/// Район.
		/// </summary>
		public virtual District District { get; set; }

		/// <summary>
		/// Улица.
		/// </summary>
		public virtual Street Street { get; set; }

		/// <summary>
		/// Номер дома/участка.
		/// </summary>
		public virtual string TerritorialNumber { get; set; }

		/// <summary>
		/// Комментарий.
		/// </summary>
		public virtual string Description { get; set; }

		/// <summary>
		/// Есть ли видео к объекту.
		/// </summary>
		public virtual bool HasVideo { get; set; }

		/// <summary>
		/// Цена (т.р.).
		/// </summary>
		public virtual decimal? Price { get; set; }

		/// <summary>
		/// Риэлтер.
		/// </summary>
		public virtual Realtor Realtor { get; set; }

		/// <summary>
		/// Вариант сделки.
		/// </summary>
		public virtual DealVariant DealVariant { get; set; }

		/// <summary>
		/// Дата создания.
		/// </summary>
		public virtual DateTime CreateDate { get; set; }

		/// <summary>
		/// Вид собственности.
		/// </summary>
		public virtual Ownership Ownership { get; set; }

		/// <summary>
		/// Состояние.
		/// </summary>
		public virtual Condition Condition { get; set; }
	}
}