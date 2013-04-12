﻿using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Data.Entities;

namespace RealEstateDirectory.Data.Map
{
	public class RoomMap : JoinedSubclassMapping<Room>
	{
		public RoomMap()
		{
			Key(k =>
				{
					k.Column("ApartmentId");
					k.OnDelete(OnDeleteAction.Cascade);
					k.ForeignKey("\"FK_Room_Apartment\"");
				});
			Property(x => x.RoomCount);
		}
	}
}
