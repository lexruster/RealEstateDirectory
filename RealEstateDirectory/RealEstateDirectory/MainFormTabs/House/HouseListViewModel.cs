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
			var table = new ExportTable("����")
				{
					Headers = new List<Header>
						{
							new Header("�����"),
							new Header("�����"),
							new Header("������", 60),
							new Header("������� �������", 80),
							new Header("������� ����", 80),
							new Header("�����������", 600),
							new Header("�������", 100),
							new Header("���� �.�.", 100),
							new Header("�.", 100),
							new Header("�������", 100),
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
						house.TotalFloor.ToString(),
						house.PlotSquareString,
						house.HouseSquareString,
						house.Description,
						GetBaseDictionaryName(house.DealVariant),
						house.PriceString,
						house.HasVideo ? "�" : "",
						house.Realtor == null ? "" : house.Realtor.Phone,
					};
				table.Data.Add(row);
			}

			return table;
		}

        public override AdsAD GetAd(RealEstateViewModel<Domain.Entities.House> item)
        {
            var estate = item as HouseViewModel;
            var ad = new AdsAD
            {

                Square = estate.HouseSquare ?? 0,
                SquareSpecified = true,

                ObjectType="���",

                LandArea = estate.PlotSquare??0,
                LandAreaSpecified = true,
                Category = Avito.House,

            };

            SetCommon(ad, estate);

            return ad;
        }
	}
}