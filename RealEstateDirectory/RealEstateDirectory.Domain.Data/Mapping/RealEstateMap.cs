using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.Domain.Data.Mapping
{
	public class RealEstateMap : ClassMapping<RealEstate>
	{
		public RealEstateMap()
		{
			Id(x => x.Id);
			ManyToOne(x => x.DealVariant);
			Property(x => x.Description, m => m.Length(4000));

			ManyToOne(x => x.District);
			Property(x => x.HasVideo, m => m.NotNullable(true));
			Property(x => x.Price, m =>
				{
					m.Precision(19);
					m.Scale(5);
				});

			ManyToOne(x => x.Realtor);
			ManyToOne(x => x.Street);
			Property(x => x.SubmitToDomino, m => m.NotNullable(true));
			Property(x => x.SubmitToVDV, m => m.NotNullable(true));
			Property(x => x.TerritorialNumber, m => m.Length(400));
			Property(x => x.CreateDate, m => m.NotNullable(true));
			ManyToOne(x => x.Ownership);
		}
	}
}