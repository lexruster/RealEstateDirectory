using System.Windows;
using System.Windows.Input;
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

namespace RealEstateDirectory.Shell
{
	[NotifyForAll]
	public class ShellViewModel : NotificationObject
	{
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
            
            FlatsDataContext = _ServiceLocator.GetInstance<FlatListViewModel>();
			RoomsDataContext = _ServiceLocator.GetInstance<FlatListViewModel>();
			PlotsDataContext = _ServiceLocator.GetInstance<PlotListViewModel>();
			HousesDataContext = _ServiceLocator.GetInstance<HouseListViewModel>();
			ResidenceDataContext = _ServiceLocator.GetInstance<ResidenceListViewModel>();
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

		public FlatListViewModel RoomsDataContext { get; private set; }
        public FlatListViewModel FlatsDataContext { get; private set; }
        public PlotListViewModel PlotsDataContext { get; private set; }
        public HouseListViewModel HousesDataContext { get; private set; }

		public ResidenceListViewModel ResidenceDataContext { get; private set; }
	}
}
