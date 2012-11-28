using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.Domain.Data.Mapping
{
	public class BuildingMap : JoinedSubclassMapping<Building>
	{
        public BuildingMap()
		{
			Key(k =>
				{
					k.Column("RealEstateId");
					k.OnDelete(OnDeleteAction.Cascade);
				});
			Property(x => x.Floor);
			Property(x => x.TotalFloor);
			Property(x => x.TotalSquare);
			ManyToOne(x => x.Material);
		}
	}
}
