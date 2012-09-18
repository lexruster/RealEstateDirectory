using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Data.Mapping.Dictionaries
{
    public class OwnershipMap : UnionSubclassMapping<Ownership>
	{
	}
}
