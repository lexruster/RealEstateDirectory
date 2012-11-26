using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.ApplicationServices
{
    public class RoomService : RealEstateService<Room>, IRoomService
	{
		public override string RealEstateName
		{
			get { return "Команты"; }
		}

		#region Конструктор

		public RoomService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator) :
            base(persistenceContext, serviceLocator)
        {
        }

        #endregion

		#region Методы

		public override IEnumerable<Room> GetAll()
		{
			return
				Repository.GetAllRoom().OrderBy(x => x.District == null ? "" : x.District.Name).ThenBy(x => x.RoomCount).ThenBy(
					x => x.Street == null ? "" : x.Street.Name);
		}

		#endregion
    }
}