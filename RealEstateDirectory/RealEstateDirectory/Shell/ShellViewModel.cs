using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace RealEstateDirectory.Shell
{
	public class ShellViewModel : NotificationObject
	{
		public ShellViewModel(IServiceLocator serviceLocator)
		{
			_ServiceLocator = serviceLocator;
		}

		#region Infrastructure

		private readonly IServiceLocator _ServiceLocator;

		#endregion
	}
}
