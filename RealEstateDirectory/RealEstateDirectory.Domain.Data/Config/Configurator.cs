using System;
using System.Configuration;
using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Impl;
using RealEstateDirectory.Domain.Data.Mapping;
using RealEstateDirectory.Domain.Data.Mapping.Dictionaries;
using RealEstateDirectory.Infrastructure.Entities;
using Configuration = NHibernate.Cfg.Configuration;

namespace RealEstateDirectory.Domain.Data.Config
{
	public static class Configurator
	{
		public static Configuration GetConfig()
		{
			var cfg = new Configuration();

			cfg.Configure()
				.CurrentSessionContext<ThreadStaticSessionContext>()
				.Cache(c =>
					{
						//c.UseQueryCache = true;
						//c.UseMinimalPuts = true;
					});

			Utils.Config.Load();
			cfg.DataBaseIntegration(properties => properties.ConnectionString = Utils.Config.GetProperty("DefaultConnectionString"));

			InitProperties(cfg);

			InitMapping(cfg);

			return cfg;
		}

		public static void InitProperties(Configuration cfg)
		{
			cfg.Properties.Add("hbm2ddl.keywords", "auto-quote");
		}

		public static void InitMapping(Configuration cfg)
		{
			var mapper = new ModelMapper();
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
			mapper.AddMapping<RealtorAgencyMap>();

			mapper.AddMapping<RealEstateMap>();
			mapper.AddMapping<PlotMap>();
			mapper.AddMapping<BuildingMap>();
			mapper.AddMapping<ResidenceMap>();
			mapper.AddMapping<ApartmentMap>();
			mapper.AddMapping<FlatMap>();
			mapper.AddMapping<HouseMap>();
			mapper.AddMapping<RoomMap>();

			mapper.BeforeMapClass += AutoMapper_BeforeMapClass;
			mapper.BeforeMapManyToOne += AutoMapper_BeforeMapManyToOne;
			mapper.BeforeMapBag += BeforeBag;

			cfg.SetNamingStrategy(new PostgresNamingStrategy());

			cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
		}

		private static void BeforeBag(IModelInspector modelinspector, PropertyPath member,
		                              IBagPropertiesMapper propertycustomizer)
		{
			propertycustomizer.Key(x => x.Column(member.GetContainerEntity(modelinspector).Name + "Id"));
			propertycustomizer.Cascade(Cascade.All);
			propertycustomizer.Inverse(false);
			//propertycustomizer.Lazy(CollectionLazy.Lazy);
			propertycustomizer.Lazy(CollectionLazy.NoLazy);
		}

		public static void AutoMapper_BeforeMapClass(IModelInspector modelInspector, Type type,
		                                             IClassAttributesMapper classCustomizer)
		{
			classCustomizer.Id(k =>
				{
					k.Generator(Generators.Identity);
					k.Column("Id");
					k.UnsavedValue("0");
				});
		}

		public static void AutoMapper_BeforeMapManyToOne(IModelInspector modelInspector, PropertyPath member,
		                                                 IManyToOneMapper propertyCustomizer)
		{
			var pi = member.LocalMember as PropertyInfo;
			if (null != pi)
			{
				propertyCustomizer.Column(k => k.Name(pi.PropertyType.Name + "Id"));
				propertyCustomizer.ForeignKey(GenerateForeignKeyName(pi.PropertyType.Name,pi.DeclaringType.Name, pi.ReflectedType.Name));
			}
		}
		public static string GenerateForeignKeyName(string typeName, string propertyName, string referenceTypeName)
		{
			return propertyName == referenceTypeName
					   ? String.Format("FK_{0}_{1}", typeName, referenceTypeName)
					   : String.Format("FK_{0}_{1}_{2}", typeName, GenerateForeignKeyColumnName(propertyName),
									   referenceTypeName);
		}

		public static string GenerateForeignKeyColumnName(string propertyName)
		{
			return String.Format("{0}{1}", propertyName, "Id"); 
		}
	}
}