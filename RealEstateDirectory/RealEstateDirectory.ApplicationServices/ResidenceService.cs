using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.ApplicationServices
{
    public class ResidenceService : RealEstateService<Residence>, IResidenceService
	{
		public override string RealEstateName
		{
			get { return "Помещения"; }
		}
		
		#region Конструктор

		public ResidenceService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            :base(persistenceContext, serviceLocator)
        {
        }

        #endregion

		#region Методы

		public override IEnumerable<Residence> GetAll()
		{
			return Repository.GetAllResidence().OrderBy(x => x.District == null ? "" : x.District.Name).ThenBy(x => x.Street==null?"":x.Street.Name);
		}

		#endregion
    }
}