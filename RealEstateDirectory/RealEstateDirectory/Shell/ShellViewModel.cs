using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.Dictionaries;
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
			StreetsDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<StreetsDictionaryViewModel>());
		}

		#region Infrastructure

		private readonly IViewsService _ViewsService;

		#endregion

		public ICommand ExitCommand { get; private set; }

		public ICommand StreetsDictionaryCommand { get; private set; }
	}
}
