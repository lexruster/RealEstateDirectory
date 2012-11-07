using System;
using System.Linq;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.Dictionaries.DealVariantDictionary;
using RealEstateDirectory.Dictionaries.DistrictDictionary;

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
			if (typeof (TViewModel) == typeof (DealVariantsDictionaryViewModel))
			{
				var currentView = Application.Current.Windows.Cast<Window>().SingleOrDefault(window => window.GetType() == typeof (DealVariantsDictionaryView));
				if (currentView == null)
					(new DealVariantsDictionaryView {DataContext = _ServiceLocator.GetInstance<TViewModel>()}).Show();
				else
					currentView.Activate();
			}
			else if (typeof (TViewModel) == typeof (DistrictsDictionaryViewModel))
			{
				var currentView = Application.Current.Windows.Cast<Window>().SingleOrDefault(window => window.GetType() == typeof(DistrictsDictionaryView));
				if (currentView == null)
					(new DistrictsDictionaryView { DataContext = _ServiceLocator.GetInstance<TViewModel>() }).Show();
				else
					currentView.Activate();
			}
			else
				throw new NotImplementedException();
		}
	}
}