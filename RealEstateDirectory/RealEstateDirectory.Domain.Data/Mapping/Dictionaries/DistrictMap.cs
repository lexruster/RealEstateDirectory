using System.Collections.Generic;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Domain.Data.Mapping.Dictionaries
{
    public class DistrictMap : UnionSubclassMapping<District>
    {
        public DistrictMap()
        {
            Set(x => x.Streets, c =>
                                    {
                                        c.Cascade(Cascade.All);
                                        c.Inverse(true);
                                        c.Fetch(CollectionFetchMode.Join);
                                        c.BatchSize(100);
                                        c.Lazy(CollectionLazy.Lazy);
                                    }, r =>
                                           {
                                               r.OneToMany(
                                                   m =>
                                                   {
                                                       m.Class(typeof(Street));
                                                   });
                                           });


        }
    }
}