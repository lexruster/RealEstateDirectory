using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using NHibernate.Cfg;
using NLog;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.ApplicationServices;
using RealEstateDirectory.ApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Dictionaries.ConditionDictionary;
using RealEstateDirectory.Dictionaries.DealVariantDictionary;
using RealEstateDirectory.Dictionaries.DestinationDictionary;
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
using RealEstateDirectory.MainFormTabs.Flat;
using RealEstateDirectory.MainFormTabs.House;
using RealEstateDirectory.MainFormTabs.Plot;
using RealEstateDirectory.MainFormTabs.Residence;
using RealEstateDirectory.MainFormTabs.Room;
using RealEstateDirectory.Migrations;
using RealEstateDirectory.Misc;
using RealEstateDirectory.Services;
using RealEstateDirectory.Services.Export;
using RealEstateDirectory.Shell;
using Configuration = NHibernate.Cfg.Configuration;

namespace RealEstateDirectory
{
	public class Bootstrapper : UnityBootstrapper
	{
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();
		private bool _appShutdown = false;

		#region Перегрузки

		protected override void ConfigureContainer()
		{
			base.ConfigureContainer();
			Log.Info("Контейнер сконфигурирован");
			Container.RegisterType<Configuration>(new ContainerControlledLifetimeManager(),
			                                      new InjectionFactory(container => Configurator.GetConfig()));
			Log.Info("Конфигурация БД получена");
			Container.RegisterType<IPersistenceContext, PersistenceContext>(new ContainerControlledLifetimeManager());
			RegisterRepositories();
			RegisterServices();
			RegisterViewModels();
			Log.Info("Типы зарегистрированы");
		}

		protected override DependencyObject CreateShell()
		{
			return Container.Resolve<ShellView>();
		}

		protected override void InitializeShell()
		{
			base.InitializeShell();
			Log.Info("Инициализация оболочки");
			Application.Current.MainWindow = (Window) Shell;
			Application.Current.MainWindow.Show();

			LoadConfig();
			if (_appShutdown) return;
			TryConfigureConnection();
			if (_appShutdown) return;
			CheckMigrationVersion();
			if (_appShutdown) return;
			RunMigrations();
			if (_appShutdown) return;

			try
			{
				((ShellView) Shell).DataContext = Container.Resolve<ShellViewModel>();
			}
			catch (Exception e)
			{
				var ms = new MessageService();
				ms.ShowMessage("Не удалось запустить приложение", "Ошибка", image: MessageBoxImage.Error);
				Log.ErrorException("Ошибка запуска приложения", e);
				Log.Info("Приложение завершает работу");
				Application.Current.Shutdown();
			}
		}

		public override void Run(bool runWithDefaultConfiguration)
		{
#if DEBUG
			HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
#endif
			base.Run(runWithDefaultConfiguration);
		}

		#endregion

		#region Регистрации

		private void RegisterRepositories()
		{
			Container.RegisterType<IDictionaryRepository, DictionaryRepository>(new ContainerControlledLifetimeManager());
			Container.RegisterType<IRealEstateRepository, RealEstateRepository>(new ContainerControlledLifetimeManager());
		}

		private void RegisterServices()
		{
			Container.RegisterType<IConditionService, ConditionService>(new ContainerControlledLifetimeManager());
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
			Container.RegisterType<IDestinationService, DestinationService>(new ContainerControlledLifetimeManager());

			Container.RegisterType<IFlatService, FlatService>(new ContainerControlledLifetimeManager());
			Container.RegisterType<IRoomService, RoomService>(new ContainerControlledLifetimeManager());
			Container.RegisterType<IResidenceService, ResidenceService>(new ContainerControlledLifetimeManager());
			Container.RegisterType<IPlotService, PlotService>(new ContainerControlledLifetimeManager());
			Container.RegisterType<IHouseService, HouseService>(new ContainerControlledLifetimeManager());
			Container.RegisterType<IRealtorAgencyService, RealtorAgencyService>(new ContainerControlledLifetimeManager());

			Container.RegisterType<IViewsService, ViewsService>();
			Container.RegisterType<IMessageService, MessageService>(new ContainerControlledLifetimeManager());
			Container.RegisterType<IUpdateService, UpdateService>();

			Container.RegisterType<IExcelService, ExcelService>();
			Container.RegisterType<IWordService, WordService>();
			Container.RegisterType<IDataGridExportService, DataGridExportService>();
		}

