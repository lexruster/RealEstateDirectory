namespace RealEstateDirectory.Infrastructure.Entities
{
    /// <summary>
    /// Типовой справочник
    /// </summary>
    public abstract class BaseDictionary : Entity<int>
    {
        #region Свойства

        /// <summary>
        /// Наименование элемента
        /// </summary>
        public virtual string Name { get; set; }

        #endregion

        #region Конструкторы

        protected BaseDictionary()
        {
        }

        public BaseDictionary(string name)
        {
            Name = name;
        }

        #endregion
    }
}
