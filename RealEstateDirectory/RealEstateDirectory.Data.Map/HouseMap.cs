using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Data.Entities;

namespace RealEstateDirectory.Data.Map
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
			ManyToOne(x => x.WaterSupply);
			ManyToOne(x => x.Sewage);
			Property(x => x.HasBathhouse, m => m.NotNullable(true));
			Property(x => x.HasGarage, m => m.NotNullable(true));
			Property(x => x.HasGas, m => m.NotNullable(true));
			ManyToOne(x => x.Material);
		}
	}
}
