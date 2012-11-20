using System;
using System.ComponentModel;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.MainFormTabs.Room
{
	[NotifyForAll]
	public class RoomViewModel : NotificationObject, IDataErrorInfo
	{
		public RoomViewModel(IRoomService service)
		{
			_RoomService = service;
		}

		#region Infrastructure

		private readonly IRoomService _RoomService;

		#endregion

		public int Id { get; set; }

		public string Rooms { get; set; }

		public District District { get; set; }

		public Street Street { get; set; }

		public string HouseNumber { get; set; }

		public string this[string propertyName]
		{
			get
			{
				if (propertyName == PropertySupport.ExtractPropertyName(() => Rooms))
				{
					int rooms;
					if (!Int32.TryParse(Rooms, out rooms) || rooms < 1)
						return "Неверное количество комнат";
				}
					
				return null;
			}
		}

		public string Error
		{
			get
			{
				var validation = _RoomService.IsValid(new Domain.Entities.Room
					{
						RoomCount = Int32.Parse(Rooms),
						District = District,
						Street = Street,
						TerritorialNumber = HouseNumber
					}, Id);
				return validation.IsValid ? null : validation.GetReasons();
			}
		}
	}
}