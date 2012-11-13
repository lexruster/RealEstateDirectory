using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using RealEstateDirectory.Domain.Data.Config;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Infrastructure.Entities;

namespace DataTransfer
{
	internal class Program
	{
		private static Dictionary<Type, List<IdMap>> _idDictionaryMaps;

		private static void Main()
		{
			var config = new Configuration();
			config.Configure("hibernate.cfg.xml");

			Configurator.InitProperties(config);
			Configurator.InitMapping(config);
			var db = new DataClassesDataContext();
			_idDictionaryMaps = new Dictionary<Type, List<IdMap>>();
			bool succes = true;
			//try
			//{
			using (var session = config.BuildSessionFactory().OpenSession())
			{
				var tran = session.BeginTransaction(IsolationLevel.Serializable);
				DeleteData(session);
				tran.Commit();
				session.Flush();


				var tran2 = session.BeginTransaction(IsolationLevel.Serializable);
				MoveDictionary(session, db);
				MoveEntity(session, db);

				tran2.Commit();
				session.Flush();
			}
			//}
			//catch (Exception ex)
			//{
			//	succes = false;
			//	Console.WriteLine(ex.Message);
			//	if (ex.InnerException != null)
			//	{
			//		Console.WriteLine(ex.InnerException.Message);
			//		if (ex.InnerException.InnerException != null)
			//			Console.WriteLine(ex.InnerException.InnerException.Message);
			//	}
			//}

			Console.WriteLine("Результат переноса: {0}", succes ? "успешен" : "ОШИБКА");
			Console.ReadKey();
		}

		private static void DeleteData(ISession hb)
		{
			foreach (var ent in hb.Query<Flat>().ToList())
			{
				hb.Delete(ent);
			}
			foreach (var ent in hb.Query<House>().ToList())
			{
				hb.Delete(ent);
			}
			foreach (var ent in hb.Query<Plot>().ToList())
			{
				hb.Delete(ent);
			}
			foreach (var ent in hb.Query<Residence>().ToList())
			{
				hb.Delete(ent);
			}
			foreach (var ent in hb.Query<Room>().ToList())
			{
				hb.Delete(ent);
			}


			foreach (var ent in hb.Query<DealVariant>().ToList())
			{
				hb.Delete(ent);
			}
			
			foreach (var ent in hb.Query<FloorLevel>().ToList())
			{
				hb.Delete(ent);
			}
			foreach (var ent in hb.Query<Layout>().ToList())
			{
				hb.Delete(ent);
			}
			foreach (var ent in hb.Query<Material>().ToList())
			{
				hb.Delete(ent);
			}
			foreach (var ent in hb.Query<Ownership>().ToList())
			{
				hb.Delete(ent);
			}
			foreach (var ent in hb.Query<Realtor>().ToList())
			{
				hb.Delete(ent);
			}
			foreach (var ent in hb.Query<Sewage>().ToList())
			{
				hb.Delete(ent);
			}
			foreach (var ent in hb.Query<RealEstateDirectory.Domain.Entities.Dictionaries.Street>().ToList())
			{
				hb.Delete(ent);
			}
			foreach (var ent in hb.Query<RealEstateDirectory.Domain.Entities.Dictionaries.District>().ToList())
			{
				hb.Delete(ent);
			}
			foreach (var ent in hb.Query<Terrace>().ToList())
			{
				hb.Delete(ent);
			}
			foreach (var ent in hb.Query<ToiletType>().ToList())
			{
				hb.Delete(ent);
			}
			foreach (var ent in hb.Query<WaterSupply>().ToList())
			{
				hb.Delete(ent);
			}

			foreach (var ent in hb.Query<RealtorAgency>().ToList())
			{
				hb.Delete(ent);
			}

			Console.WriteLine("Удаление старого - готово");
		}

