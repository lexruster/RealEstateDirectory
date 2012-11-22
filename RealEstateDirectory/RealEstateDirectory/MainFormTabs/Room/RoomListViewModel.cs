using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.MainFormTabs.Room
{
	[NotifyForAll]
	public class RoomListViewModel : NotificationObject
	{
		public RoomListViewModel(IServiceLocator serviceLocator, IRoomService roomService, IDistrictService districtService, IStreetService streetService, IViewsService viewsService)
		{
			_ServiceLocator = serviceLocator;
			_RoomService = roomService;
			_DistrictService = districtService;
			_StreetService = streetService;
			_ViewsService = viewsService;

			AddCommand = new DelegateCommand(Add);
			ChangeCommand = new DelegateCommand(Change);
			DeleteCommand = new DelegateCommand(Delete);
		}

		#region Infrastructure

		private readonly IServiceLocator _ServiceLocator;

		private readonly IRoomService _RoomService;

		private readonly IDistrictService _DistrictService;

		private readonly IStreetService _StreetService;
		private readonly IViewsService _ViewsService;

		public void Initialize()
		{
			_RoomService.StartSession();
			LoadFiltersData();
			LoadData();
			//_RoomService.StopSession();
		}

		#endregion

		private readonly District _AllDistricts = new District("< все >");

		private readonly District _NoneDistricts = new District("< не указано >");

		private readonly Street _AllStreets = new Street("< все >");

		private readonly Street _NoneStreets = new Street("< не указано >");

		private void LoadFiltersData()
		{
			//_DistrictService.StartSession();
			Districts = new ObservableCollection<District>((new [] { _AllDistricts, _NoneDistricts }).Concat(
				_DistrictService.GetAll()));
			//_DistrictService.StopSession();

			//_StreetService.StartSession();
			Streets = new ObservableCollection<Street>((new[] { _AllStreets, _NoneStreets }).Concat(
				_StreetService.GetAll()));
			//_StreetService.StopSession();
		}

		private void LoadData()
		{
			var rooms = _RoomService.GetAll();

			var t = rooms.Count();
			if (CurrentDistrict != null && CurrentDistrict != _AllDistricts)
				rooms = CurrentDistrict == _NoneDistricts
					? rooms.Where(room => room.District == null)
					: rooms.Where(room => room.District != null && room.District.Id == CurrentDistrict.Id);

			if (CurrentStreet != null && CurrentStreet != _AllStreets)
				rooms = CurrentStreet == _NoneStreets
					? rooms.Where(room => room.Street == null)
					: rooms.Where(room => room.Street != null && room.Street.Id == CurrentStreet.Id);

			Rooms = new ObservableCollection<Domain.Entities.Room>(rooms.ToArray());
		}

		public ObservableCollection<District> Districts { get; private set; }

		public District CurrentDistrict { get; set; }

		public ObservableCollection<Street> Streets { get; private set; }

		public Street CurrentStreet { get; private set; }
		public Domain.Entities.Room CurrentRoom { get; private set; }
		
		//public ListCollectionView Rooms { get; private set; }
		public ObservableCollection<Domain.Entities.Room> Rooms { get; private set; }
		

		public RoomViewModel PopupDataContext { get; private set; }

		public ICommand AddCommand { get; private set; }
		public ICommand ChangeCommand { get; private set; }
		public ICommand DeleteCommand { get; private set; }

		protected void Add()
		{
			var roomViewModel = _ServiceLocator.GetInstance<RoomViewModel>();
			roomViewModel.EditEnded += RoomViewModelOnEditEnded;
			roomViewModel.BeginEdit(new Domain.Entities.Room());
		}

		protected void Change()
		{
			var roomViewModel = _ServiceLocator.GetInstance<RoomViewModel>();
			roomViewModel.EditEnded += RoomViewModelOnEditEnded;
			roomViewModel.BeginEdit(CurrentRoom);
		}

		protected void Delete()
		{
			_RoomService.Delete(CurrentRoom);
			Rooms.Remove(CurrentRoom);
		}

		private void RoomViewModelOnEditEnded(RoomViewModel.EditEndedMode editEndedMode, Domain.Entities.Room room)
		{
			if(editEndedMode==RoomViewModel.EditEndedMode.Add)
			{
				//Rooms.AddNewItem(room);
				//Rooms.CommitNew();
				Rooms.Add(room);
			}
			if(editEndedMode==RoomViewModel.EditEndedMode.Edit)
			{
				//RaisePropertyChanged(PropertySupport.ExtractPropertyName(() => Rooms));
				var curItem = CurrentRoom;
				var t = Rooms;
				Rooms = null;
				Rooms = t;
				CurrentRoom = curItem;
			}
		}
	}
}