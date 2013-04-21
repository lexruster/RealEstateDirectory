using System.Linq;
using NHibernate.Linq;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Infrastructure.NHibernate.Repositories;

namespace RealEstateDirectory.Domain.Data.Repository
{
	public class DictionaryRepository : RepositoryWithTypedIdBase<int>, IDictionaryRepository
	{
		public DictionaryRepository(IPersistenceContext persistentContext)
			: base(persistentContext)
		{
		}

		public bool IsPossibleToDeleteStreet(Street entity)
		{
			return CurrentSession.Query<RealEstate>().Count(x => x.Street == entity) == 0;
		}

		public bool IsPossibleToDeleteDistrict(District entity)
		{
			return CurrentSession.Query<RealEstate>().Count(x => x.District == entity) == 0;
		}

		public bool IsPossibleToDeleteWaterSupply(WaterSupply entity)
		{
			return true;
		}

		public bool IsPossibleToDeleteToiletType(ToiletType entity)
		{
			return CurrentSession.Query<Flat>().Count(x => x.ToiletType == entity) == 0;
		}

		public bool IsPossibleToDeleteTerrace(Terrace entity)
		{
			return CurrentSession.Query<Apartment>().Count(x => x.Terrace == entity) == 0;
		}

		public bool IsPossibleToDeleteMaterial(Material entity)
		{
			return CurrentSession.Query<Building>().Count(x => x.Material == entity) == 0;
		}

		public bool IsPossibleToDeleteDealVariant(DealVariant entity)
		{
			return CurrentSession.Query<RealEstate>().Count(x => x.DealVariant == entity) == 0;
		}

		public bool IsPossibleToDeleteSewage(Sewage entity)
		{
			return true;
		}

		public bool IsPossibleToDeleteRealtor(Realtor entity)
		{
			return CurrentSession.Query<RealEstate>().Count(x => x.Realtor == entity) == 0;
		}

		public bool IsPossibleToDeleteOwnership(Ownership entity)
		{
			return CurrentSession.Query<RealEstate>().Count(x => x.Ownership == entity) == 0;
		}

		public bool IsPossibleToDeleteLayout(Layout entity)
		{
			return CurrentSession.Query<Apartment>().Count(x => x.Layout == entity) == 0;
		}

		public bool IsPossibleToDeleteFloorLevel(FloorLevel entity)
		{
			return CurrentSession.Query<Apartment>().Count(x => x.FloorLevel == entity) == 0;
		}

		public bool IsPossibleToDeleteCondition(Condition entity)
		{
			return CurrentSession.Query<RealEstate>().Count(x => x.Condition == entity) == 0;
		}

		public bool IsPossibleToDeleteDestination(Destination entity)
		{
			return CurrentSession.Query<Residence>().Count(x => x.Destination == entity) == 0;
		}

		public bool IsNameUniqueness<T>(T entity, int id = 0) where T : BaseDictionary
		{
			var localId = id == 0 ? entity.Id : id;
			return CurrentSession.Query<T>().Count(x => x.Name == entity.Name && x.Id != localId) == 0;
		}

		public bool IsPossibleToDeleteRealtorAgency(RealtorAgency entity)
		{
			return true;
		}

		public T Get<T>(string name) where T : BaseDictionary
		{
			return CurrentSession.Query<T>().FirstOrDefault(x => x.Name == name);
		}
	}
}