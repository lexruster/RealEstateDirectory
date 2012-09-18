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

        public void Initialize()
        {
            //var l1=_dataService.CreateLayout("хрущевка");
            //var s1 = _dataService.CreateStreet("пушкина2");
            //var l2=_dataService.CreateLayout("сталинка");
            //var plot=_dataService.CreatePlot("Прекрасный участок2", s1);
        }

		#endregion
	}
}
