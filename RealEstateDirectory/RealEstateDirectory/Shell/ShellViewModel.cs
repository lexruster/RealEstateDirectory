using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;

namespace RealEstateDirectory.Shell
{
	[NotifyForAll]
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
