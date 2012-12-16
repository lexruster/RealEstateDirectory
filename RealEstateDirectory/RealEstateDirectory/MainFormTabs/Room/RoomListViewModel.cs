using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Misc.Miscellaneous;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
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
		                         IConditionService conditionService,
		                         IExcelService excelService, IWordService wordService,
		                         IServiceLocator serviceLocator)
			: base(
				service, messageService, districtService, realtorService, ownershipService, dealVariantService, conditionService,
				excelService,
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
			entities = RoomCount.HasValue
				           ? entities.Where(room => room.RoomCount == RoomCount.Value)
				           : entities;

			if (FilterTerrace != null && !Equals(FilterTerrace, _AllTerrace))
			{
				entities = entities.Where(room => room.Terrace != null && room.Terrace.Name.ToLower() != "Нет".ToLower());
			}

			return entities;
		}

		protected override void LoadChildFiltersData()
		{
			RoomCount = null;
		}

		public override ExportTable GetExportedTable(ExportMode mode)
		{
			var table = new ExportTable("Комнаты")
				{
					Headers = new List<Header>
						{
							new Header("К", 30),
							new Header("Район", 60),
							new Header("Адрес", 100),
							new Header("Этаж", 150),
							new Header("В/Н", 200),
							new Header("План.", 300),
							new Header("Площадь", 400),
							new Header("Мат.", 500),
							new Header("Балк.", 700),
							new Header("Сост.", 100),
							new Header("Комментарий", 500),
							new Header("Вариант", 100),
							new Header("Цена т.р.", 100),
							new Header("В.", 100),
							new Header("Риэлтор", 100)
						}
				};
			var collection = GetCollectionForExport(mode);
			foreach (var item in collection)
			{
				var room = item as RoomViewModel;
				var row = new List<string>
					{
						room.RoomString,
						GetBaseDictionaryName(room.District),
						room.Address,
						room.FloorString,
						GetBaseDictionaryName(room.FloorLevel),

						GetBaseDictionaryName(room.Layout),
						room.TotalSquareString,
						GetBaseDictionaryName(room.Material),
						GetBaseDictionaryName(room.Terrace),
						GetBaseDictionaryName(room.Condition),
						room.Description,
						GetBaseDictionaryName(room.DealVariant),
						room.PriceString,
						room.HasVideo ? "В" : "",
						room.Realtor == null ? "" : room.Realtor.Phone
					};
				table.Data.Add(row);
			}

			return table;
		}
	}
}