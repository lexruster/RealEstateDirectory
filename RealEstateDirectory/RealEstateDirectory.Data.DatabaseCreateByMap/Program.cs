using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using RealEstateDirectory.Domain.Data.Mapping;
using RealEstateDirectory.Infrastructure.NHibernate.Config;

namespace RealEstateDirectory.Data.DatabaseCreateByMap
{
	class Program
	{
		static void Main(string[] args)
		{
			var config = new Configuration();
			//config.Configure("MSSQL.cfg.xml");
			config.Configure("PostgreSQL.cfg.xml");
            config.Properties.Add("hbm2ddl.keywords", "auto-quote");
            config.SetNamingStrategy(new PostgresNamingStrategy());

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
			sc.SetOutputFile("d.txt");
			sc.Create(true, true);
            //sc.Drop(true,true);
			
			Console.ReadKey();
		}
	}
}
