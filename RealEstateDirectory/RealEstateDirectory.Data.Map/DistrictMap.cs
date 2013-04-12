using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Data.Entities;

namespace RealEstateDirectory.Data.Map
{
	public class DistrictMap : ClassMapping<District>
	{
		public DistrictMap()
		{
			Id(x => x.Id);
			Property(x => x.Name, m =>
				{
					m.NotNullable(true);
					m.Length(2000);
					m.Unique(true);
				});

			Bag(x => x.Streets, c =>
				{
					c.Cascade(Cascade.All);
					c.Inverse(true);
				},
				r => r.OneToMany(m => m.Class(typeof (Street))));
		}
	}
}