		private void RegisterViewModels()
		{
			Container.RegisterType<DealVariantsDictionaryViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<ConditionDictionaryViewModel>(new InjectionMethod("Initialize"));
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
			Container.RegisterType<DestinationDictionaryViewModel>(new InjectionMethod("Initialize"));

			Container.RegisterType<RealtorAgencyDictionaryViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<RoomListViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<FlatListViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<PlotListViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<HouseListViewModel>(new InjectionMethod("Initialize"));
			Container.RegisterType<ResidenceListViewModel>(new InjectionMethod("Initialize"));
		}

		#endregion

		#region Инициализация

		private void LoadConfig()
		{
			Utils.Config.Load();
			var connectionString = Utils.Config.GetProperty("DefaultConnectionString");
			Log.Info("Строка подключения загружена {0}", connectionString);
			if (String.IsNullOrEmpty(connectionString))
			{
				var configWindow = new ConfigWindow();
				var result = configWindow.ShowDialog();
				if (!result.HasValue || !result.Value)
				{
					Application.Current.Shutdown();
					_appShutdown = true;
				}
			}
		}

		private void TryConfigureConnection()
		{
			while (!TestConnection())
			{
				var configWindow = new ConfigWindow();
				var result = configWindow.ShowDialog();
				if (!result.HasValue || !result.Value)
				{
					_appShutdown = true;
					Application.Current.Shutdown();
				}
				if (_appShutdown) return;
			}
		}

		public bool TestConnection()
		{
			var result = true;
			Utils.Config.Load();
			var connectionString = Utils.Config.GetProperty("DefaultConnectionString");
			Log.Info("Тест подключения со строкой {0}", connectionString);
			var config = new NHibernate.Cfg.Configuration();
			config.Configure("hibernate.cfg.xml");
			config.DataBaseIntegration(
				properties => properties.ConnectionString = connectionString);

			try
			{
				using (var session = config.BuildSessionFactory().OpenSession())
				{
				}
			}
			catch (Exception ex)
			{
				result = false;
				Log.ErrorException("Тест подключения провалился", ex);
			}

			return result;
		}

		private void RunMigrations()
		{
			Utils.Config.Load();
			Log.Info("Миграции запущены");
			try
			{
				var context = new RunnerContext(new NullAnnouncer())
					{
						Database = "postgres",
						Connection = Utils.Config.GetProperty("DefaultConnectionString"),
						Target = Assembly.GetAssembly(typeof (MigrationsBeacon)).Location,
						PreviewOnly = false,
						NestedNamespaces = false,
						Task = "migrate"
					};
				new TaskExecutor(context).Execute();
				Log.Info("Миграции завершены");
			}
			catch (Exception e)
			{
				MessageBox.Show("Не удалось проверить актуальность БД", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				Log.ErrorException("Миграции провалены", e);
				Application.Current.Shutdown();
				_appShutdown = true;
			}
		}

		private int GetBdMigrationVersion()
		{
			var version = 0;
			Utils.Config.Load();
			var connectionString = Utils.Config.GetProperty("DefaultConnectionString");
			Log.Info("Получение версии бд со строкой {0}", connectionString);
			var config = new NHibernate.Cfg.Configuration();
			config.Configure("hibernate.cfg.xml");
			config.DataBaseIntegration(
				properties => properties.ConnectionString = connectionString);

			try
			{
				using (var session = config.BuildSessionFactory().OpenSession())
				{
					var versionString =
						session.CreateSQLQuery("SELECT \"Version\" FROM \"VersionInfo\" order by \"Version\" desc limit 1;").UniqueResult();
					version = Int32.Parse(versionString.ToString());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Не удалось проверить версию БД", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				Log.ErrorException("Тест версии бд провален", ex);
				Application.Current.Shutdown();
				_appShutdown = true;
			}

			return version;
		}

		private int GetCurrentMigrationVersion()
		{
			return Migrations.LastMigrationVersion.Version;
		}

		private void CheckMigrationVersion()
		{
			var currentMigrationVersion = GetCurrentMigrationVersion();
			var bdMigrationVersion = GetBdMigrationVersion();
			if (currentMigrationVersion < bdMigrationVersion)
			{
				var message =
					String.Format(
						"Программа устарела и не может работать с новой версией базы данных. Программа может работать с БД версией {0}, настоящая версия БД: {1}. Необходимо обновить программу. Скачать новую версию прямо сейчас?",
						currentMigrationVersion, bdMigrationVersion);
				Log.Info(message);

				if (MessageBox.Show(message, "Программа устарела", MessageBoxButton.OKCancel, MessageBoxImage.Information) ==
				    MessageBoxResult.OK)
				{
					Log.Info("Установка обновления");
					Process.Start(ConfigurationManager.AppSettings["UpdateUrl"]);
				}
				Application.Current.Shutdown();
				_appShutdown = true;
			}
		}

		#endregion
	}
}