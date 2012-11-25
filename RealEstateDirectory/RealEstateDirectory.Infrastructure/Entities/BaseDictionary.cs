namespace RealEstateDirectory.Infrastructure.Entities
{
    /// <summary>
    /// ������� ����������
    /// </summary>
    public abstract class BaseDictionary : Entity<int>
    {
        #region ��������

        /// <summary>
        /// ������������ ��������
        /// </summary>
        public virtual string Name { get; set; }

        #endregion

        #region ������������

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
