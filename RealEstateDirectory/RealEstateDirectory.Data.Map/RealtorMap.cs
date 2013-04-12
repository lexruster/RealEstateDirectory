using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Data.Entities;

namespace RealEstateDirectory.Data.Map
{
	public class RealtorMap : ClassMapping<Realtor>
	{
		public RealtorMap()
		{
			Id(x => x.Id);
			Property(x => x.Name, m =>
				{
					m.NotNullable(true);
					m.Length(2000);
					m.Unique(true);
				});
			Property(x => x.Phone, m => m.NotNullable(false));
		}
	}
}