		private static void AddMapForDictionary<T>(int lId, ISession hb, T entity) where T : BaseDictionary
		{
			var exist = _idDictionaryMaps[typeof (T)].FirstOrDefault(x => x.Name == entity.Name);
			if (exist != null)
			{
				//есть такие
				exist.LinqIds.Add(lId);
			}
			else
			{
				hb.SaveOrUpdate(entity);
				_idDictionaryMaps[typeof (T)].Add(new IdMap(lId, entity.Id, entity.Name));
			}
		}

		private static void MoveDictionary(ISession hb, DataClassesDataContext linq)
		{

			foreach (var ent in hb.Query<DealVariant>().ToList())
			{
				hb.Delete(ent);
			}

			//DealVariant
			_idDictionaryMaps.Add(typeof (DealVariant), new List<IdMap>());
			foreach (var curEnitie in linq.Variants)
			{
				var hbEntity = new DealVariant(curEnitie.vcVariant);
				AddMapForDictionary(curEnitie.idVariant, hb, hbEntity);
			}
			Console.WriteLine("Сделки - готово");

			//District
			_idDictionaryMaps.Add(typeof (RealEstateDirectory.Domain.Entities.Dictionaries.District), new List<IdMap>());
			foreach (var curEnitie in linq.Districts)
			{
				var hbEntity = new RealEstateDirectory.Domain.Entities.Dictionaries.District(curEnitie.vcDistrict);
				AddMapForDictionary(curEnitie.idDistrict, hb, hbEntity);
			}
			Console.WriteLine("Районы - готово");

			//Layout
			_idDictionaryMaps.Add(typeof (Layout), new List<IdMap>());
			foreach (var curEnitie in linq.Planings)
			{
				var hbEntity = new Layout(curEnitie.vcPlaning);
				AddMapForDictionary(curEnitie.idPlaning, hb, hbEntity);

			}
			Console.WriteLine("Планировки - готово");

			//Material
			_idDictionaryMaps.Add(typeof (Material), new List<IdMap>());
			foreach (var curEnitie in linq.WallMatherials)
			{
				var hbEntity = new Material(curEnitie.vcWallMatherial);
				AddMapForDictionary(curEnitie.idWallMatherial, hb, hbEntity);
			}
			Console.WriteLine("Материалы - готово");

			//Realtor
			_idDictionaryMaps.Add(typeof (Realtor), new List<IdMap>());
			foreach (var curEnitie in linq.Rielters)
			{
				var hbEntity = new Realtor(curEnitie.vcName, curEnitie.vcContacts);
				AddMapForDictionary(curEnitie.idRielter, hb, hbEntity);
			}
			Console.WriteLine("Риэлторы - готово");

			//Street
			_idDictionaryMaps.Add(typeof (RealEstateDirectory.Domain.Entities.Dictionaries.Street), new List<IdMap>());
			using (var helper = new DataResolveHelper(hb))
			{
				helper.IdDictionaryMaps = _idDictionaryMaps;
				foreach (var curEnitie in linq.Streets)
				{
					if (curEnitie.idDistrict.HasValue)
					{
						var hbEntity = new RealEstateDirectory.Domain.Entities.Dictionaries.Street(curEnitie.vcStreet);
						var hDistrictId =
							helper.ResolveHbId<RealEstateDirectory.Domain.Entities.Dictionaries.District>(curEnitie.idDistrict.Value);

						var distictH = hb.Get<RealEstateDirectory.Domain.Entities.Dictionaries.District>(hDistrictId);
						hbEntity.District = distictH;
						AddMapForDictionary(curEnitie.idStreet, hb, hbEntity);
					}
				}
				Console.WriteLine("Улицы - готово");
			}

			//Terrace
			_idDictionaryMaps.Add(typeof (Terrace), new List<IdMap>());
			foreach (var curEnitie in linq.Balconies)
			{
				var hbEntity = new Terrace(curEnitie.vcBalcony);
				AddMapForDictionary(curEnitie.idBalcony, hb, hbEntity);
			}
			Console.WriteLine("Балконы - готово");

			//ToiletType
			_idDictionaryMaps.Add(typeof (ToiletType), new List<IdMap>());
			foreach (var curEnitie in linq.SanUsels)
			{
				var hbEntity = new ToiletType(curEnitie.vcSanUsel);
				AddMapForDictionary(curEnitie.idSanUsel, hb, hbEntity);
			}
			Console.WriteLine("Сан узел - готово");

			//Агентства
			_idDictionaryMaps.Add(typeof(RealtorAgency), new List<IdMap>());
			foreach (var curEnitie in linq.Agencies)
			{
				var hbEntity = new RealtorAgency(curEnitie.vcName)
					{
						Address = curEnitie.vcAddress,
						Contacts = curEnitie.vcContacts,
						Description = curEnitie.vcComments,
						Director = curEnitie.vcDirectorName
					};

				AddMapForDictionary(curEnitie.idAgency, hb, hbEntity);
			}
			Console.WriteLine("Агентства - готово");
		}

