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

namespace RealEstateDirectory.MainFormTabs.House
{
	[NotifyForAll]
	public class HouseListViewModel : RealEstateListViewModel<Domain.Entities.House>
	{
		public HouseListViewModel(IHouseService service, IMessageService messageService,
		                          IDistrictService districtService, IRealtorService realtorService,
		                          IOwnershipService ownershipService, IDealVariantService dealVariantService,
		                          IExcelService excelService, IWordService wordService,
		                          IServiceLocator serviceLocator)
			: base(service, messageService, districtService, realtorService, ownershipService, dealVariantService, excelService,
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

		public override ExportTable GetExportedTable(bool forAll = false)
		{
			var table = new ExportTable("Дома")
				{
					Headers = new List<string>
						{
							"Район",
							"Адрес",
							"Площадь участка",
							"Площадь дома",
							"Этажей",
							"Материал",
							"Канализация",
							"Вода",
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
				var house = item as HouseViewModel;
				var row = new List<string>
					{
						GetBaseDictionaryName(house.District),
						house.Address,
						house.PlotSquareString,
						house.HouseSquareString,
						house.TotalFloor.ToString(),
						GetBaseDictionaryName(house.Material),
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