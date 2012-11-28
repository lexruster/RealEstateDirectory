using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.Domain.Data.Mapping
{
	public class ApartmentMap : JoinedSubclassMapping<Apartment>
	{
        public ApartmentMap()
		{
			Key(k =>
				{
                    k.Column("BuildingId");
					k.OnDelete(OnDeleteAction.Cascade);
					k.ForeignKey("FK_ApartmentId_BuildingId");
				});
			Property(x => x.TotalRoomCount);
            ManyToOne(x => x.Layout);
            ManyToOne(x => x.Terrace);
            ManyToOne(x => x.FloorLevel);
		}
	}
}
