using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Data.Entities;

namespace RealEstateDirectory.Data.Map
{
	public class FloorLevelMap : ClassMapping<FloorLevel>
	{
		public FloorLevelMap()
		{
			Id(x => x.Id);
			Property(x => x.Name, m =>
				{
					m.NotNullable(true);
					m.Length(2000);
					m.Unique(true);
				});
		}
	}
}
