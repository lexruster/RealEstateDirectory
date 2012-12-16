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

namespace RealEstateDirectory.MainFormTabs.House
{
	[NotifyForAll]
	public class HouseListViewModel : RealEstateListViewModel<Domain.Entities.House>
	{
		public HouseListViewModel(IHouseService service, IMessageService messageService,
		                          IDistrictService districtService, IRealtorService realtorService,
		                          IOwnershipService ownershipService, IDealVariantService dealVariantService, IConditionService conditionService,
		                          IExcelService excelService, IWordService wordService,
		                          IServiceLocator serviceLocator)
			: base(service, messageService, districtService, realtorService, ownershipService, dealVariantService, conditionService, excelService,
			       wordService, serviceLocator)
		{
		}

		public int? RoomCount { get; set; }

		protected override RealEstateViewModel<Domain.Entities.House> GetViewEntityViewModel()
		{
			return _ServiceLocator.GetInstance<HouseViewModel>();
		}

		protected override RealEstateEditViewModel<Domain.Entities.House> GetEditEntityViewModel()
		{
			return _ServiceLocator.GetInstance<HouseEditViewModel>();
		}

		protected override IEnumerable<Domain.Entities.House> UpdateChildFilterData(
			IEnumerable<Domain.Entities.House> entities)
		{
			return entities;
		}

		protected override void LoadChildFiltersData()
		{
		}

		public override ExportTable GetExportedTable(ExportMode mode)
		{
			var table = new ExportTable("Дома")
				{
					Headers = new List<Header>
						{
							new Header("Район"),
							new Header("Адрес"),
							new Header("Площадь участка"),
							new Header("Площадь дома"),
							new Header("Этажей"),
							new Header("Материал"),
							new Header("Состояние"),
							new Header("Канализация"),
							new Header("Вода"),
							new Header("Вариант"),
							new Header("Собственность"),
							new Header("Комментарий"),
							new Header("Риэлтор"),
							new Header("Цена т.р."),
						}
				};

			var collection = GetCollectionForExport(mode);
			foreach (var item in collection)
			{
				var house = item as HouseViewModel;
				var row = new List<string>
					{
						GetBaseDictionaryName(house.District),
						house.Address,
						house.PlotSquareString,
						house.HouseSquareString,
						house.TotalFloor.ToString(),
						GetBaseDictionaryName(house.Material),
						GetBaseDictionaryName(house.Condition),
						GetBaseDictionaryName(house.Sewage),
						GetBaseDictionaryName(house.WaterSupply),
						GetBaseDictionaryName(house.DealVariant),
						GetBaseDictionaryName(house.Ownership),
						house.Description,
						house.Realtor == null ? "" : house.Realtor.Phone,
						house.PriceString
					};
				table.Data.Add(row);
			}

			return table;
		}
	}
}