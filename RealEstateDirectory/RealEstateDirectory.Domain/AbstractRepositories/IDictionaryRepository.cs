using System.Collections.Generic;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Infrastructure.Repositories;

namespace RealEstateDirectory.Domain.AbstractRepositories
{
	public interface IDictionaryRepository : IRepositoryWithTypedId<int>
	{
		bool IsPossibleToDeleteStreet(Street entity);
		bool IsPossibleToDeleteDistrict(District entity);
		bool IsPossibleToDeleteWaterSupply(WaterSupply entity);
		bool IsPossibleToDeleteToiletType(ToiletType entity);
		bool IsPossibleToDeleteTerrace(Terrace entity);
		bool IsPossibleToDeleteMaterial(Material entity);
		bool IsPossibleToDeleteDealVariant(DealVariant entity);
		bool IsPossibleToDeleteSewage(Sewage entity);
		bool IsPossibleToDeleteRealtor(Realtor entity);
		bool IsPossibleToDeleteOwnership(Ownership entity);
		bool IsPossibleToDeleteLayout(Layout entity);
		bool IsPossibleToDeleteFloorLevel(FloorLevel entity);

		bool IsNameUniqueness<T>(T entity, int id = 0) where T : BaseDictionary;
		bool IsPossibleToDeleteRealtorAgency(RealtorAgency entity);
		bool IsPossibleToDeleteCondition(Condition entity);
	}
}