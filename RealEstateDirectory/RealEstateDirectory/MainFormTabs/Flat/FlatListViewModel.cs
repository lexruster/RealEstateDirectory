using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using Misc.Miscellaneous;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
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

		public override ExportTable GetExportedTable(ExportMode mode)
		{
			var table = new ExportTable("Квартиры")
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
							new Header("Сан.узел", 100),
							new Header("Сост.", 100),
							new Header("Комментарий", 500),
							new Header("Вариант", 100),
							new Header("Цена т.р.", 100),
							new Header("В.", 100),
							new Header("Риэлтор", 100),
						}
				};

			var collection = GetCollectionForExport(mode);
			
			foreach (var item in collection)
			{
				var flat = item as FlatViewModel;
				var row = new List<string>
					{
						flat.TotalRoomCount.ToString(),
						GetBaseDictionaryName(flat.District),
						flat.Address,
						flat.FloorString,
						GetBaseDictionaryName(flat.FloorLevel),
						GetBaseDictionaryName(flat.Layout),
						flat.SquareString,
						GetBaseDictionaryName(flat.Material),
						GetBaseDictionaryName(flat.Terrace),
						GetBaseDictionaryName(flat.ToiletType),
						GetBaseDictionaryName(flat.Condition),
						flat.Description,
						GetBaseDictionaryName(flat.DealVariant),
						flat.PriceString,
						flat.HasVideo ? "В" : "",
						flat.Realtor == null ? "" : flat.Realtor.Phone

					};
				table.Data.Add(row);
			}

			return table;
		}
	}
}