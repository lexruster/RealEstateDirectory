using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;

namespace RealEstateDirectory.Shell
{
	[NotifyForAll]
	public class ShellViewModel : NotificationObject
	{
		public ShellViewModel(IServiceLocator serviceLocator, IDataService dataService)
		{
			_serviceLocator = serviceLocator;
			_dataService = dataService;
		}

		#region Infrastructure

		private readonly IServiceLocator _serviceLocator;
		private readonly IDataService _dataService;

		#endregion
	}
}
