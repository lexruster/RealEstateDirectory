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
				});

			ManyToOne(x => x.WaterSupply, mapper => mapper.Column("WaterSupplyId"));
			ManyToOne(x => x.Sewage, mapper => mapper.Column("SewageId"));
			ManyToOne(x => x.Material, mapper => mapper.Column("MaterialId"));

			Property(x => x.TotalFloor);
			Property(x => x.HouseSquare);
			Property(x => x.HasBathhouse);
			Property(x => x.HasGarage);
			Property(x => x.HasGas);
		}
	}
}
