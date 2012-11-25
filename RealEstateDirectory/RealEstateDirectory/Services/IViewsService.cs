﻿using System.Windows;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.MainFormTabs.Common;
using RealEstateDirectory.MainFormTabs.Room;

namespace RealEstateDirectory.Services
{
	public interface IViewsService
	{
		void OpenView<TViewModel>();
		object GetView<T>();
		void OpenRoomDialog(RoomEditViewModel roomEditViewModel);
		void CloseRoomDialog();
	}
}