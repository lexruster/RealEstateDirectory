namespace RealEstateDirectory.Data.Entities
{
	/// <summary>
	/// Типовой справочник.
	/// </summary>
	public class BaseDictionary : BaseEntity
	{
		/// <summary>
		/// Наименование элемента.
		/// </summary>
		public virtual string Name { get; set; }
	}
}
