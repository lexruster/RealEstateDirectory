using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    /// <summary>
    /// ������� ������
    /// </summary>
    public class DealVariant : BaseDictionary
    {
        protected DealVariant()
        {
        }

        public DealVariant(string name)
            : base(name)
        {
        }
    }
}