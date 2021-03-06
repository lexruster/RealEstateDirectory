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
			var table = new ExportTable("��������� �������")
				{
					Headers = new List<Header>
						{
							new Header("�����"),
							new Header("�����"),
							new Header("�������"),
							new Header("������."),
							new Header("�����������"),
							new Header("�������"),
							new Header("���� �.�."),
							new Header("�.", 100),
							new Header("�������"),
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
						plot.HasVideo ? "�" : "",
						plot.Realtor == null ? "" : plot.Realtor.Phone,
						
					};
				table.Data.Add(row);
			}

			return table;
		}
	}
}