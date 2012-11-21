using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.MainFormTabs.Room
{
	[NotifyForAll]
	public class RoomViewModel : NotificationObject, IDataErrorInfo
	{
		public enum EditEndedMode
		{
			Cancel = 1,
			Add = 2,
			Edit = 3
		}

		#region Конструктор

		public RoomViewModel(IRoomService service, IMessageService messageService,
		                     IDistrictService districtService, IViewsService viewsService)
		{
			_RoomService = service;
			_MessageService = messageService;
			_DistrictService = districtService;
			_ViewsService = viewsService;

			PropertyChanged += (sender, args) =>
				{
					//if (args.PropertyName == PropertySupport.ExtractPropertyName(() => HouseNumber) ||
					//	args.PropertyName == PropertySupport.ExtractPropertyName(() => StreetList) ||
					//	args.PropertyName == PropertySupport.ExtractPropertyName(() => DistrictList))
					//{
					//	OkCommand.RaiseCanExecuteChanged();
					//	CancelCommand.RaiseCanExecuteChanged();
					//}
				};

			OkCommand = new DelegateCommand(() =>
				{
					var mode = Id == 0 ? EditEndedMode.Add : EditEndedMode.Edit;
					var error = Error;
					if (error == null)
					{
						UpdateModelFromValues();
						SaveToDatabase();
						_ViewsService.CloseRoomDialog(this);
						OnEditEnded(mode, DbEntity);
					}
					else
					{
						_MessageService.ShowMessage(error, "Ошибка", image: MessageBoxImage.Error);
					}
				}, CanOk);

			CancelCommand = new DelegateCommand(() =>
				{
					UpdateValuesFromModel();
					_ViewsService.CloseRoomDialog(this);
					OnEditEnded(EditEndedMode.Cancel, null);
				}, CanCancel);
		}

		#endregion

		#region Infrastructure

		private readonly IRoomService _RoomService;
		private readonly IMessageService _MessageService;
		private readonly IDistrictService _DistrictService;
		private readonly IViewsService _ViewsService;

		

		#endregion

		#region Свойства  INotifi

		public ListCollectionView DistrictList { get; private set; }
		public ListCollectionView StreetList { get; private set; }
		public int Rooms { get; set; }
		public string HouseNumber { get; set; }

		#endregion

		#region Свойства

        public event Action<EditEndedMode, Domain.Entities.Room> EditEnded;
		protected int Id { get; set; }
		public Domain.Entities.Room DbEntity;
		protected District NullDistrict = new District("");
		protected Street NullStreet = new Street("");

		#endregion

		#region Методы

		public virtual void BeginEdit(Domain.Entities.Room room)
		{
			DistrictList = new ListCollectionView((new[] { NullDistrict }).Concat(_DistrictService.GetAll()).ToList());

			DbEntity = room;
			UpdateValuesFromModel();

			DistrictList.CurrentChanged += (sender, args) =>
			{
				UpdateStreet();
			};

			_ViewsService.OpenRoomDialog(this);
		}

		private void UpdateStreet()
		{
			var district = DistrictList.CurrentItem as District;
			if (district != null)
			{
				StreetList = new ListCollectionView((new[] {NullStreet}).Concat(district.Streets).ToList());
				if (district.Equals(DbEntity.District))
				{
					StreetList.MoveCurrentTo(DbEntity.Street);
				}
			}
		}

		protected void UpdateValuesFromModel()
		{
			Rooms = DbEntity.RoomCount ?? 0;
			DistrictList.MoveCurrentTo(DbEntity.District);

			if (DbEntity.District != null)
			{
				StreetList = new ListCollectionView((new[] {NullStreet}).Concat(DbEntity.District.Streets).ToList());
				StreetList.MoveCurrentTo(DbEntity.Street);
			}
			HouseNumber = DbEntity.TerritorialNumber;
			Id = DbEntity.Id;
		}

		protected void UpdateModelFromValues()
		{
			DbEntity.RoomCount = Rooms;
			DbEntity.District = ResolveDistrict(DistrictList.CurrentItem as District);
			DbEntity.Street = ResolveStreet(StreetList.CurrentItem as Street);
			DbEntity.TerritorialNumber = HouseNumber;
		}

		public void SaveToDatabase()
		{
			_RoomService.Save(DbEntity);
			UpdateValuesFromModel();
		}

		private District ResolveDistrict(District district)
		{
			return district.Equals(NullDistrict) ? null : district;
		}

		private Street ResolveStreet(Street street)
		{
			return street.Equals(NullStreet) ? null : street;
		}

        

        public void OnEditEnded(EditEndedMode mode, Domain.Entities.Room room)
        {
            Action<EditEndedMode, Domain.Entities.Room> handler = EditEnded;
            if (handler != null) handler(mode, room);
        }

		#endregion

		#region Команды

		public DelegateCommand OkCommand { get; protected set; }
		public DelegateCommand CancelCommand { get; protected set; }

		#endregion

		#region Методы проверки команд

		protected bool CanOk()
		{
			return true;
		}

		protected bool CanCancel()
		{
			return true;
		}

		#endregion

		#region Перегрузки


		#region IDataErrorInfo

		public string this[string propertyName]
		{
			get
			{
				if (propertyName == PropertySupport.ExtractPropertyName(() => Rooms))
				{
					if (Rooms < 1)
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
						RoomCount = Rooms,
						District = ResolveDistrict(DistrictList.CurrentItem as District),
						Street = ResolveStreet(StreetList.CurrentItem as Street),
						TerritorialNumber = HouseNumber
					}, Id);
				return validation.IsValid ? null : validation.GetReasons();
			}
		}

		

		#endregion

		#endregion

		public virtual void EndEdisdst()
		{
			var error = Error;

			if (error == null)
			{
				UpdateModelFromValues();
				SaveToDatabase();
			}
			else
			{
				UpdateValuesFromModel();
				if (_MessageService != null)
				{
					_MessageService.ShowMessage(error, "Ошибка", image: MessageBoxImage.Error);
				}
			}
		}

		
	}
}