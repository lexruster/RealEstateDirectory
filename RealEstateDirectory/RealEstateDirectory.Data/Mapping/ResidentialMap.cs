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
	public class ResidentialMap : JoinedSubclassMapping<Residential>
	{
		public ResidentialMap()
		{
			Key(k =>
				{
					k.Column("RealEstateId");
					k.OnDelete(OnDeleteAction.Cascade);
				});
			Property(x => x.LivingSquare);
			Property(x => x.TotalSquare);
			Property(x => x.RoomsCount);
			ManyToOne(x => x.State, m => m.Column("StateId"));
			Property(x => x.Number);
		}
	}
}
