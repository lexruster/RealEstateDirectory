using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    /// <summary>
    /// ��� �������������
    /// </summary>
    public class Ownership : BaseDictionary
    {
        protected Ownership()
        {
        }

        public Ownership(string name)
            : base(name)
        {
        }
    }
}