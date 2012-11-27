using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Domain.Data.Mapping.Dictionaries
{
    public class FloorLevelMap : ClassMapping<FloorLevel>
    {
        public FloorLevelMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Name, m =>
                                      {
                                          m.NotNullable(true);
                                          m.Length(2048);
                                          m.Unique(true);
                                      });
        }
    }
}