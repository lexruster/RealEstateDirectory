using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Data.Entities;

namespace RealEstateDirectory.Data.Mapping
{
	public class HouseMap : JoinedSubclassMapping<House>
	{
		public HouseMap()
		{
			Key(k =>
				{
					k.Column("ResidentialId");
					k.OnDelete(OnDeleteAction.Cascade);
				});
			Property(x => x.Floors);
			Property(x => x.Square);
			Property(x => x.WithGarage);
			Property(x => x.ExtBuilt);
			Property(x => x.IsElectricityPresent);
			Property(x => x.IsGasPresent);
			ManyToOne(x => x.Sewage, m => m.Column("SewageId"));
		}
	}
}
