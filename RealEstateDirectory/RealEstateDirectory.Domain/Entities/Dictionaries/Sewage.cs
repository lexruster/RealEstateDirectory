using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    /// <summary>
    /// �����������
    /// </summary>
    public class Sewage : BaseDictionary
    {
        protected Sewage()
        {
        }

        public Sewage(string name)
            : base(name)
        {
        }
    }
}