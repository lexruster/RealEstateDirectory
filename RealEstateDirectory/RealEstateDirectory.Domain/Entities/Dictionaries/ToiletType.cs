using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities.Dictionaries
{
    /// <summary>
    /// Сан. узел
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