using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;

namespace RealEstateDirectory.Shell
{
    [NotifyForAll]
    public class ShellViewModel : NotificationObject
    {
        public ShellViewModel(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        #region Infrastructure

        private readonly IServiceLocator _serviceLocator;

        public void Initialize()
        {

        }

        #endregion
    }
}