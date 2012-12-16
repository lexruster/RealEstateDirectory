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
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.MainFormTabs.Common;
using RealEstateDirectory.Services;
using RealEstateDirectory.Services.Export;

namespace RealEstateDirectory.MainFormTabs.Flat
{
	[NotifyForAll]
	public class FlatListViewModel : RealEstateListViewModel<Domain.Entities.Flat>
	{
		public FlatListViewModel(IFlatService service, IMessageService messageService,
		                         IDistrictService districtService, IRealtorService realtorService,
		                         IOwnershipService ownershipService, IDealVariantService dealVariantService, IConditionService conditionService,
		                         IExcelService excelService, IWordService wordService,
		                         IServiceLocator serviceLocator)
			: base(
				service, messageService, districtService, realtorService, ownershipService, dealVariantService, conditionService, excelService,
				wordService, serviceLocator)
		{
		}

		public int? TotalRoomCount { get; set; }

		protected override RealEstateViewModel<Domain.Entities.Flat> GetViewEntityViewModel()
		{
			return _ServiceLocator.GetInstance<FlatViewModel>();
		}

		protected override RealEstateEditViewModel<Domain.Entities.Flat> GetEditEntityViewModel()
		{
			return _ServiceLocator.GetInstance<FlatEditViewModel>();
		}

		protected override IEnumerable<Domain.Entities.Flat> UpdateChildFilterData(IEnumerable<Domain.Entities.Flat> entities)
		{
			entities = TotalRoomCount.HasValue
				           ? entities.Where(room => room.TotalRoomCount == TotalRoomCount.Value)
				           : entities;

			if (FilterTerrace != null && !Equals(FilterTerrace, _AllTerrace))
			{
				entities = entities.Where(room => room.Terrace != null && room.Terrace.Name.ToLower() != "Нет".ToLower());
			}

			return entities;
		}

		protected override void LoadChildFiltersData()
		{
			TotalRoomCount = null;
		}

		public override ExportTable GetExportedTable(bool forAll = false)
		{
			var table = new ExportTable("Квартиры")
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
							"Состояние",
							"Сан. узел",
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
				var flat = item as FlatViewModel;
				var row = new List<string>
					{
						GetBaseDictionaryName(flat.District),
						flat.Address,
						flat.TotalRoomCount.ToString(),
						flat.FloorString,
						GetBaseDictionaryName(flat.Layout),
						flat.SquareString,
						GetBaseDictionaryName(flat.Terrace),
						GetBaseDictionaryName(flat.Material),
						GetBaseDictionaryName(flat.Condition),
						GetBaseDictionaryName(flat.ToiletType),
						GetBaseDictionaryName(flat.FloorLevel),
						GetBaseDictionaryName(flat.DealVariant),
						GetBaseDictionaryName(flat.Ownership),
						flat.Description,
						flat.Realtor == null ? "" : flat.Realtor.Phone,
						flat.PriceString
					};
				table.Data.Add(row);
			}

			return table;
		}
	}
}