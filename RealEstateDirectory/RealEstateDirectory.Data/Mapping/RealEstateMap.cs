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
	public class RealEstateMap : ClassMapping<RealEstate>
	{
		public RealEstateMap()
		{
			Id(x => x.Id, m => m.Generator(Generators.Identity));
			Property(x => x.IsSale);
			Property(x => x.Price);
			ManyToOne(x => x.Area, m => m.Column("AreaId"));
			ManyToOne(x => x.Street, m => m.Column("StreetId"));
			Property(x => x.Additional);
		}
	}
}
