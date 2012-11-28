using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    /// <summary>
    /// ������
    /// </summary>
    public class Terrace : BaseDictionary
    {
        protected Terrace()
        {
        }

        public Terrace(string name)
            : base(name)
        {
        }
    }
}