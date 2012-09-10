using System;
using System.Linq;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.Dictionaries;

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
			if (typeof (TViewModel) == typeof (StreetDictionaryViewModel))
			{
				var currentView = Application.Current.Windows.Cast<Window>().SingleOrDefault(window => window.GetType() == typeof (DictionaryView));
				if (currentView == null)
					(new DictionaryView {DataContext = _ServiceLocator.GetInstance<TViewModel>()}).Show();
				else
					currentView.Activate();
			}
			else
				throw new NotImplementedException();
		}
	}
}