using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Domain.Data.Mapping.Dictionaries
{
    public class RealtorAgencyMap : ClassMapping<RealtorAgency>
    {
		public RealtorAgencyMap()
        {
            Id(x => x.Id);
            Property(x => x.Name, m =>
            {
                m.NotNullable(true);
				m.Length(2000);
                m.Unique(true);
            });
            Property(x=>x.Address, m =>
            {
                m.Length(2000);
            });

			Property(x => x.Contacts, m =>
			{
				m.Length(2000);
			});

			Property(x => x.Description, m =>
			{
				m.Length(4000);
			});

			Property(x => x.Director, m =>
			{
				m.Length(1000);
			});
        }
    }
}