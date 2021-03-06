﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Dictionaries.ConditionDictionary;
using RealEstateDirectory.Dictionaries.DealVariantDictionary;
using RealEstateDirectory.Dictionaries.DestinationDictionary;
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
using RealEstateDirectory.MainFormTabs.Residence;
using RealEstateDirectory.MainFormTabs.Flat;
using RealEstateDirectory.MainFormTabs.House;
using RealEstateDirectory.MainFormTabs.Plot;
using RealEstateDirectory.MainFormTabs.Room;
using RealEstateDirectory.Misc;

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
            _viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (LayoutDictionaryViewModel),
                                                               typeof (LayoutDictionaryView)));
            _viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (MaterialDictionaryViewModel),
                                                               typeof (MaterialDictionaryView)));
            _viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (OwnershipDictionaryViewModel),
                                                               typeof (OwnershipDictionaryView)));
            _viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (RealtorDictionaryViewModel),
                                                               typeof (RealtorDictionaryView)));
            _viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (SewageDictionaryViewModel),
                                                               typeof (SewageDictionaryView)));
            _viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (StreetDictionaryViewModel),
                                                               typeof (StreetDictionaryView)));
            _viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (TerraceDictionaryViewModel),
                                                               typeof (TerraceDictionaryView)));
            _viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (ToiletTypeDictionaryViewModel),
                                                               typeof (ToiletTypeDictionaryView)));
            _viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof (WaterSupplyDictionaryViewModel),
                                                               typeof (WaterSupplyDictionaryView)));
			_viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof(ConditionDictionaryViewModel),
															   typeof(ConditionDictionaryView)));
			_viewModelViewMap.Add(new KeyValuePair<Type, Type>(typeof(DestinationDictionaryViewModel),
															   typeof(DestinationDictionaryView)));
			
        }

        #region Infrastructure

        private readonly IServiceLocator _ServiceLocator;

        #endregion

        public void OpenView<TViewModel>()
        {
            var needViewType = _viewModelViewMap.First(x => x.Key == typeof (TViewModel)).Value;
            if (needViewType != null)
            {
                var view =
                    Application.Current.Windows.Cast<Window>().SingleOrDefault(
                        window => window.GetType() == needViewType);
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
                var view =
                    Application.Current.Windows.Cast<Window>().SingleOrDefault(
                        window => window.GetType() == needViewType);
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
            var view =
                Application.Current.Windows.Cast<Window>().SingleOrDefault(
                    window => window.GetType() == typeof (RoomEditView));
            if (view == null)
            {
                var newView = new RoomEditView();
                newView.DataContext = roomEditViewModel;
                newView.ShowDialog();
            }
            else
            {
                view.Activate();
            }
        }

        public void OpenResidenceDialog(ResidenceEditViewModel residenceEditViewModel)
        {
            var newView = new ResidenceEditView();
            newView.DataContext = residenceEditViewModel;
            newView.ShowDialog();
        }

        public void CloseResidenceDialog()
        {
            var view =
                Application.Current.Windows.Cast<Window>().SingleOrDefault(
                    window => window.GetType() == typeof (ResidenceEditView));
            if (view != null)
                view.Close();
        }

        public void CloseRoomDialog()
        {
            var view =
                Application.Current.Windows.Cast<Window>().SingleOrDefault(
                    window => window.GetType() == typeof (RoomEditView));
            if (view != null)
            {
                view.Close();
            }
        }

        public void OpenFlatDialog(FlatEditViewModel viewModel)
        {
            var newView = new FlatEditView();
            newView.DataContext = viewModel;
            newView.ShowDialog();
        }

        public void CloseFlatDialog()
        {
            var view =
                Application.Current.Windows.Cast<Window>().SingleOrDefault(
                    window => window.GetType() == typeof (FlatEditView));
            if (view != null)
            {
                view.Close();
            }
        }

        public void OpenPlotDialog(PlotEditViewModel viewModel)
        {
            var newView = new PlotEditView();
            newView.DataContext = viewModel;
            newView.ShowDialog();
        }

        public void ClosePlotDialog()
        {
            var view =
                Application.Current.Windows.Cast<Window>().SingleOrDefault(
                    window => window.GetType() == typeof (PlotEditView));
            if (view != null)
            {
                view.Close();
            }
        }


        public void OpenHouseDialog(HouseEditViewModel viewModel)
        {
            var newView = new HouseEditView();
            newView.DataContext = viewModel;
            newView.ShowDialog();
        }

        public void CloseHouseDialog()
        {
            var view =
                Application.Current.Windows.Cast<Window>().SingleOrDefault(
                    window => window.GetType() == typeof (HouseEditView));
            if (view != null)
            {
                view.Close();
            }
        }

	    public void OpenAboutDialog()
	    {
			var newView = new About();
			newView.ShowDialog();
	    }

	    public void OpenConfigDialog()
	    {
			var newView = new ConfigWindow();
			newView.ShowDialog();
	    }
    }
}