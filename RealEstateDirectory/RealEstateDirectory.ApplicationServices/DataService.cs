using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Infrastructure.NHibernate.DbSession;

namespace RealEstateDirectory.ApplicationServices
{
    public class DataService : IDataService
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly IPersistenceContext _persistenceContext;

        public DataService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
		{
            _serviceLocator = serviceLocator;
            _persistenceContext = persistenceContext;
		}

        public void CreateArea(string name)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateArea(int id, string name)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteArea(int id)
        {
            throw new System.NotImplementedException();
        }

        public Layout CreateLayout(string name)
        {
            var layoutRepository = _serviceLocator.GetInstance<IDictionaryRepository<Layout>>();
            //получить текущую сессию
            using (new DbSession(_persistenceContext))
            {
                //Для записи создать транзакцию
                using (var transaction = _persistenceContext.CurrentSession.BeginTransaction())
                {
                    var layout = new Layout(name);

                    layoutRepository.SaveOrUpdate(layout);

                    //Комитить
                    transaction.Commit();
                    return layout;
                }
            }
        }

        public void UpdateLayout(int id, string name)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteLayout(int id)
        {
            throw new System.NotImplementedException();
        }

        public Layout FindLayout(string name)
        {
            using (new DbSession(_persistenceContext))
            {
                return _serviceLocator.GetInstance<IDictionaryRepository<Layout>>().Get(name);
            }
        }

        public void CreateSewage(string name)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateSewage(int id, string name)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteSewage(int id)
        {
            throw new System.NotImplementedException();
        }

        public Street CreateStreet(string name)
        {
            //получить текущую сессию
            using (new DbSession(_persistenceContext))
            {
                //Для записи создать транзакцию
                using (var transaction = _persistenceContext.CurrentSession.BeginTransaction())
                {
                    var street = new Street(name);

                    _serviceLocator.GetInstance<IDictionaryRepository<Street>>().SaveOrUpdate(street);

                    //Комитить
                    transaction.Commit();
                    return street;
                }
            }
        }

        public void UpdateStreet(int id, string name)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteStreet(int id)
        {
            throw new System.NotImplementedException();
        }

        public void CreateState(string name)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateState(int id, string name)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteState(int id)
        {
            throw new System.NotImplementedException();
        }

        public Plot CreatePlot(string descr, Street street)
        {
            var plotRepository = _serviceLocator.GetInstance<IRealEstateRepository<Plot>>();
            var plot = new Plot();
            plot.Description = descr;
            plot.Street = street;

            //получить текущую сессию
            using (new DbSession(_persistenceContext))
            {
                //Для записи создать транзакцию
                using (var transaction = _persistenceContext.CurrentSession.BeginTransaction())
                {
                    plotRepository.SaveOrUpdate(plot);

                    //Комитить
                    transaction.Commit();
                    return plot;
                }
            }
        }
    }
}