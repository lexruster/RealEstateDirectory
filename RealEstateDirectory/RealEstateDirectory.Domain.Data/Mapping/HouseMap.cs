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

			Property(x => x.TotalFloor);
			Property(x => x.HouseSquare);
            ManyToOne(x=>x.WaterSupply);
            ManyToOne(x => x.Sewage);
            Property(x => x.HasBathhouse);
            Property(x => x.HasGarage);
            Property(x => x.HasGas);
		}
	}
}
