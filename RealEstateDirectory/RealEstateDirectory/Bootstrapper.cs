using System.Windows;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Mapping.ByCode;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.ApplicationServices;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Data.Mapping;
using RealEstateDirectory.Domain.Data.Repository;
using RealEstateDirectory.Infrastructure.NHibernate.Config;
using RealEstateDirectory.Infrastructure.NHibernate.PersistenceContext;
using RealEstateDirectory.Shell;

namespace RealEstateDirectory
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<ShellView>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            ((ShellView)Shell).DataContext = Container.Resolve<ShellViewModel>();
            Application.Current.MainWindow = (Window)Shell;
        }

        public override void Run(bool runWithDefaultConfiguration)
        {
            base.Run(runWithDefaultConfiguration);
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<Configuration>(new ContainerControlledLifetimeManager(),
                new InjectionFactory(container =>
                                         {
                                             var cfg = new Configuration();

                                             cfg.Configure()
                                                 .CurrentSessionContext<CallSessionContext>()
                                                 .Cache(c =>
                                                 {
                                                     c.UseQueryCache = true;
                                                     c.UseMinimalPuts = true;
                                                 });

                                             cfg.Properties.Add("hbm2ddl.keywords", "auto-quote");
                                             cfg.SetNamingStrategy(new PostgresNamingStrategy());


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

                                             cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());


                                             return cfg;
                                         }));

            Container.RegisterType<IPersistenceContext, PersistenceContext>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ShellViewModel>();
            Container.RegisterType<ILayoutRepository, LayoutRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDataService, DataService>(new ContainerControlledLifetimeManager());
        }
    }
}
