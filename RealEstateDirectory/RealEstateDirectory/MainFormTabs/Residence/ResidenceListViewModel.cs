using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Misc.Miscellaneous;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.MainFormTabs.Common;
using RealEstateDirectory.Services;
using RealEstateDirectory.Services.Export;

namespace RealEstateDirectory.MainFormTabs.Residence
{
	public class ResidenceListViewModel : RealEstateListViewModel<Domain.Entities.Residence>
	{
		public ResidenceListViewModel(IResidenceService residenceService, IMessageService messageService,
		                              IDistrictService districtService, IRealtorService realtorService,
		                              IOwnershipService ownershipService, IDealVariantService dealVariantService,
		                              IConditionService conditionService,
		                              IExcelService excelService, IWordService wordService,
		                              IServiceLocator serviceLocator)
			: base(
				residenceService, messageService, districtService, realtorService, ownershipService, dealVariantService,
				conditionService,
				excelService, wordService, serviceLocator)
		{

		}

		protected override RealEstateViewModel<Domain.Entities.Residence> GetViewEntityViewModel()
		{
			return _ServiceLocator.GetInstance<ResidenceViewModel>();
		}

		protected override RealEstateEditViewModel<Domain.Entities.Residence> GetEditEntityViewModel()
		{
			return _ServiceLocator.GetInstance<ResidenceEditViewModel>();
		}

		protected override IEnumerable<Domain.Entities.Residence> UpdateChildFilterData(
			IEnumerable<Domain.Entities.Residence> entities)
		{
			return entities;
		}

		protected override void LoadChildFiltersData()
		{

		}

		public override ExportTable GetExportedTable(ExportMode mode)
		{
			var table = new ExportTable("Помещения")
				{
					Headers = new List<Header>
						{
							new Header("Район"),
							new Header("Адрес"),
							new Header("Этаж"),
							new Header("Площадь"),
							new Header("Материал"),
							new Header("Назначение"),
							new Header("Состояние"),
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
				var residence = item as ResidenceViewModel;
				var row = new List<string>
					{
						GetBaseDictionaryName(residence.District),
						residence.Address,
						residence.FloorString,
						residence.TotalSquareString,
						GetBaseDictionaryName(residence.Material),
						GetBaseDictionaryName(residence.Destination),
						GetBaseDictionaryName(residence.Condition),
						GetBaseDictionaryName(residence.DealVariant),
						GetBaseDictionaryName(residence.Ownership),
						residence.Description,
						residence.Realtor == null ? "" : residence.Realtor.Phone,
						residence.PriceString
					};
				table.Data.Add(row);
			}

			return table;
		}
	}
}