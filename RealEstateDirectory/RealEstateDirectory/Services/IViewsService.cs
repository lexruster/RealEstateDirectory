using System.Windows;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.MainFormTabs.Common;
using RealEstateDirectory.MainFormTabs.Flat;
using RealEstateDirectory.MainFormTabs.House;
using RealEstateDirectory.MainFormTabs.Plot;
using RealEstateDirectory.MainFormTabs.Room;

namespace RealEstateDirectory.Services
{
	public interface IViewsService
	{
		void OpenView<TViewModel>();
		object GetView<T>();
		void OpenRoomDialog(RoomEditViewModel roomEditViewModel);
		void CloseRoomDialog();
        void OpenFlatDialog(FlatEditViewModel roomEditViewModel);
        void CloseFlatDialog();

        void OpenPlotDialog(PlotEditViewModel roomEditViewModel);
        void ClosePlotDialog();

        void OpenHouseDialog(HouseEditViewModel roomEditViewModel);
        void CloseHouseDialog();
	}
}