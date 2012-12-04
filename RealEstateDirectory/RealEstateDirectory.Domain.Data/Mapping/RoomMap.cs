using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.Domain.Data.Mapping
{
	public class RoomMap : JoinedSubclassMapping<Room>
	{
        public RoomMap()
		{
			Key(k =>
				{
                    k.Column("ApartmentId");
					k.OnDelete(OnDeleteAction.Cascade);
					k.ForeignKey("FK_RoomId_ApartmentId");
				});
			Property(x => x.RoomCount);
		}
	}
}