		private static void MoveEntity(ISession hb, DataClassesDataContext linq)
		{
			var helper = new DataResolveHelper(hb);
			helper.IdDictionaryMaps = _idDictionaryMaps;
			//IFlatService
			foreach (var l in linq.AppartmentForSales)
			{
				var hbEntity = new Flat
					{
						CreateDate = l.DateOfAdd.HasValue ? l.DateOfAdd.Value : DateTime.Now,
						DealVariant = helper.ResolveHbEntity<DealVariant>(l.Variant),
						Description = l.vcComment,
						District = helper.ResolveHbEntity<RealEstateDirectory.Domain.Entities.Dictionaries.District>(l.Variant),
						Floor = l.iFloor,
						FloorLevel = null,
						HasVideo = l.bVideo ?? false,
						KitchenSquare = l.iKitchenArea,
						Layout = helper.ResolveHbEntity<Layout>(l.Planing),
						Material = helper.ResolveHbEntity<Material>(l.WallMatherial),
						Ownership = null,
						Price = l.iPrice,
						Realtor = helper.ResolveHbEntity<Realtor>(l.Rielter),
						ResidentialSquare = l.iLivArea,
						Street = helper.ResolveHbEntity<RealEstateDirectory.Domain.Entities.Dictionaries.Street>(l.Street),
						SubmitToDomino = l.bDominoReclam ?? false,
						SubmitToVDV = l.bVdvReclam ?? false,
						Terrace = helper.ResolveHbEntity<Terrace>(l.Balcony),
						TerritorialNumber = l.vcHouseNumber,
						ToiletType = helper.ResolveHbEntity<ToiletType>(l.SanUsel),
						TotalFloor = l.iFloors,
						TotalRoomCount = l.iRoomsAmount,
						TotalSquare = l.iAllArea
					};
				hb.SaveOrUpdate(hbEntity);
			}

			Console.WriteLine("Квартиры - готово");

			//IHouseService
			foreach (var l in linq.HousesForSales)
			{
				var hbEntity = new House
					{
						CreateDate = l.DateOfAdd.HasValue ? l.DateOfAdd.Value : DateTime.Now,
						DealVariant = helper.ResolveHbEntity<DealVariant>(l.Variant),
						Description = l.vcComment,
						District = helper.ResolveHbEntity<RealEstateDirectory.Domain.Entities.Dictionaries.District>(l.Variant),
						HasVideo = l.bVideo ?? false,
						Material = helper.ResolveHbEntity<Material>(l.WallMatherial),
						Ownership = null,
						Price = l.iPrice,
						Realtor = helper.ResolveHbEntity<Realtor>(l.Rielter),
						Street = helper.ResolveHbEntity<RealEstateDirectory.Domain.Entities.Dictionaries.Street>(l.Street),
						SubmitToDomino = l.bDominoReclam ?? false,
						SubmitToVDV = l.bVdvReclam ?? false,
						TerritorialNumber = l.vcHouseNumber,
						TotalFloor = l.iFloors,
						HasBathhouse = false,
						HasGarage = false,
						HasGas = false,
						HouseSquare = l.iHouseArea,
						PlotSquare = l.iLandArea,
						Sewage = null,
						WaterSupply = null
					};
				hb.SaveOrUpdate(hbEntity);
			}

			Console.WriteLine("Дома - готово");

			//IPlotService
			foreach (var l in linq.HomestadForSales)
			{
				var hbEntity = new Plot
					{
						CreateDate = l.DateOfAdd.HasValue ? l.DateOfAdd.Value : DateTime.Now,
						DealVariant = helper.ResolveHbEntity<DealVariant>(l.Variant),
						Description = l.vcComment,
						District = helper.ResolveHbEntity<RealEstateDirectory.Domain.Entities.Dictionaries.District>(l.Variant),
						HasVideo = l.bVideo ?? false,
						Ownership = null,
						Price = l.iPrice,
						Realtor = helper.ResolveHbEntity<Realtor>(l.Rielter),
						Street = helper.ResolveHbEntity<RealEstateDirectory.Domain.Entities.Dictionaries.Street>(l.Street),
						SubmitToDomino = l.bDominoReclam ?? false,
						SubmitToVDV = l.bVdvReclam ?? false,
						TerritorialNumber = l.vcHouseNumber,
						PlotSquare = l.iArea
					};
				hb.SaveOrUpdate(hbEntity);
			}
			Console.WriteLine("Участки - готово");

			//IResidenceService
			foreach (var l in linq.PlacementForSales)
			{
				var hbEntity = new Residence
					{
						CreateDate = l.DateOfAdd.HasValue ? l.DateOfAdd.Value : DateTime.Now,
						DealVariant = helper.ResolveHbEntity<DealVariant>(l.Variant),
						Description = l.vcComment,
						District = helper.ResolveHbEntity<RealEstateDirectory.Domain.Entities.Dictionaries.District>(l.Variant),
						HasVideo = l.bVideo ?? false,
						Material = helper.ResolveHbEntity<Material>(l.WallMatherial),
						Ownership = null,
						Price = l.iPrice,
						Realtor = helper.ResolveHbEntity<Realtor>(l.Rielter),
						Street = helper.ResolveHbEntity<RealEstateDirectory.Domain.Entities.Dictionaries.Street>(l.Street),
						SubmitToDomino = l.bDominoReclam ?? false,
						SubmitToVDV = l.bVdvReclam ?? false,
						TerritorialNumber = l.vcHouseNumber,
						TotalFloor = l.iFloors,
						Floor = l.iFloor,
						TotalSquare = l.iPlaceArea
					};
				hb.SaveOrUpdate(hbEntity);
			}

			Console.WriteLine("Помещения - готово");

			//IRoomService
			foreach (var l in linq.RoomsForSales)
			{
				var hbEntity = new Room
					{
						CreateDate = l.DateOfAdd.HasValue ? l.DateOfAdd.Value : DateTime.Now,
						DealVariant = helper.ResolveHbEntity<DealVariant>(l.Variant),
						Description = l.vcComment,
						District = helper.ResolveHbEntity<RealEstateDirectory.Domain.Entities.Dictionaries.District>(l.Variant),
						HasVideo = l.bVideo ?? false,
						Material = helper.ResolveHbEntity<Material>(l.WallMatherial),
						Ownership = null,
						Price = l.iPrice,
						Realtor = helper.ResolveHbEntity<Realtor>(l.Rielter),
						Street = helper.ResolveHbEntity<RealEstateDirectory.Domain.Entities.Dictionaries.Street>(l.Street),
						SubmitToDomino = l.bDominoReclam ?? false,
						SubmitToVDV = l.bVdvReclam ?? false,
						TerritorialNumber = l.vcHouseNumber,
						TotalFloor = l.iFloors,
						Floor = l.iFloor,
						FloorLevel = null,
						Layout = helper.ResolveHbEntity<Layout>(l.Planing),
						RoomCount = l.iRoomsAmount,
						TotalRoomCount = l.iRoomsAll,
						Terrace = helper.ResolveHbEntity<Terrace>(l.Balcony),
						TotalSquare = l.iAllArea
					};
				hb.SaveOrUpdate(hbEntity);
			}

			Console.WriteLine("Комнаты - готово");
		}
	}
}