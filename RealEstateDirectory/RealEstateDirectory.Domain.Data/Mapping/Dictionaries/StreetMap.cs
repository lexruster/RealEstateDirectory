using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Domain.Data.Mapping.Dictionaries
{
    public class StreetMap : ClassMapping<Street>
    {
        public StreetMap()
        {
            Id(x => x.Id);
            Property(x => x.Name, m =>
            {
                m.NotNullable(true);
                m.Length(2000);
            });
            ManyToOne(x => x.District, r =>
                                           {
                                               r.NotNullable(true);
                                               r.Class(typeof(District));
                                           });
        }
    }
}