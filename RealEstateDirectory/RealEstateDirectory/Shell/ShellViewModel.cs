using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.ApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Infrastructure.NHibernate.DbSession;

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