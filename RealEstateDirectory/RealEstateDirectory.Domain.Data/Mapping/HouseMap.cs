using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.Domain.Data.Mapping
{
	public class HouseMap : JoinedSubclassMapping<House>
	{
		public HouseMap()
		{
			Key(k =>
				{
					k.Column("PlotId");
					k.OnDelete(OnDeleteAction.Cascade);
					k.ForeignKey("\"FK_House_Plot\"");
				});

			Property(x => x.TotalFloor);
			Property(x => x.HouseSquare, m =>
				{
					m.Precision(19);
					m.Scale(5);
				});
		}
	}
}