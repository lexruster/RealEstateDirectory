using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.Domain.Data.Mapping
{
	public class ResidenceMap : JoinedSubclassMapping<Residence>
	{
		public ResidenceMap()
		{
			Key(k =>
				{
					k.Column("BuildingId");
					k.OnDelete(OnDeleteAction.Cascade);
					k.ForeignKey("FK_ResidenceId_BuildingId");
				});
			Property(x => x.Floor);
			Property(x => x.TotalFloor);
			Property(x => x.TotalSquare);
			ManyToOne(x => x.Material);
		}
	}
}