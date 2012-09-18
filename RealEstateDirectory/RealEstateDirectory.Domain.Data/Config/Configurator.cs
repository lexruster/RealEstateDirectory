using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Mapping.ByCode;
using RealEstateDirectory.Domain.Data.Mapping;
using RealEstateDirectory.Domain.Data.Mapping.Dictionaries;

namespace RealEstateDirectory.Domain.Data.Config
{
    public static class Configurator
    {
        public static Configuration GetConfig()
        {
            var cfg = new Configuration();

            cfg.Configure()
                .CurrentSessionContext<CallSessionContext>()
                .Cache(c =>
                {
                    c.UseQueryCache = true;
                    c.UseMinimalPuts = true;
                });

            InitProperties(cfg);

            InitMapping(cfg);

            return cfg;
        }

        public static void InitProperties(Configuration cfg)
        {
            cfg.Properties.Add("hbm2ddl.keywords", "auto-quote");
            cfg.SetNamingStrategy(new PostgresNamingStrategy());
        }

        public static void InitMapping(Configuration cfg)
        {
            var mapper = new ModelMapper();
            mapper.AddMapping<BaseDictionaryMap>();
            mapper.AddMapping<DealVariantMap>();
            mapper.AddMapping<DistrictMap>();
            mapper.AddMapping<OwnershipMap>();
            mapper.AddMapping<SewageMap>();
            mapper.AddMapping<WaterSupplyMap>();
            mapper.AddMapping<LayoutMap>();
            mapper.AddMapping<MaterialMap>();
            mapper.AddMapping<RealtorMap>();
            mapper.AddMapping<StreetMap>();
            mapper.AddMapping<TerraceMap>();
            mapper.AddMapping<ToiletTypeMap>();
            mapper.AddMapping<FloorLevelMap>();

            mapper.AddMapping<RealEstateMap>();
            mapper.AddMapping<PlotMap>();
            mapper.AddMapping<BuildingMap>();
            mapper.AddMapping<ResidenceMap>();
            mapper.AddMapping<ApartmentMap>();
            mapper.AddMapping<FlatMap>();
            mapper.AddMapping<HouseMap>();
            mapper.AddMapping<RoomMap>();

            cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
        }
    }
}
