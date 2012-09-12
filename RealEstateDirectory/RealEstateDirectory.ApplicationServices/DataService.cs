using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Infrastructure.NHibernate.DbSession;

namespace RealEstateDirectory.ApplicationServices
{
    public class DataService : IDataService
    {
	    //private readonly IUnitOfWorkFactory _UnitOfWorkFactory;
        private readonly IPersistenceContext _persistenceContext;
        private readonly ILayoutRepository _LayoutRepository;

        public DataService(IPersistenceContext persistenceContext, ILayoutRepository layoutRepository)
		{
            _persistenceContext = persistenceContext;
            _LayoutRepository = layoutRepository;
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
            //получить текущую сессию
            using (new DbSession(_persistenceContext))
            {
                //Для записи создать транзакцию
                using (var transaction = _persistenceContext.CurrentSession.BeginTransaction())
                {
                    var layout = new Layout(name);

                    _LayoutRepository.SaveOrUpdate(layout);

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
                return _LayoutRepository.Get(name);
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

        public void CreateStreet(string name)
        {
            throw new System.NotImplementedException();
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
    }
}