using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Data.Mapping.Dictionaries
{
    public class OwnershipMap : ClassMapping<Ownership>
    {
        public OwnershipMap()
        {
            Id(x => x.Id);
            Property(x => x.Name, m =>
                                      {
                                          m.NotNullable(true);
                                          m.Length(2048);
                                          m.Unique(true);
                                      });
        }
    }
}