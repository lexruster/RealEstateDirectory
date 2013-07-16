using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Misc.AvitoProvider;
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
                            new Header("Вид помещения"),
                            new Header("Этаж"),
                            new Header("Материал"),
                            new Header("Площадь"),
                            new Header("Комментарий", 500),
                            new Header("Цена т.р.", 100),
                            new Header("В.", 100),
                            new Header("Риэлтор", 100),
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
                        GetBaseDictionaryName(residence.Destination),
                        residence.FloorString,
                        GetBaseDictionaryName(residence.Material),
                        residence.TotalSquareString,
                        residence.Description,
                        residence.PriceString,
                        residence.HasVideo ? "В" : "",
                        residence.Realtor == null ? "" : residence.Realtor.Phone,
                    };
                table.Data.Add(row);
            }

            return table;
        }

        public override AdsAD GetAd(RealEstateViewModel<Domain.Entities.Residence> item)
        {
            var estate = item as ResidenceViewModel;
            var ad = new AdsAD
                {
                    Square = estate.TotalSquare ?? 0,
                    SquareSpecified = true,

                    ObjectType = "Другое",
                    Category = Avito.Residence,

                };

            SetCommon(ad, estate);

            return ad;
        }
    }
}