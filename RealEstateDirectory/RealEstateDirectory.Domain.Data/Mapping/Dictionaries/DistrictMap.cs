using System.Collections.Generic;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Domain.Data.Mapping.Dictionaries
{
    public class DistrictMap : ClassMapping<District>
    {
        public DistrictMap()
        {
            Id(x => x.Id);
            Property(x => x.Name, m =>
            {
                m.NotNullable(true);
                m.Length(2048);
                m.Unique(true);
            });

            Bag(x => x.Streets, c =>
                                    {
                                    },
                r =>
                    {
                        r.OneToMany(m =>
                                        {
                                            m.Class(typeof (Street));
                                        }
                            );

                    }
                );
        }
    }
}