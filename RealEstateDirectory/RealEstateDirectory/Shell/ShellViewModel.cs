using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Misc.Miscellaneous;
using NotifyPropertyWeaver;
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
using RealEstateDirectory.MainFormTabs.Flat;
using RealEstateDirectory.MainFormTabs.House;
using RealEstateDirectory.MainFormTabs.Plot;
using RealEstateDirectory.MainFormTabs.Residence;
using RealEstateDirectory.MainFormTabs.Room;
using RealEstateDirectory.Services;
using RealEstateDirectory.Services.Export;
using RealEstateDirectory.Utils;

namespace RealEstateDirectory.Shell
{
	[NotifyForAll]
	public class ShellViewModel : NotificationObject
	{
		public ShellViewModel(IViewsService viewsService, IServiceLocator serviceLocator, IUpdateService updateService)
		{
			_ViewsService = viewsService;
			_ServiceLocator = serviceLocator;
			_UpdateService = updateService;

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
			ConditionDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<ConditionDictionaryViewModel>());
			DestinationDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<DestinationDictionaryViewModel>());
			
			AboutCommand = new DelegateCommand(() => _ViewsService.OpenAboutDialog());
			CheckUpdatesCommand = new DelegateCommand(() => _UpdateService.CheckUpdates(true));
			ConfigCommand = new DelegateCommand(() => _ViewsService.OpenConfigDialog());
			ExportToWordCommand = new DelegateCommand(ExportToWord);

			FlatsDataContext = _ServiceLocator.GetInstance<FlatListViewModel>();
			RoomsDataContext = _ServiceLocator.GetInstance<RoomListViewModel>();
			PlotsDataContext = _ServiceLocator.GetInstance<PlotListViewModel>();
			HousesDataContext = _ServiceLocator.GetInstance<HouseListViewModel>();
			ResidenceDataContext = _ServiceLocator.GetInstance<ResidenceListViewModel>();

			_UpdateService.StartPeriodicCheck();
		}

		#region Infrastructure

		private readonly IViewsService _ViewsService;
		private readonly IServiceLocator _ServiceLocator;
		private readonly IUpdateService _UpdateService;

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
		public ICommand ConditionDictionaryCommand { get; private set; }
		public ICommand DestinationDictionaryCommand { get; private set; }
		
		public ICommand AboutCommand { get; private set; }
		public ICommand CheckUpdatesCommand { get; private set; }
		public ICommand ConfigCommand { get; private set; }
		public ICommand ExportToWordCommand { get; private set; }

		public FlatListViewModel FlatsDataContext { get; private set; }
		public RoomListViewModel RoomsDataContext { get; private set; }
		public PlotListViewModel PlotsDataContext { get; private set; }
		public HouseListViewModel HousesDataContext { get; private set; }
		public ResidenceListViewModel ResidenceDataContext { get; private set; }

		private void ExportToWord()
		{
			var _wordService = _ServiceLocator.GetInstance<IWordService>();
			var obj = new ExportObject();

			obj.Tables.Add(FlatsDataContext.GetExportedTable(ExportMode.All));
			obj.Tables.Add(RoomsDataContext.GetExportedTable(ExportMode.All));
			obj.Tables.Add(PlotsDataContext.GetExportedTable(ExportMode.All));
			obj.Tables.Add(HousesDataContext.GetExportedTable(ExportMode.All));
			obj.Tables.Add(ResidenceDataContext.GetExportedTable(ExportMode.All));

			_wordService.ExportToWord(obj);
		}
	}
}