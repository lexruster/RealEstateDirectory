using System.Linq;
using System.Windows.Data;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.MainFormTabs.Room
{
	[NotifyForAll]
	public class RoomListViewModel : NotificationObject
	{
		public RoomListViewModel(IRoomService roomService, IDistrictService districtService, IStreetService streetService)
		{
			_RoomService = roomService;
			_DistrictService = districtService;
			_StreetService = streetService;
		}

		#region Infrastructure

		private readonly IRoomService _RoomService;

		private readonly IDistrictService _DistrictService;

		private readonly IStreetService _StreetService;

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
			Districts = new ListCollectionView((new [] { _AllDistricts, _NoneDistricts }).Concat(
				_DistrictService.GetAll().OrderBy(district => district.Name)).ToArray());
			//_DistrictService.StopSession();

			//_StreetService.StartSession();
			Streets = new ListCollectionView((new[] { _AllStreets, _NoneStreets }).Concat(
				_StreetService.GetAll().OrderBy(street => street.Name)).ToArray());
			//_StreetService.StopSession();
		}

		private void LoadData()
		{
			var rooms = _RoomService.GetAll();

			if (Districts.CurrentItem != null && Districts.CurrentItem != _AllDistricts)
				rooms = Districts.CurrentItem == _NoneDistricts
					? rooms.Where(room => room.District == null)
					: rooms.Where(room => room.District != null && room.District.Id == ((District) Districts.CurrentItem).Id);

			if (Streets.CurrentItem != null && Streets.CurrentItem != _AllStreets)
				rooms = Streets.CurrentItem == _NoneStreets
					? rooms.Where(room => room.Street == null)
					: rooms.Where(room => room.Street != null && room.Street.Id == ((Street)Streets.CurrentItem).Id);

			Rooms = new ListCollectionView(rooms.ToArray());
		}

		public ListCollectionView Districts { get; private set; }

		public ListCollectionView Streets { get; private set; }

		public ListCollectionView Rooms { get; private set; }
	}
}