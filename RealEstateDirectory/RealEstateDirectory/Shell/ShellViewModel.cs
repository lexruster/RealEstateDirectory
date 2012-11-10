using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.Dictionaries.DealVariantDictionary;
using RealEstateDirectory.Dictionaries.DistrictDictionary;
using RealEstateDirectory.Dictionaries.FloorLevelDictionary;
using RealEstateDirectory.Dictionaries.LayoutDictionary;
using RealEstateDirectory.Dictionaries.MaterialDictionary;
using RealEstateDirectory.Dictionaries.OwnershipDictionary;
using RealEstateDirectory.Dictionaries.RealtorDictionary;
using RealEstateDirectory.Dictionaries.SewageDictionary;
using RealEstateDirectory.Dictionaries.TerraceDictionary;
using RealEstateDirectory.Dictionaries.ToiletTypeDictionary;
using RealEstateDirectory.Dictionaries.WaterSupplyDictionary;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Shell
{
	[NotifyForAll]
	public class ShellViewModel : NotificationObject
	{
		public ShellViewModel(IViewsService viewsService)
		{
			_ViewsService = viewsService;

			ExitCommand = new DelegateCommand(() => Application.Current.Shutdown());
			DealVariantsDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<DealVariantsDictionaryViewModel>());
			DistrictsDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<DistrictsDictionaryViewModel>());

			FloorLevelDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<FloorLevelDictionaryViewModel>());
			LayoutDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<LayoutDictionaryViewModel>());
			MaterialDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<MaterialDictionaryViewModel>());
			OwnershipDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<OwnershipDictionaryViewModel>());
			RealtorDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<RealtorDictionaryViewModel>());
			SewageDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<SewageDictionaryViewModel>());
			//StreetDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<StreetDictionaryViewModel>());
			TerraceDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<TerraceDictionaryViewModel>());
			ToiletTypeDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<ToiletTypeDictionaryViewModel>());
			WaterSupplyDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<WaterSupplyDictionaryViewModel>());


		}

		#region Infrastructure

		private readonly IViewsService _ViewsService;

		#endregion

		public ICommand ExitCommand { get; private set; }
		public ICommand DealVariantsDictionaryCommand { get; private set; }
		public ICommand DistrictsDictionaryCommand { get; private set; }

		public ICommand FloorLevelDictionaryCommand { get; private set; }
		public ICommand LayoutDictionaryCommand { get; private set; }
		public ICommand MaterialDictionaryCommand { get; private set; }
		public ICommand OwnershipDictionaryCommand { get; private set; }
		public ICommand RealtorDictionaryCommand { get; private set; }
		public ICommand SewageDictionaryCommand { get; private set; }
		//public ICommand StreetDictionaryCommand { get; private set; }
		public ICommand TerraceDictionaryCommand { get; private set; }
		public ICommand ToiletTypeDictionaryCommand { get; private set; }
		public ICommand WaterSupplyDictionaryCommand { get; private set; }

	}
}
