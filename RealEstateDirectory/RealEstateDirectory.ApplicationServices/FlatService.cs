using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.ApplicationServices
{
    public class FlatService : RealEstateService<Flat>, IFlatService
    {

		public override string RealEstateName
		{
			get { return "Квартиры"; }
		}

        #region Конструктор

        public FlatService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator) :
            base(persistenceContext, serviceLocator)
        {
        }

        #endregion

		#region Методы

		public override IEnumerable<Flat> GetAll()
		{
			return Repository.GetAllFlat().OrderBy(x => x.District == null ? "" : x.District.Name).ThenBy(x => x.TotalRoomCount).ThenBy(
					x => x.Street == null ? "" : x.Street.Name);
		}

		#endregion
    }
}
