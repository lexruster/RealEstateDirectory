using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.Domain.Data.Mapping
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
			ManyToOne(x => x.Layout, m => m.Column("LayoutId"));
			Property(x => x.IsSeparateBathroom);
		}
	}
}
