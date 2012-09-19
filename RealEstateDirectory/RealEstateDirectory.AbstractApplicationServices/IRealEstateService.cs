using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.AbstractApplicationServices
{
    public interface IRealEstateService<T> :IBaseService<T> where T : RealEstate
	{
	}
}