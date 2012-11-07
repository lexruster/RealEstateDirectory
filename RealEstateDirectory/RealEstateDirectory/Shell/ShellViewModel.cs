using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.Dictionaries.DealVariantDictionary;
using RealEstateDirectory.Dictionaries.DistrictDictionary;
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
		}

		#region Infrastructure

		private readonly IViewsService _ViewsService;

		#endregion

		public ICommand ExitCommand { get; private set; }
		public ICommand DealVariantsDictionaryCommand { get; private set; }
		public ICommand DistrictsDictionaryCommand { get; private set; }
	}
}
