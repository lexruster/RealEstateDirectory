using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.ApplicationServices
{
    public class PlotService : RealEstateService<Plot>, IPlotService
    {
        #region Конструктор

        protected PlotService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator) :
            base(persistenceContext, serviceLocator)
        {
        }

        #endregion
    }
}