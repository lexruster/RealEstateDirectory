using System.Windows;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Mapping.ByCode;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.ApplicationServices;
using RealEstateDirectory.ApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Data.Config;
using RealEstateDirectory.Domain.Data.Mapping;
using RealEstateDirectory.Domain.Data.Repository;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Domain.Entities.Dictionaries;
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
            ((ShellView) Shell).DataContext = Container.Resolve<ShellViewModel>();
            Application.Current.MainWindow = (Window) Shell;
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
                                                  new InjectionFactory(container => Configurator.GetConfig()));

            Container.RegisterType<IPersistenceContext, PersistenceContext>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ShellViewModel>(new InjectionMethod("Initialize"));

            RegisterRepositories();
            RegisterServices();

            Container.RegisterType<IDataService, DataService>(new ContainerControlledLifetimeManager());
        }

        private void RegisterRepositories()
        {
            Container.RegisterType<IDictionaryRepository<DealVariant>, DictionaryRepository<DealVariant>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryRepository<Ownership>, DictionaryRepository<Ownership>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryRepository<Sewage>, DictionaryRepository<Sewage>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryRepository<District>, DictionaryRepository<District>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryRepository<Layout>, DictionaryRepository<Layout>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryRepository<Material>, DictionaryRepository<Material>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryRepository<Realtor>, DictionaryRepository<Realtor>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryRepository<Street>, DictionaryRepository<Street>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryRepository<Terrace>, DictionaryRepository<Terrace>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryRepository<ToiletType>, DictionaryRepository<ToiletType>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryRepository<WaterSupply>, DictionaryRepository<WaterSupply>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryRepository<FloorLevel>, DictionaryRepository<FloorLevel>>(
                new ContainerControlledLifetimeManager());

            Container.RegisterType<IRealEstateRepository<Flat>, RealEstateRepository<Flat>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IRealEstateRepository<Room>, RealEstateRepository<Room>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IRealEstateRepository<Apartment>, RealEstateRepository<Apartment>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IRealEstateRepository<Building>, RealEstateRepository<Building>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IRealEstateRepository<Residence>, RealEstateRepository<Residence>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IRealEstateRepository<House>, RealEstateRepository<House>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IRealEstateRepository<Plot>, RealEstateRepository<Plot>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IRealEstateRepository<RealEstate>, RealEstateRepository<RealEstate>>(
                new ContainerControlledLifetimeManager());
        }

        private void RegisterServices()
        {
            Container.RegisterType<IDictionaryService<Ownership>, DictionaryService<Ownership>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryService<Sewage>, DictionaryService<Sewage>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryService<District>, DictionaryService<District>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryService<Layout>, DictionaryService<Layout>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryService<Material>, DictionaryService<Material>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryService<Realtor>, DictionaryService<Realtor>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryService<Street>, DictionaryService<Street>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryService<Terrace>, DictionaryService<Terrace>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryService<ToiletType>, DictionaryService<ToiletType>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryService<WaterSupply>, DictionaryService<WaterSupply>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IDictionaryService<FloorLevel>, DictionaryService<FloorLevel>>(
                new ContainerControlledLifetimeManager());

            Container.RegisterType<IRealEstateService<Flat>, RealEstateService<Flat>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IRealEstateService<Room>, RealEstateService<Room>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IRealEstateService<Apartment>, RealEstateService<Apartment>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IRealEstateService<Building>, RealEstateService<Building>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IRealEstateService<Residence>, RealEstateService<Residence>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IRealEstateService<House>, RealEstateService<House>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IRealEstateService<Plot>, RealEstateService<Plot>>(
                new ContainerControlledLifetimeManager());
            Container.RegisterType<IRealEstateService<RealEstate>, RealEstateService<RealEstate>>(
                new ContainerControlledLifetimeManager());
        }
    }
}