using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Domain.Data.Mapping.Dictionaries
{
    public class RealtorMap : UnionSubclassMapping<Realtor>
    {
        public RealtorMap()
        {
            Property(x=>x.Phone);
        }
    }
}