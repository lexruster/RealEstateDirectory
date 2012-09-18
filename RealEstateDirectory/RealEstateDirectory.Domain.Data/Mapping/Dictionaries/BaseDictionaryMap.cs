using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Data.Mapping.Dictionaries
{
	public class BaseDictionaryMap : ClassMapping<BaseDictionary>
	{
        public BaseDictionaryMap()
		{
			Id(x => x.Id, m => m.Generator(Generators.HighLow));
			Property(x => x.Name, m=>
			                          {
			                              m.NotNullable(true);
                                          m.Length(2048);
                                          m.Unique(true);
			                          });
		}
	}
}
