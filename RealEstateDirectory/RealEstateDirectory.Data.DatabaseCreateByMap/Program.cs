using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using RealEstateDirectory.Data.Mapping;

namespace RealEstateDirectory.Data.DatabaseCreateByMap
{
	class Program
	{
		static void Main(string[] args)
		{
			var config = new Configuration();
			config.Configure("MSSQL.cfg.xml");
			var mapper = new ModelMapper();
			mapper.AddMapping<SewageMap>();
			mapper.AddMapping<StateMap>();
			mapper.AddMapping<StreetMap>();
			mapper.AddMapping<AreaMap>();
			mapper.AddMapping<LayoutMap>();
			mapper.AddMapping<RealEstateMap>();
			mapper.AddMapping<ResidentialMap>();
			mapper.AddMapping<ApartmentMap>();
			mapper.AddMapping<HouseMap>();

			config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

			var sc = new SchemaExport(config);
			sc.Create(true, true);

			Console.ReadKey();
		}
	}
}
