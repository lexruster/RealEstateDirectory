using System.Diagnostics;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.ApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Infrastructure.NHibernate.DbSession;

namespace RealEstateDirectory.Shell
{
    [NotifyForAll]
    public class ShellViewModel : NotificationObject
    {
        private static IDealVariantService dealVariantService;
        private static IDistrictService districtService;
        private static IFloorLevelService floorLevelService;
        private static ILayoutService layoutService;
        private static IMaterialService materialService;
        private static IOwnershipService ownershipService;
        private static IRealtorService realtorService;
        private static ISewageService sewageService;
        private static IStreetService streetService;
        private static ITerraceService terraceService;
        private static IToiletTypeService toiletTypeService;
        private static IWaterSupplyService waterSupplyService;
        private static IFlatService flatService;
        private static IHouseService houseService;
        private static IPlotService plotService;
        private static IResidenceService residenceService;
        private static IRoomService roomService;

        private static DbSession Session;

        public ShellViewModel(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        #region Infrastructure

        private readonly IServiceLocator _serviceLocator;

        public void Initialize()
        {
            TestMethod();
        }

        public void TestMethod()
        {

            Session = new DbSession(_serviceLocator.GetInstance<IPersistenceContext>());

            #region Создать сервисы

            CreateServices();

            #endregion

            #region Удалить мусор

            DeleteAllFlat();
            DeleteAllDict();

            #endregion

            #region Создать справочники
            
            AddDicitonaryValue();

            var distrF = districtService.GetAll().First();
            Debug.Assert(distrF.Streets.Count==2);
            Debug.Assert(!districtService.IsPossibilityToDelete(distrF));
            Debug.Assert(!string.IsNullOrEmpty(distrF.Streets.First().Name));
            
            #endregion

            #region Проверка справочников

            ValidateDictToCount(2);
            Debug.Assert(streetService.GetAll().Count() == 3);

            #endregion

            DeleteAllDict();

            ValidateDictToCount(0);
            Debug.Assert(streetService.GetAll().Count() == 0);
            AddDicitonaryValue(true);
            ValidateFlatToCount(1);
            DeleteAllDict();
            ValidateDictToCount(1);
            Debug.Assert(streetService.GetAll().Count() == 1);
            DeleteAllFlat();
            DeleteAllDict();
            ValidateDictToCount(0);
            Debug.Assert(streetService.GetAll().Count() == 0);
            ValidateFlatToCount(0);
            Session.Dispose();
        }

        private static void ValidateDictToCount(int count)
        {
            Debug.Assert(dealVariantService.GetAll().Count() == count);
            Debug.Assert(districtService.GetAll().Count() == count);
            Debug.Assert(floorLevelService.GetAll().Count() == count);
            Debug.Assert(layoutService.GetAll().Count() == count);
            Debug.Assert(materialService.GetAll().Count() == count);
            Debug.Assert(ownershipService.GetAll().Count() == count);
            Debug.Assert(realtorService.GetAll().Count() == count);
            Debug.Assert(sewageService.GetAll().Count() == count);
            
            Debug.Assert(terraceService.GetAll().Count() == count);
            Debug.Assert(toiletTypeService.GetAll().Count() == count);
            Debug.Assert(waterSupplyService.GetAll().Count() == count);
        }

        private static void ValidateFlatToCount(int count)
        {
            Debug.Assert(roomService.GetAll().Count() == count);
            Debug.Assert(flatService.GetAll().Count() == count);
            Debug.Assert(residenceService.GetAll().Count() == count);
            Debug.Assert(houseService.GetAll().Count() == count);
            var plots=plotService.GetAll();
            Debug.Assert(plots.Count() == count);
        }

        private void CreateServices()
        {
            dealVariantService = _serviceLocator.GetInstance<DealVariantService>();
            districtService = _serviceLocator.GetInstance<IDistrictService>();
            floorLevelService = _serviceLocator.GetInstance<IFloorLevelService>();
            layoutService = _serviceLocator.GetInstance<ILayoutService>();
            materialService = _serviceLocator.GetInstance<IMaterialService>();
            ownershipService = _serviceLocator.GetInstance<IOwnershipService>();
            realtorService = _serviceLocator.GetInstance<IRealtorService>();
            sewageService = _serviceLocator.GetInstance<ISewageService>();
            streetService = _serviceLocator.GetInstance<IStreetService>();
            terraceService = _serviceLocator.GetInstance<ITerraceService>();
            toiletTypeService = _serviceLocator.GetInstance<IToiletTypeService>();
            waterSupplyService = _serviceLocator.GetInstance<IWaterSupplyService>();
            flatService = _serviceLocator.GetInstance<IFlatService>();
            houseService = _serviceLocator.GetInstance<IHouseService>();
            plotService = _serviceLocator.GetInstance<IPlotService>();
            residenceService = _serviceLocator.GetInstance<IResidenceService>();
            roomService = _serviceLocator.GetInstance<IRoomService>();
        }

        private static void AddDicitonaryValue(bool addRealEstate = false)
        {
            var dealVariantEntityNew = new DealVariant("dealVariant1");
            if (dealVariantService.IsValid(dealVariantEntityNew))
            {
                dealVariantService.Save(dealVariantEntityNew);
            }

            var dealVariantEntityNew2 = new DealVariant("dealVariant2");
            if (dealVariantService.IsValid(dealVariantEntityNew2))
            {
                dealVariantService.Save(dealVariantEntityNew2);
            }
            var dealVariantEntityNew3 = new DealVariant("dealVariant2");
            if (dealVariantService.IsValid(dealVariantEntityNew3))
            {
                dealVariantService.Save(dealVariantEntityNew3);
            }

            var districtEntityNew = new District("district1");
            if (districtService.IsValid(districtEntityNew))
            {
                districtService.Save(districtEntityNew);
            }

            var districtEntityNew2 = new District("district2");
            if (districtService.IsValid(districtEntityNew2))
            {
                districtService.Save(districtEntityNew2);
            }
            var districtEntityNew3 = new District("district2");
            if (districtService.IsValid(districtEntityNew3))
            {
                districtService.Save(districtEntityNew3);
            }

            var floorLevelEntityNew = new FloorLevel("floorLevel1");
            if (floorLevelService.IsValid(floorLevelEntityNew))
            {
                floorLevelService.Save(floorLevelEntityNew);
            }

            var floorLevelEntityNew2 = new FloorLevel("floorLevel2");
            if (floorLevelService.IsValid(floorLevelEntityNew2))
            {
                floorLevelService.Save(floorLevelEntityNew2);
            }
            var floorLevelEntityNew3 = new FloorLevel("floorLevel2");
            if (floorLevelService.IsValid(floorLevelEntityNew3))
            {
                floorLevelService.Save(floorLevelEntityNew3);
            }

            var layoutEntityNew = new Layout("layout1");
            if (layoutService.IsValid(layoutEntityNew))
            {
                layoutService.Save(layoutEntityNew);
            }

            var layoutEntityNew2 = new Layout("layout2");
            if (layoutService.IsValid(layoutEntityNew2))
            {
                layoutService.Save(layoutEntityNew2);
            }
            var layoutEntityNew3 = new Layout("layout2");
            if (layoutService.IsValid(layoutEntityNew3))
            {
                layoutService.Save(layoutEntityNew3);
            }

            var materialEntityNew = new Material("material1");
            if (materialService.IsValid(materialEntityNew))
            {
                materialService.Save(materialEntityNew);
            }

            var materialEntityNew2 = new Material("material2");
            if (materialService.IsValid(materialEntityNew2))
            {
                materialService.Save(materialEntityNew2);
            }
            var materialEntityNew3 = new Material("material2");
            if (materialService.IsValid(materialEntityNew3))
            {
                materialService.Save(materialEntityNew3);
            }

            var ownershipEntityNew = new Ownership("ownership1");
            if (ownershipService.IsValid(ownershipEntityNew))
            {
                ownershipService.Save(ownershipEntityNew);
            }

            var ownershipEntityNew2 = new Ownership("ownership2");
            if (ownershipService.IsValid(ownershipEntityNew2))
            {
                ownershipService.Save(ownershipEntityNew2);
            }
            var ownershipEntityNew3 = new Ownership("ownership2");
            if (ownershipService.IsValid(ownershipEntityNew3))
            {
                ownershipService.Save(ownershipEntityNew3);
            }

            var realtorEntityNew = new Realtor("realtor1", "phone");
            if (realtorService.IsValid(realtorEntityNew))
            {
                realtorService.Save(realtorEntityNew);
            }

            var realtorEntityNew2 = new Realtor("realtor2", "phone");
            if (realtorService.IsValid(realtorEntityNew2))
            {
                realtorService.Save(realtorEntityNew2);
            }
            var realtorEntityNew3 = new Realtor("realtor2", "phone");
            if (realtorService.IsValid(realtorEntityNew3))
            {
                realtorService.Save(realtorEntityNew3);
            }

            var sewageEntityNew = new Sewage("sewage1");
            if (sewageService.IsValid(sewageEntityNew))
            {
                sewageService.Save(sewageEntityNew);
            }

            var sewageEntityNew2 = new Sewage("sewage2");
            if (sewageService.IsValid(sewageEntityNew2))
            {
                sewageService.Save(sewageEntityNew2);
            }
            var sewageEntityNew3 = new Sewage("sewage2");
            if (sewageService.IsValid(sewageEntityNew3))
            {
                sewageService.Save(sewageEntityNew3);
            }

            var streetEntityNew = new Street("street1");
            if (streetService.IsValid(streetEntityNew))
            {
                districtEntityNew.AddStreet(streetEntityNew);
                districtService.Save(districtEntityNew);
            }

            var streetEntityNew1 = new Street("street11");
            if (streetService.IsValid(streetEntityNew1))
            {
                districtEntityNew.AddStreet(streetEntityNew1);
                districtService.Save(districtEntityNew);
            }

            var streetEntityNew2 = new Street("street2");
            if (streetService.IsValid(streetEntityNew2))
            {
                districtEntityNew2.AddStreet(streetEntityNew2);
                streetService.Save(streetEntityNew);
            }

            var streetEntityNew3 = new Street("street2");
            if (streetService.IsValid(streetEntityNew3))
            {
                Debug.Assert(false,"Не должан была пройти валидация.");
            }

            var terraceEntityNew = new Terrace("terrace1");
            if (terraceService.IsValid(terraceEntityNew))
            {
                terraceService.Save(terraceEntityNew);
            }

            var terraceEntityNew2 = new Terrace("terrace2");
            if (terraceService.IsValid(terraceEntityNew2))
            {
                terraceService.Save(terraceEntityNew2);
            }
            var terraceEntityNew3 = new Terrace("terrace2");
            if (terraceService.IsValid(terraceEntityNew3))
            {
                terraceService.Save(terraceEntityNew3);
            }

            var toiletTypeEntityNew = new ToiletType("toiletType1");
            if (toiletTypeService.IsValid(toiletTypeEntityNew))
            {
                toiletTypeService.Save(toiletTypeEntityNew);
            }

            var toiletTypeEntityNew2 = new ToiletType("toiletType2");
            if (toiletTypeService.IsValid(toiletTypeEntityNew2))
            {
                toiletTypeService.Save(toiletTypeEntityNew2);
            }
            var toiletTypeEntityNew3 = new ToiletType("toiletType2");
            if (toiletTypeService.IsValid(toiletTypeEntityNew3))
            {
                toiletTypeService.Save(toiletTypeEntityNew3);
            }

            var waterSupplyEntityNew = new WaterSupply("waterSupply1");
            if (waterSupplyService.IsValid(waterSupplyEntityNew))
            {
                waterSupplyService.Save(waterSupplyEntityNew);
            }

            var waterSupplyEntityNew2 = new WaterSupply("waterSupply2");
            if (waterSupplyService.IsValid(waterSupplyEntityNew2))
            {
                waterSupplyService.Save(waterSupplyEntityNew2);
            }
            var waterSupplyEntityNew3 = new WaterSupply("waterSupply2");
            if (waterSupplyService.IsValid(waterSupplyEntityNew3))
            {
                waterSupplyService.Save(waterSupplyEntityNew3);
            }
            if (!addRealEstate) return;

            var flat = new Flat
                           {
                               DealVariant = dealVariantEntityNew,
                               Description = "decr",
                               District = districtEntityNew,
                               Floor = 3,
                               FloorLevel = floorLevelEntityNew,
                               HasVideo = true,
                               KitchenSquare = 32,
                               Layout = layoutEntityNew,
                               Material = materialEntityNew,
                               Ownership = ownershipEntityNew,
                               Price = 32323,
                               Realtor = realtorEntityNew,
                               ResidentialSquare = 323,
                               Street = streetEntityNew,
                               SubmitToDomino = false,
                               SubmitToVDV = true,
                               Terrace = terraceEntityNew,
                               TerritorialNumber = "32a",
                               ToiletType = toiletTypeEntityNew,
                               TotalFloor = 32,
                               TotalRoomCount = 5,
                               TotalSquare = 323
                           };

            if (flatService.IsValid(flat))
            {
                flatService.Save(flat);
            }

            var room = new Room
                           {
                               DealVariant = dealVariantEntityNew,
                               Description = "decr",
                               District = districtEntityNew,
                               Floor = 3,
                               FloorLevel = floorLevelEntityNew,
                               HasVideo = true,
                               Layout = layoutEntityNew,
                               Material = materialEntityNew,
                               Ownership = ownershipEntityNew,
                               Price = 32323,
                               Realtor = realtorEntityNew,
                               Street = streetEntityNew,
                               SubmitToDomino = false,
                               SubmitToVDV = true,
                               Terrace = terraceEntityNew,
                               TerritorialNumber = "32a",
                               TotalFloor = 32,
                               TotalRoomCount = 5,
                               TotalSquare = 323,
                               RoomCount = 2
                           };

            if (roomService.IsValid(room))
            {
                roomService.Save(room);
            }

            var residence = new Residence
                                {
                                    DealVariant = dealVariantEntityNew,
                                    Description = "decr",
                                    District = districtEntityNew,
                                    Floor = 3,
                                    HasVideo = true,
                                    Material = materialEntityNew,
                                    Ownership = ownershipEntityNew,
                                    Price = 32323,
                                    Realtor = realtorEntityNew,
                                    Street = streetEntityNew,
                                    SubmitToDomino = false,
                                    SubmitToVDV = true,
                                    TerritorialNumber = "32a",
                                    TotalFloor = 32,
                                    TotalSquare = 323
                                };

            if (residenceService.IsValid(residence))
            {
                residenceService.Save(residence);
            }

            var house = new House
                            {
                                DealVariant = dealVariantEntityNew,
                                Description = "decr",
                                District = districtEntityNew,
                                HasVideo = true,
                                Material = materialEntityNew,
                                Ownership = ownershipEntityNew,
                                Price = 32323,
                                Realtor = realtorEntityNew,
                                Street = streetEntityNew,
                                SubmitToDomino = false,
                                SubmitToVDV = true,
                                TerritorialNumber = "32a",
                                TotalFloor = 32,
                                HasBathhouse = true,
                                HasGarage = true,
                                HasGas = true,
                                HouseSquare = 342,
                                PlotSquare = 123,
                                Sewage = sewageEntityNew,
                                WaterSupply = waterSupplyEntityNew
                            };

            if (houseService.IsValid(house))
            {
                houseService.Save(house);

            }
            var plot = new Plot
                           {
                               DealVariant = dealVariantEntityNew,
                               Description = "decr",
                               District = districtEntityNew,
                               HasVideo = true,
                               Ownership = ownershipEntityNew,
                               Price = 32323,
                               Realtor = realtorEntityNew,
                               Street = streetEntityNew,
                               SubmitToDomino = false,
                               SubmitToVDV = true,
                               TerritorialNumber = "32a",
                               PlotSquare = 123
                           };

            if (plotService.IsValid(plot))
            {
                plotService.Save(plot);
            }

        }

        private static void DeleteAllFlat()
        {
            var flatServiceEntities = flatService.GetAll();
            foreach (var flatServiceEntity in flatServiceEntities)
            {
                if (flatService.IsPossibilityToDelete(flatServiceEntity))
                {
                    flatService.Delete(flatServiceEntity);
                }
            }
            var houseServiceEntities = houseService.GetAll();
            foreach (var houseServiceEntity in houseServiceEntities)
            {
                if (houseService.IsPossibilityToDelete(houseServiceEntity))
                {
                    houseService.Delete(houseServiceEntity);
                }
            }
            var plotServiceEntities = plotService.GetAll();
            foreach (var plotServiceEntity in plotServiceEntities)
            {
                if (plotService.IsPossibilityToDelete(plotServiceEntity))
                {
                    plotService.Delete(plotServiceEntity);
                }
            }
            var residenceServiceEntities = residenceService.GetAll();
            foreach (var residenceServiceEntity in residenceServiceEntities)
            {
                if (residenceService.IsPossibilityToDelete(residenceServiceEntity))
                {
                    residenceService.Delete(residenceServiceEntity);
                }
            }
            var roomServiceEntities = roomService.GetAll();
            foreach (var roomServiceEntity in roomServiceEntities)
            {
                if (roomService.IsPossibilityToDelete(roomServiceEntity))
                {
                    roomService.Delete(roomServiceEntity);
                }
            }
        }

        private static void DeleteAllDict()
        {

            var dealVariantServiceEntities = dealVariantService.GetAll();
            foreach (var dealVariantServiceEntity in dealVariantServiceEntities)
            {
                if (dealVariantService.IsPossibilityToDelete(dealVariantServiceEntity))
                {
                    dealVariantService.Delete(dealVariantServiceEntity);
                }
            }

            var streetServiceEntities = streetService.GetAll();
            foreach (var streetServiceEntity in streetServiceEntities)
            {
                if (streetService.IsPossibilityToDelete(streetServiceEntity))
                {
                    streetService.Delete(streetServiceEntity);
                }
            }

            var districtServiceEntities = districtService.GetAll();
            foreach (var districtServiceEntity in districtServiceEntities)
            {
                if (districtService.IsPossibilityToDelete(districtServiceEntity))
                {
                    districtService.Delete(districtServiceEntity);
                }
            }
            var floorLevelServiceEntities = floorLevelService.GetAll();
            foreach (var floorLevelServiceEntity in floorLevelServiceEntities)
            {
                if (floorLevelService.IsPossibilityToDelete(floorLevelServiceEntity))
                {
                    floorLevelService.Delete(floorLevelServiceEntity);
                }
            }
            var layoutServiceEntities = layoutService.GetAll();
            foreach (var layoutServiceEntity in layoutServiceEntities)
            {
                if (layoutService.IsPossibilityToDelete(layoutServiceEntity))
                {
                    layoutService.Delete(layoutServiceEntity);
                }
            }
            var materialServiceEntities = materialService.GetAll();
            foreach (var materialServiceEntity in materialServiceEntities)
            {
                if (materialService.IsPossibilityToDelete(materialServiceEntity))
                {
                    materialService.Delete(materialServiceEntity);
                }
            }
            var ownershipServiceEntities = ownershipService.GetAll();
            foreach (var ownershipServiceEntity in ownershipServiceEntities)
            {
                if (ownershipService.IsPossibilityToDelete(ownershipServiceEntity))
                {
                    ownershipService.Delete(ownershipServiceEntity);
                }
            }
            var realtorServiceEntities = realtorService.GetAll();
            foreach (var realtorServiceEntity in realtorServiceEntities)
            {
                if (realtorService.IsPossibilityToDelete(realtorServiceEntity))
                {
                    realtorService.Delete(realtorServiceEntity);
                }
            }
            var sewageServiceEntities = sewageService.GetAll();
            foreach (var sewageServiceEntity in sewageServiceEntities)
            {
                if (sewageService.IsPossibilityToDelete(sewageServiceEntity))
                {
                    sewageService.Delete(sewageServiceEntity);
                }
            }
            
            
            var terraceServiceEntities = terraceService.GetAll();
            foreach (var terraceServiceEntity in terraceServiceEntities)
            {
                if (terraceService.IsPossibilityToDelete(terraceServiceEntity))
                {
                    terraceService.Delete(terraceServiceEntity);
                }
            }
            var toiletTypeServiceEntities = toiletTypeService.GetAll();
            foreach (var toiletTypeServiceEntity in toiletTypeServiceEntities)
            {
                if (toiletTypeService.IsPossibilityToDelete(toiletTypeServiceEntity))
                {
                    toiletTypeService.Delete(toiletTypeServiceEntity);
                }
            }
            var waterSupplyServiceEntities = waterSupplyService.GetAll();
            foreach (var waterSupplyServiceEntity in waterSupplyServiceEntities)
            {
                if (waterSupplyService.IsPossibilityToDelete(waterSupplyServiceEntity))
                {
                    waterSupplyService.Delete(waterSupplyServiceEntity);
                }
            }
        }

        #endregion
    }
}