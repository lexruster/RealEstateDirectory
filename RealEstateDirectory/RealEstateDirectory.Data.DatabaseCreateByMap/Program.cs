using System;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using RealEstateDirectory.Domain.Data.Config;

namespace RealEstateDirectory.Data.DatabaseCreateByMap
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = new Configuration();
            config.Configure("PostgreSQL.cfg.xml");

            Configurator.InitProperties(config);
            Configurator.InitMapping(config);


            var sc = new SchemaExport(config);
            sc.SetOutputFile("d.txt");
            sc.Create(true, true);
            //sc.Drop(true, true);

            Console.ReadKey();
        }
    }
}