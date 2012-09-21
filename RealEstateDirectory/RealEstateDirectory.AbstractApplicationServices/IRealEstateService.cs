using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.AbstractApplicationServices
{
    public interface IRealEstateService<T> :IDataEntityBaseService<T> where T : RealEstate
	{
	}
}