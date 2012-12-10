﻿using System;
using System.Windows;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using NHibernate.Cfg;
using NLog;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.ApplicationServices;
using RealEstateDirectory.ApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Dictionaries.DealVariantDictionary;
using RealEstateDirectory.Dictionaries.DistrictDictionary;
using RealEstateDirectory.Dictionaries.FloorLevelDictionary;
using RealEstateDirectory.Dictionaries.LayoutDictionary;
using RealEstateDirectory.Dictionaries.MaterialDictionary;
using RealEstateDirectory.Dictionaries.OwnershipDictionary;
using RealEstateDirectory.Dictionaries.RealtorAgencyDictionary;
using RealEstateDirectory.Dictionaries.RealtorDictionary;
using RealEstateDirectory.Dictionaries.SewageDictionary;
using RealEstateDirectory.Dictionaries.StreetDictionary;
using RealEstateDirectory.Dictionaries.TerraceDictionary;
using RealEstateDirectory.Dictionaries.ToiletTypeDictionary;
using RealEstateDirectory.Dictionaries.WaterSupplyDictionary;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Data.Config;
using RealEstateDirectory.Domain.Data.Repository;
using RealEstateDirectory.Infrastructure.NHibernate.PersistenceContext;
using RealEstateDirectory.MainFormTabs;
using RealEstateDirectory.MainFormTabs.Flat;
using RealEstateDirectory.MainFormTabs.House;
using RealEstateDirectory.MainFormTabs.Plot;
using RealEstateDirectory.MainFormTabs.Residence;
using RealEstateDirectory.MainFormTabs.Room;
using RealEstateDirectory.Misc;
using RealEstateDirectory.Services;
using RealEstateDirectory.Services.Export;
using RealEstateDirectory.Shell;
using Environment = System.Environment;
using RoomListViewModel = RealEstateDirectory.MainFormTabs.Room.RoomListViewModel;

namespace RealEstateDirectory
{
	public class Bootstrapper : UnityBootstrapper
	{
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();
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
			try
			{
				((ShellView) Shell).DataContext = Container.Resolve<ShellViewModel>();
			}
			catch (Exception e)
			{
				var ms = new MessageService();
				var error = e.Message;
				
				ms.ShowMessage(error, "Ошибка", image: MessageBoxImage.Error);

				var configWindow = new ConfigWindow();
				var result = configWindow.ShowDialog();
				Log.ErrorException("Ошибка запуска приложения", e);
				Log.Info("Приложение завершает работу");
				Application.Current.Shutdown();
				return;
			}

			Application.Current.MainWindow = (Window) Shell;
		}

		public override void Run(bool runWithDefaultConfiguration)
		{
#if DEBUG
			HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
#endif

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
			Container.RegisterType<IRealtorAgencyService, RealtorAgencyService>(new ContainerControlledLifetimeManager());

			Container.RegisterType<IViewsService, ViewsService>();
			Container.RegisterType<IMessageService, MessageService>();

			Container.RegisterType<IExcelService, ExcelService>();
		}

		private void RegisterViewModels()
		{
			Container.RegisterType<DealVariantsDictionaryViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<DistrictsDictionaryViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<FloorLevelDictionaryViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<LayoutDictionaryViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<MaterialDictionaryViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<OwnershipDictionaryViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<RealtorDictionaryViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<SewageDictionaryViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<StreetDictionaryViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<TerraceDictionaryViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<ToiletTypeDictionaryViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<WaterSupplyDictionaryViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<RealtorAgencyDictionaryViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<RoomListViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<FlatListViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<PlotListViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<HouseListViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<ResidenceListViewModel>(new InjectionMethod("Initialize"));
		}
	}
}