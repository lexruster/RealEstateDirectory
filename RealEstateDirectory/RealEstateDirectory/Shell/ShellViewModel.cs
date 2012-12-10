using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
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
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.MainFormTabs;
using RealEstateDirectory.MainFormTabs.Flat;
using RealEstateDirectory.MainFormTabs.House;
using RealEstateDirectory.MainFormTabs.Plot;
using RealEstateDirectory.MainFormTabs.Residence;
using RealEstateDirectory.MainFormTabs.Room;
using RealEstateDirectory.Services;
using RealEstateDirectory.Utils;

namespace RealEstateDirectory.Shell
{
	[NotifyForAll]
	public class ShellViewModel : NotificationObject
	{
		private DispatcherTimer _timer;

		public ShellViewModel(IViewsService viewsService, IServiceLocator serviceLocator)
		{
			_ViewsService = viewsService;
			_ServiceLocator = serviceLocator;

			ExitCommand = new DelegateCommand(() => Application.Current.Shutdown());
			RealtorAgencyDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<RealtorAgencyDictionaryViewModel>());
			DealVariantsDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<DealVariantsDictionaryViewModel>());
			DistrictsDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<DistrictsDictionaryViewModel>());

			FloorLevelDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<FloorLevelDictionaryViewModel>());
			LayoutDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<LayoutDictionaryViewModel>());
			MaterialDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<MaterialDictionaryViewModel>());
			OwnershipDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<OwnershipDictionaryViewModel>());
			RealtorDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<RealtorDictionaryViewModel>());
			SewageDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<SewageDictionaryViewModel>());
			StreetDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<StreetDictionaryViewModel>());
			TerraceDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<TerraceDictionaryViewModel>());
			ToiletTypeDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<ToiletTypeDictionaryViewModel>());
			WaterSupplyDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<WaterSupplyDictionaryViewModel>());
			AboutCommand = new DelegateCommand(() => _ViewsService.OpenAboutDialog());
			CheckUpdatesCommand = new DelegateCommand(() => CheckUpdates(true));
			ConfigCommand=new DelegateCommand(() => _ViewsService.OpenConfigDialog());

			FlatsDataContext = _ServiceLocator.GetInstance<FlatListViewModel>();
			RoomsDataContext = _ServiceLocator.GetInstance<RoomListViewModel>();
			PlotsDataContext = _ServiceLocator.GetInstance<PlotListViewModel>();
			HousesDataContext = _ServiceLocator.GetInstance<HouseListViewModel>();
			ResidenceDataContext = _ServiceLocator.GetInstance<ResidenceListViewModel>();

			_timer = new DispatcherTimer();
			_timer.Tick += timerTick;
			_timer.Interval = new TimeSpan(0, 0, 5);
			_timer.Start();
		}

		private void timerTick(object sender, EventArgs e)
		{
			_timer.Stop();
			var thread = new Thread(CheckUpdatesOnTimer);
			thread.Start();
		}

		#region Infrastructure

		private readonly IViewsService _ViewsService;
		private readonly IServiceLocator _ServiceLocator;

		#endregion

		public ICommand ExitCommand { get; private set; }

		public ICommand RealtorAgencyDictionaryCommand { get; private set; }
		public ICommand DealVariantsDictionaryCommand { get; private set; }
		public ICommand DistrictsDictionaryCommand { get; private set; }

		public ICommand FloorLevelDictionaryCommand { get; private set; }
		public ICommand LayoutDictionaryCommand { get; private set; }
		public ICommand MaterialDictionaryCommand { get; private set; }
		public ICommand OwnershipDictionaryCommand { get; private set; }
		public ICommand RealtorDictionaryCommand { get; private set; }
		public ICommand SewageDictionaryCommand { get; private set; }
		public ICommand StreetDictionaryCommand { get; private set; }
		public ICommand TerraceDictionaryCommand { get; private set; }
		public ICommand ToiletTypeDictionaryCommand { get; private set; }
		public ICommand WaterSupplyDictionaryCommand { get; private set; }
		public ICommand AboutCommand { get; private set; }
		public ICommand CheckUpdatesCommand { get; private set; }
		public ICommand ConfigCommand { get; private set; }

		public FlatListViewModel FlatsDataContext { get; private set; }
		public RoomListViewModel RoomsDataContext { get; private set; }
		public PlotListViewModel PlotsDataContext { get; private set; }
		public HouseListViewModel HousesDataContext { get; private set; }
		public ResidenceListViewModel ResidenceDataContext { get; private set; }

		public void CheckUpdatesOnTimer()
		{
			CheckUpdates(false);
		}

		public void CheckUpdates(bool showSucces)
		{
			var messageService = _ServiceLocator.GetInstance<IMessageService>();
			try
			{
				var webRequest = new HTTP();
				var remoteVersion =
					Version.Parse(webRequest.ReadFromServer(ConfigurationManager.AppSettings["GetVersionUrl"], 10000));
				var localVersion = Assembly.GetExecutingAssembly().GetName().Version;

				if (remoteVersion.CompareTo(localVersion) > 0)
				{
					var message =
						String.Format(
							"Вышла новая версия ПО. Ваша версия {0}, новая версия {1}. Рекомендуется обновиться. Скачать новую версию прямо сейчас?",
							localVersion, remoteVersion);
					if (messageService.ShowMessage(message, "Новая версия", image: MessageBoxImage.Information,
					                               buttons: MessageBoxButton.OKCancel) == MessageBoxResult.OK)
					{
						Process.Start(ConfigurationManager.AppSettings["UpdateUrl"]);
					}
				}
				else
				{
					if (showSucces)
					{
						messageService.ShowMessage("Вы используете самую последнюю версию.", "Программа обновлена",
						                           image: MessageBoxImage.Information);
					}
				}
			}
			catch (Exception)
			{
				if (showSucces)
				{
					messageService.ShowMessage("Ошибка определения наличия обновлений.", "Ошибка", image: MessageBoxImage.Error);
				}
			}

			_timer.Interval = new TimeSpan(0, 30, 0);
			_timer.Start();
		}
	}
}