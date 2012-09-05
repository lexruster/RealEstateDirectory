using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.Domain.Data.Mapping
{
	public class StateMap : ClassMapping<State>
	{
		public StateMap()
		{
			Id(x => x.Id, m => m.Generator(Generators.Identity));
			Property(x => x.Name);
		}
	}
}
