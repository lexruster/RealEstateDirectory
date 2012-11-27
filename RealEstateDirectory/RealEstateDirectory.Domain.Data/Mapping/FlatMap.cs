using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.Domain.Data.Mapping
{
	public class FlatMap : JoinedSubclassMapping<Flat>
	{
		public FlatMap()
		{
			Key(k =>
				{
					k.Column("ApartmentId");
					k.OnDelete(OnDeleteAction.Cascade);
				});

			Property(x => x.ResidentialSquare);
			Property(x => x.KitchenSquare);

			ManyToOne(x => x.ToiletType, mapper => mapper.Column("ToiletTypeId"));
		}
	}
}
