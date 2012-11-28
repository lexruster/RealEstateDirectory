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
            Property(x => x.Description, m => m.Length(4096));

            ManyToOne(x => x.District);
            Property(x => x.HasVideo);
            Property(x => x.Price);

            ManyToOne(x => x.Realtor);
            ManyToOne(x => x.Street);
            Property(x => x.SubmitToDomino);
            Property(x => x.SubmitToVDV);
            Property(x => x.TerritorialNumber);
            Property(x => x.CreateDate);
            ManyToOne(x => x.Ownership);
        }
    }
}