﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Shell
{
	[NotifyForAll]
	public class ShellViewModel : NotificationObject
	{
		public ShellViewModel(IServiceLocator serviceLocator, IViewsService viewsService)
		{
			_ServiceLocator = serviceLocator;
			_ViewsService = viewsService;
		}

		#region Infrastructure

		public void Initialize()
		{
			ExitCommand = new DelegateCommand(() => Application.Current.Shutdown());
			StreetsDictionaryCommand = new DelegateCommand(() => _ViewsService.OpenView<StreetTemplatedDictionaryViewModel>());
		}

		private readonly IServiceLocator _ServiceLocator;

		private readonly IViewsService _ViewsService;

		#endregion

		public ICommand ExitCommand { get; private set; }

		public ICommand StreetsDictionaryCommand { get; private set; }
	}
}
