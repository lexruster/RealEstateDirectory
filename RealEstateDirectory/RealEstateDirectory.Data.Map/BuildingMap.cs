using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Data.Entities;

namespace RealEstateDirectory.Data.Map
{
	public class BuildingMap : JoinedSubclassMapping<Building>
	{
		public BuildingMap()
		{
			Key(k =>
				{
					k.Column("RealEstateId");
					k.OnDelete(OnDeleteAction.Cascade);
					k.ForeignKey("\"FK_Building_RealEstate\"");
				});
			Property(x => x.Floor);
			Property(x => x.TotalFloor);
			Property(x => x.TotalSquare, m =>
				{
					m.Precision(19);
					m.Scale(5);
				});
			ManyToOne(x => x.Material);
		}
	}
}
