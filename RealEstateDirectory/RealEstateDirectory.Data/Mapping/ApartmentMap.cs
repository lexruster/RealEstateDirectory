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
	public class ApartmentMap : JoinedSubclassMapping<Apartment>
	{
		public ApartmentMap()
		{
			Key(k =>
				{
					k.Column("ResidentialId");
					k.OnDelete(OnDeleteAction.Cascade);
				});
			Property(x => x.Floor);
			Property(x => x.TotalFloors);
			Property(x => x.Layout, m => m.Column("LayoutId"));
			Property(x => x.IsSeparateBathroom);
		}
	}
}
