using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Domain.Data.Mapping.Dictionaries
{
    public class RealtorMap : ClassMapping<Realtor>
    {
        public RealtorMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.HighLow));
            Property(x => x.Name, m =>
            {
                m.NotNullable(true);
                m.Length(2048);
                m.Unique(true);
            });
            Property(x=>x.Phone, m=>m.NotNullable(false));
        }
    }
}