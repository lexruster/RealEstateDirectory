using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.ApplicationServices
{
    public class HouseService : RealEstateService<House>, IHouseService
    {

		public override string RealEstateName
		{
			get { return "Дома"; }
		}

        #region Конструктор

        public HouseService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator) :
            base(persistenceContext, serviceLocator)
        {
        }

        #endregion
    }
}