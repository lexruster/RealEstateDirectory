using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.Domain.Data.Mapping
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
