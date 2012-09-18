using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Domain.Data.Mapping.Dictionaries
{
    public class StreetMap : UnionSubclassMapping<Street>
    {
        public StreetMap()
        {
            ManyToOne(x =>  x.District,r=>
                                           {
                                               r.NotNullable(true);
                                           } );
        }
    }
}