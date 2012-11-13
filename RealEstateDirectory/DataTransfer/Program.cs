using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		private static void Main(string[] args)
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
				MoveDictionary(session, db);
				MoveEntitie(session, db);

				tran.Commit();
			}
			//}
			//catch (Exception ex)
			//{
			//	succes = false;
			//	Console.WriteLine(ex.Message);
			//	if (ex.InnerException != null)
			//		Console.WriteLine(ex.InnerException.Message);
			//}

			Console.WriteLine("Результат переноса: {0}", succes);
			Console.ReadKey();
		}

		private static void DeleteData(ISession hb)
		{
			foreach (var ent in hb.Query<DealVariant>().ToList())
			{
				hb.Delete(ent);
			}
			foreach (var ent in hb.Query<RealEstateDirectory.Domain.Entities.Dictionaries.District>().ToList())
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

			Console.WriteLine("Удаление старого - готово");
		}

		private static void AddMapForDictionary<T>(int lId, ISession hb, T entitie) where T : BaseDictionary
		{
			var exist = _idDictionaryMaps[typeof (T)].FirstOrDefault(x => x.Name == entitie.Name);
			if (exist != null)
			{
				//есть такие
				exist.LinqIds.Add(lId);
			}
			else
			{
				hb.SaveOrUpdate(entitie);
				_idDictionaryMaps[typeof (T)].Add(new IdMap(lId, entitie.Id, entitie.Name));
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
				var hbEntitie = new DealVariant(curEnitie.vcVariant);
				AddMapForDictionary(curEnitie.idVariant, hb, hbEntitie);
			}
			Console.WriteLine("Сделки - готово");

			//District
			_idDictionaryMaps.Add(typeof (RealEstateDirectory.Domain.Entities.Dictionaries.District), new List<IdMap>());
			foreach (var curEnitie in linq.Districts)
			{
				var hbEntitie = new RealEstateDirectory.Domain.Entities.Dictionaries.District(curEnitie.vcDistrict);
				AddMapForDictionary(curEnitie.idDistrict, hb, hbEntitie);
			}
			Console.WriteLine("Районы - готово");

			//Layout
			_idDictionaryMaps.Add(typeof (Layout), new List<IdMap>());
			foreach (var curEnitie in linq.Planings)
			{
				var hbEntitie = new Layout(curEnitie.vcPlaning);
				AddMapForDictionary(curEnitie.idPlaning, hb, hbEntitie);

			}
			Console.WriteLine("Планировки - готово");

			//Material
			_idDictionaryMaps.Add(typeof (Material), new List<IdMap>());
			foreach (var curEnitie in linq.WallMatherials)
			{
				var hbEntitie = new Material(curEnitie.vcWallMatherial);
				AddMapForDictionary(curEnitie.idWallMatherial, hb, hbEntitie);
			}
			Console.WriteLine("Материалы - готово");

			//Realtor
			_idDictionaryMaps.Add(typeof (Realtor), new List<IdMap>());
			foreach (var curEnitie in linq.Rielters)
			{
				var hbEntitie = new Realtor(curEnitie.vcName, curEnitie.vcContacts);
				AddMapForDictionary(curEnitie.idRielter, hb, hbEntitie);
			}
			Console.WriteLine("Риэлторы - готово");

			//Street
			_idDictionaryMaps.Add(typeof (RealEstateDirectory.Domain.Entities.Dictionaries.Street), new List<IdMap>());
			foreach (var curEnitie in linq.Streets)
			{
				if (curEnitie.idDistrict.HasValue)
				{
					var hbEntitie = new RealEstateDirectory.Domain.Entities.Dictionaries.Street(curEnitie.vcStreet);
					var hDistrictId = ResolveHbId<RealEstateDirectory.Domain.Entities.Dictionaries.District>(curEnitie.idDistrict.Value);

					var distictH = hb.Get<RealEstateDirectory.Domain.Entities.Dictionaries.District>(hDistrictId);
					hbEntitie.District = distictH;
					AddMapForDictionary(curEnitie.idStreet, hb, hbEntitie);
				}
			}
			Console.WriteLine("Улицы - готово");

			//Terrace
			_idDictionaryMaps.Add(typeof (Terrace), new List<IdMap>());
			foreach (var curEnitie in linq.Balconies)
			{
				var hbEntitie = new Terrace(curEnitie.vcBalcony);
				AddMapForDictionary(curEnitie.idBalcony, hb, hbEntitie);
			}
			Console.WriteLine("Балконы - готово");

			//ToiletType
			_idDictionaryMaps.Add(typeof (ToiletType), new List<IdMap>());
			foreach (var curEnitie in linq.SanUsels)
			{
				var hbEntitie = new ToiletType(curEnitie.vcSanUsel);
				AddMapForDictionary(curEnitie.idSanUsel, hb, hbEntitie);
			}
			Console.WriteLine("Сан узел - готово");
		}

		private static void MoveEntitie(ISession hb, DataClassesDataContext linq)
		{
			//IFlatService
			foreach (var l in linq.AppartmentForSales)
			{
				var hbEntitie = new Flat
					{
						CreateDate = l.DateOfAdd.HasValue ? l.DateOfAdd.Value : DateTime.Now,
						DealVariant = ResolveHbEntitie<DealVariant>(l.Variant.idVariant, hb),
						Description = l.vcComment,
						District = ResolveHbEntitie<RealEstateDirectory.Domain.Entities.Dictionaries.District>(l.Variant.idVariant, hb),
						Floor = l.iFloor,
						FloorLevel = null,
						HasVideo = l.bVideo ?? false,
						KitchenSquare = l.iKitchenArea,
						Layout = ResolveHbEntitie<Layout>(l.Planing.idPlaning, hb),
						Material = ResolveHbEntitie<Material>(l.WallMatherial.idWallMatherial, hb),
						Ownership = null,
						Price = l.iPrice,
						Realtor = ResolveHbEntitie<Realtor>(l.Rielter.idRielter, hb),
						ResidentialSquare = l.iLivArea,
						Street = ResolveHbEntitie<RealEstateDirectory.Domain.Entities.Dictionaries.Street>(l.Street.idStreet, hb),
						SubmitToDomino = l.bDominoReclam ?? false,
						SubmitToVDV = l.bVdvReclam ?? false,
						Terrace = ResolveHbEntitie<Terrace>(l.Balcony.idBalcony, hb),
						TerritorialNumber = l.vcHouseNumber,
						ToiletType = ResolveHbEntitie<ToiletType>(l.SanUsel.idSanUsel, hb),
						TotalFloor = l.iFloors,
						TotalRoomCount = l.iRoomsAmount,
						TotalSquare = l.iAllArea,
					};
				hb.SaveOrUpdate(hbEntitie);
			}

			Console.WriteLine("Квартиры - готово");

			//IHouseService
			//IPlotService
			//IResidenceService
			//IRoomService

		}

		private static T ResolveHbEntitie<T>(int lId, ISession hb) where T : BaseDictionary
		{
			var hbId = ResolveHbId<T>(lId);
			var en = hb.Get<T>(hbId);

			return en;
		}

		private static int ResolveHbId<T>(int linqId) where T : BaseDictionary
		{
			return _idDictionaryMaps[typeof (T)].Single(x => x.LinqIds.Contains(linqId)).HbId;
		}
	}

	public class IdMap
	{
		/// <summary>
		/// id элемента в базе linq 
		/// </summary>
		public List<int> LinqIds;
		/// <summary>
		/// id нового элемента в хибере
		/// </summary>
		public int HbId;
		/// <summary>
		/// имя для поиска дублей
		/// </summary>
		public string Name;

		public IdMap(int l, int h, string name)
		{
			LinqIds = new List<int> {l};
			HbId = h;
			Name = name;
		}
	}
}