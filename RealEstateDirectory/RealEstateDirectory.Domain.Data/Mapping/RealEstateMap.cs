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

			ManyToOne(x => x.DealVariant, mapper => mapper.Column("DealVariantId"));
			ManyToOne(x => x.District, mapper => mapper.Column("DistrictId"));
			ManyToOne(x => x.Realtor, mapper => mapper.Column("RealtorId"));
			ManyToOne(x => x.Street, mapper => mapper.Column("StreetId"));
			ManyToOne(x => x.Ownership, mapper => mapper.Column("OwnershipId"));

			Property(x => x.Description, m => m.Length(4096));
			Property(x => x.HasVideo);
			Property(x => x.Price);
			Property(x => x.SubmitToDomino);
			Property(x => x.SubmitToVDV);
			Property(x => x.TerritorialNumber);
			Property(x => x.CreateDate);
		}
	}
}
