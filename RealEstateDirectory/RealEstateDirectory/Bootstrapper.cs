using System;
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
using RealEstateDirectory.Infrastructure.NHibernate.PersistenceContext;
using RealEstateDirectory.Dictionaries;
using RealEstateDirectory.Services;
using RealEstateDirectory.Shell;

namespace RealEstateDirectory
{
	public class Bootstrapper : UnityBootstrapper
	{
		protected override void ConfigureContainer()
		{
			base.ConfigureContainer();

            Container.RegisterType<Configuration>(new ContainerControlledLifetimeManager(),
                                                  new InjectionFactory(container => Configurator.GetConfig()));

            Container.RegisterType<IPersistenceContext, PersistenceContext>(new ContainerControlledLifetimeManager());
            RegisterRepositories();
            RegisterServices();
			RegisterViewModels();
		}

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

		private void RegisterRepositories()
        {
            Container.RegisterType<IDictionaryRepository, DictionaryRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRealEstateRepository, RealEstateRepository>(new ContainerControlledLifetimeManager());
        }

		private void RegisterServices()
        {
            Container.RegisterType<IDealVariantService, DealVariantService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDistrictService, DistrictService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IFloorLevelService, FloorLevelService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ILayoutService, LayoutService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMaterialService, MaterialService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IOwnershipService, OwnershipService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRealtorService, RealtorService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ISewageService, SewageService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IStreetService, StreetService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ITerraceService, TerraceService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IToiletTypeService, ToiletTypeService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IWaterSupplyService, WaterSupplyService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IFlatService, FlatService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRoomService, RoomService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IResidenceService, ResidenceService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IPlotService, PlotService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IHouseService, HouseService>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IDictionaryService<Layout>, LayoutService>(new ContainerControlledLifetimeManager());

			Container.RegisterType<IViewsService, ViewsService>();
			Container.RegisterType<IMessageService, MessageService>();
		}

		private void RegisterViewModels()
		{
			Container.RegisterType<DealVariantsDictionaryViewModel>(new InjectionMethod("Initialize"));
		}
	}
}
