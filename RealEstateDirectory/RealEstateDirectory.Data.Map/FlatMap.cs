using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Data.Entities;

namespace RealEstateDirectory.Data.Map
{
	public class FlatMap : JoinedSubclassMapping<Flat>
	{
		public FlatMap()
		{
			Key(k =>
				{
					k.Column("ApartmentId");
					k.OnDelete(OnDeleteAction.Cascade);
					k.ForeignKey("\"FK_Flat_Apartment\"");
				});
			ManyToOne(x => x.ToiletType);
			Property(x => x.ResidentialSquare, m =>
				{
					m.Precision(19);
					m.Scale(5);
				});
			Property(x => x.KitchenSquare, m =>
				{
					m.Precision(19);
					m.Scale(5);
				});
		}
	}
}
