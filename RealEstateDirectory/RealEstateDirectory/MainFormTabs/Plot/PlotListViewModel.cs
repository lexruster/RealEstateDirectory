using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Misc.AvitoProvider;
using Misc.Miscellaneous;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.MainFormTabs.Common;
using RealEstateDirectory.Services;
using RealEstateDirectory.Services.Export;

namespace RealEstateDirectory.MainFormTabs.Plot
{
	[NotifyForAll]
	public class PlotListViewModel : RealEstateListViewModel<Domain.Entities.Plot>
	{
		public PlotListViewModel(IPlotService service, IMessageService messageService,
		                         IDistrictService districtService, IRealtorService realtorService,
								 IOwnershipService ownershipService, IDealVariantService dealVariantService, IConditionService conditionService,
		                         IExcelService excelService, IWordService wordService,
		                         IServiceLocator serviceLocator)
			: base(service, messageService, districtService, realtorService, ownershipService, dealVariantService, conditionService, excelService,
			       wordService, serviceLocator)
		{
		}

		protected override RealEstateViewModel<Domain.Entities.Plot> GetViewEntityViewModel()
		{
			return _ServiceLocator.GetInstance<PlotViewModel>();
		}

		protected override RealEstateEditViewModel<Domain.Entities.Plot> GetEditEntityViewModel()
		{
			return _ServiceLocator.GetInstance<PlotEditViewModel>();
		}

		protected override IEnumerable<Domain.Entities.Plot> UpdateChildFilterData(IEnumerable<Domain.Entities.Plot> entities)
		{
			return entities;
		}

		protected override void LoadChildFiltersData()
		{
		}

		public override ExportTable GetExportedTable(ExportMode mode)
		{
			var table = new ExportTable("Земельные участки")
				{
					Headers = new List<Header>
						{
							new Header("Район"),
							new Header("Адрес"),
							new Header("Площадь"),
							new Header("Собств."),
							new Header("Комментарий"),
							new Header("Вариант"),
							new Header("Цена т.р."),
							new Header("В.", 100),
							new Header("Риэлтор"),
						}
				};

			var collection = GetCollectionForExport(mode);
			foreach (var item in collection)
			{
				var plot = item as PlotViewModel;
				var row = new List<string>
					{
						GetBaseDictionaryName(plot.District),
						plot.Address,
						plot.PlotSquareString,
						GetBaseDictionaryName(plot.Ownership),
						plot.Description,
						GetBaseDictionaryName(plot.DealVariant),
						plot.PriceString,
						plot.HasVideo ? "В" : "",
						plot.Realtor == null ? "" : plot.Realtor.Phone,
						
					};
				table.Data.Add(row);
			}

			return table;
		}

	    public override AdsAD GetAd(RealEstateViewModel<Domain.Entities.Plot> item)
	    {
            var estate = item as PlotViewModel;
            var ad = new AdsAD
            {
                LandArea = estate.PlotSquare ?? 0,
                LandAreaSpecified = true,

                Category = Avito.Plot,

            };

            SetCommon(ad, estate);

            return ad;
	    }
	}
}