using System;
using System.Linq;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.Dictionaries;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Services
{
    public class ViewsService : IViewsService
    {
        public ViewsService(IServiceLocator serviceLocator)
        {
            _ServiceLocator = serviceLocator;
        }

        #region Infrastructure

        private readonly IServiceLocator _ServiceLocator;

        #endregion

        public void OpenView<TViewModel>()
        {
            if (typeof(TViewModel) == typeof(StreetsDictionaryViewModel))
            {
                var currentView =
                    Application.Current.Windows.Cast<Window>().SingleOrDefault(
                        window => window.GetType() == typeof(StreetsDictionaryView));
                if (currentView == null)
                    (new StreetsDictionaryView { DataContext = _ServiceLocator.GetInstance<TViewModel>() }).Show();
                else
                    currentView.Activate();
                return;
            }
            if (typeof(TViewModel) == typeof(DealVariantsDictionaryViewModel))
            {
                var currentView =
                    Application.Current.Windows.Cast<Window>().SingleOrDefault(
                        window => window.GetType() == typeof(DealVariantsDictionaryView));
                if (currentView == null)
                    (new DealVariantsDictionaryView { DataContext = _ServiceLocator.GetInstance<TViewModel>() }).Show();
                else
                    currentView.Activate();
                return;
            }
            {
                var currentView =
                    Application.Current.Windows.Cast<Window>().SingleOrDefault(
                        window => window.GetType() == typeof(CommonDictionaryView));
                if (currentView == null)
                    (new CommonDictionaryView { DataContext = _ServiceLocator.GetInstance<TViewModel>() }).Show();
                else
                    currentView.Activate();
                return;
            }
            throw new NotImplementedException();
        }
    }
}