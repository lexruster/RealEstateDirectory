using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Domain.Data.Mapping.Dictionaries
{
    public class StreetMap : ClassMapping<Street>
    {
        public StreetMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.HighLow));
            Property(x => x.Name, m =>
            {
                m.NotNullable(true);
                m.Length(2048);
                m.Unique(true);
            });
            ManyToOne(x => x.District, r =>
                                           {
                                               r.Cascade(Cascade.All | Cascade.None | Cascade.Persist | Cascade.Remove);
                                               r.Update(true);
                                               r.Insert(true);
                                               r.NotNullable(true);
                                               r.Class(typeof(District));
                                           });
        }
    }
}