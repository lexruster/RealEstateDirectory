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
				});

			ManyToOne(x => x.Layout, mapper => mapper.Column("LayoutId"));
			ManyToOne(x => x.Terrace, mapper => mapper.Column("TerraceId"));
			ManyToOne(x => x.FloorLevel, mapper => mapper.Column("FloorLevelId"));

			Property(x => x.TotalRoomCount);
		}
	}
}
