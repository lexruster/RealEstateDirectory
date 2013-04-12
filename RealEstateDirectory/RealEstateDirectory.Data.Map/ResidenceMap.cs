using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Data.Entities;

namespace RealEstateDirectory.Data.Map
{
	public class ResidenceMap : JoinedSubclassMapping<Residence>
	{
		public ResidenceMap()
		{
			Key(k =>
				{
					k.Column("BuildingId");
					k.OnDelete(OnDeleteAction.Cascade);
					k.ForeignKey("\"FK_Residence_Building\"");
				});
			Property(x => x.Floor);
			Property(x => x.TotalFloor);
			Property(x => x.TotalSquare, m =>
				{
					m.Precision(19);
					m.Scale(5);
				});
			ManyToOne(x => x.Material);
			ManyToOne(x => x.Destination);
		}
	}
}
