using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Dictionaries.DealVariantDictionary;
using RealEstateDirectory.Dictionaries.DistrictDictionary;
using RealEstateDirectory.Dictionaries.FloorLevelDictionary;
using RealEstateDirectory.Dictionaries.LayoutDictionary;
using RealEstateDirectory.Dictionaries.MaterialDictionary;
using RealEstateDirectory.Dictionaries.OwnershipDictionary;
using RealEstateDirectory.Dictionaries.RealtorAgencyDictionary;
using RealEstateDirectory.Dictionaries.RealtorDictionary;
using RealEstateDirectory.Dictionaries.SewageDictionary;
using RealEstateDirectory.Dictionaries.StreetDictionary;
using RealEstateDirectory.Dictionaries.TerraceDictionary;
using RealEstateDirectory.Dictionaries.ToiletTypeDictionary;
using RealEstateDirectory.Dictionaries.WaterSupplyDictionary;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.MainFormTabs.Common;
using RealEstateDirectory.MainFormTabs.Room;

namespace RealEstateDirectory.Services
{
	public class ViewsService : IViewsService
	{
		private List<KeyValuePair<Type, Type>> _viewModelViewMap;

		public ViewsService(IServiceLocator serviceLocator)
		{
			_ServiceLocator = serviceLocator;
			Initialize();
		}

		private void Initialize()
		{
			_viewModelViewMap = new List<KeyValuePair<Type, Type>>();
			_viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (RealtorAgencyDictionaryViewModel),
			                                                   typeof (RealtorAgencyDictionaryView)));
			_viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (DealVariantsDictionaryViewModel),
			                                                   typeof (DealVariantsDictionaryView)));
			_viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (DistrictsDictionaryViewModel),
			                                                   typeof (DistrictsDictionaryView)));
			_viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (FloorLevelDictionaryViewModel),
			                                                   typeof (FloorLevelDictionaryView)));
			_viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (LayoutDictionaryViewModel), typeof (LayoutDictionaryView)));
			_viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (MaterialDictionaryViewModel),
			                                                   typeof (MaterialDictionaryView)));
			_viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (OwnershipDictionaryViewModel),
			                                                   typeof (OwnershipDictionaryView)));
			_viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (RealtorDictionaryViewModel),
			                                                   typeof (RealtorDictionaryView)));
			_viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (SewageDictionaryViewModel), typeof (SewageDictionaryView)));
			_viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (StreetDictionaryViewModel), typeof (StreetDictionaryView)));
			_viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (TerraceDictionaryViewModel),
			                                                   typeof (TerraceDictionaryView)));
			_viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (ToiletTypeDictionaryViewModel),
			                                                   typeof (ToiletTypeDictionaryView)));
			_viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (WaterSupplyDictionaryViewModel),
			                                                   typeof (WaterSupplyDictionaryView)));

		}

		#region Infrastructure

		private readonly IServiceLocator _ServiceLocator;

		#endregion

		public void OpenView<TViewModel>()
		{
			var needViewType = _viewModelViewMap.First(x => x.Key == typeof (TViewModel)).Value;
			if (needViewType != null)
			{
				var view = Application.Current.Windows.Cast<Window>().SingleOrDefault(window => window.GetType() == needViewType);
				if (view == null)
				{
					var newView = Activator.CreateInstance(needViewType);
					(newView as DictionaryWindow).DataContext = _ServiceLocator.GetInstance<TViewModel>();
					(newView as DictionaryWindow).Show();
				}
				else
				{
					view.Activate();
				}
			}
			else
			{
				_ServiceLocator.GetInstance<IMessageService>().ShowMessage("Не найден справочник", "Ошибка");
			}
		}

		public object GetView<TViewModel>()
		{
			var needViewType = _viewModelViewMap.First(x => x.Key == typeof (TViewModel)).Value;
			object needView = null;
			if (needViewType != null)
			{
				var view = Application.Current.Windows.Cast<Window>().SingleOrDefault(window => window.GetType() == needViewType);
				if (view == null)
				{
					var newView = Activator.CreateInstance(needViewType);
					(newView as DictionaryWindow).DataContext = _ServiceLocator.GetInstance<TViewModel>();
					needView = newView;
				}
				else
				{
					needView = view;
				}
			}
			else
			{
				_ServiceLocator.GetInstance<IMessageService>().ShowMessage("Не найден справочник", "Ошибка");
			}

			return needView;
		}

		public void OpenRoomDialog(RoomEditViewModel roomEditViewModel)
		{
			var view = Application.Current.Windows.Cast<Window>().SingleOrDefault(window => window.GetType() == typeof (RoomView));
			if (view == null)
			{
				var newView = new RoomView();
				newView.DataContext = roomEditViewModel;
				newView.ShowDialog();
			}
			else
			{
				view.Activate();
			}
		}

		public void CloseRoomDialog()
		{
			var view = Application.Current.Windows.Cast<Window>().SingleOrDefault(window => window.GetType() == typeof (RoomView));
			if (view != null)
			{
				view.Close();
			}
		}
	}
}