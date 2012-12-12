using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Misc.Miscellaneous;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.MainFormTabs.Common;
using RealEstateDirectory.Services;
using RealEstateDirectory.Services.Export;

namespace RealEstateDirectory.MainFormTabs.Room
{
	[NotifyForAll]
	public class RoomListViewModel : RealEstateListViewModel<Domain.Entities.Room>
	{
		public RoomListViewModel(IRoomService service, IMessageService messageService,
		                         IDistrictService districtService, IRealtorService realtorService,
		                         IOwnershipService ownershipService, IDealVariantService dealVariantService,
		                         IExcelService excelService, IWordService wordService,
		                         IServiceLocator serviceLocator)
			: base(service, messageService, districtService, realtorService, ownershipService, dealVariantService, excelService,
			       wordService, serviceLocator)
		{
		}

		public int? RoomCount { get; set; }

		protected override RealEstateViewModel<Domain.Entities.Room> GetViewEntityViewModel()
		{
			return _ServiceLocator.GetInstance<RoomViewModel>();
		}

		protected override RealEstateEditViewModel<Domain.Entities.Room> GetEditEntityViewModel()
		{
			return _ServiceLocator.GetInstance<RoomEditViewModel>();
		}

		protected override IEnumerable<Domain.Entities.Room> UpdateChildFilterData(IEnumerable<Domain.Entities.Room> entities)
		{
			return RoomCount.HasValue ? entities.Where(room => room.RoomCount == RoomCount.Value) : entities;
		}

		protected override void LoadChildFiltersData()
		{
			RoomCount = null;
		}

		public override ExportTable GetExportedTable(bool forAll = false)
		{
			var table = new ExportTable("Комнаты")
				{
					Headers = new List<string>
						{
							"Район",
							"Адрес",
							"Комнат",
							"Этаж",
							"Планировка",
							"Площадь",
							"Балкон",
							"Материал",
							"Потолки",
							"Вариант",
							"Собственность",
							"Комментарий",
							"Риэлтор",
							"Цена т.р."
						}
				};
			var collection = forAll ? _RealEstateService.GetAll().Select(CreateNewViewModel).ToArray() : Entities.ToArray();
			foreach (var item in collection)
			{
				var room = item as RoomViewModel;
				var row = new List<string>
					{
						GetBaseDictionaryName(room.District),
						room.Address,
						room.RoomString,
						room.FloorString,
						GetBaseDictionaryName(room.Layout),
						room.TotalSquareString,
						GetBaseDictionaryName(room.Terrace),
						GetBaseDictionaryName(room.Material),
						GetBaseDictionaryName(room.FloorLevel),
						GetBaseDictionaryName(room.DealVariant),
						GetBaseDictionaryName(room.Ownership),
						room.Description,
						room.Realtor == null ? "" : room.Realtor.Phone,
						room.PriceString
					};
				table.Data.Add(row);
			}

			return table;
		}
	}
}