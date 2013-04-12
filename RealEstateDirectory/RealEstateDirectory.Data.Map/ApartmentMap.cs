using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Data.Entities;

namespace RealEstateDirectory.Data.Map
{
	public class ApartmentMap : JoinedSubclassMapping<Apartment>
	{
		public ApartmentMap()
		{
			Key(k =>
				{
					k.Column("BuildingId");
					k.OnDelete(OnDeleteAction.Cascade);
					k.ForeignKey("\"FK_Apartment_Building\"");
				});
			Property(x => x.TotalRoomCount);
			ManyToOne(x => x.Layout);
			ManyToOne(x => x.Terrace);
			ManyToOne(x => x.FloorLevel);
		}
	}
}
