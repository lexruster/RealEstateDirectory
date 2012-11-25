using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.ApplicationServices
{
    public class PlotService : RealEstateService<Plot>, IPlotService
    {
		public override string RealEstateName
		{
			get { return "�������"; }
		}

        #region �����������

        public PlotService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator) :
            base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region ������

        public override IEnumerable<Plot> GetAll()
        {
            return Repository.GetAllPlot();
        }

        #endregion
    }
}