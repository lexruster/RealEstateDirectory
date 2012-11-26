using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    /// <summary>
    /// ���. ����
    /// </summary>
    public class ToiletType : BaseDictionary
    {
        protected ToiletType()
        {
        }

        public ToiletType(string name)
            : base(name)
        {
        }
    }
}