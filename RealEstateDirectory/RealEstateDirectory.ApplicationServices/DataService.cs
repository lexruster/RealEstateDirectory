using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.ApplicationServices
{
    public class DataService : IDataService
    {
        private readonly ILayoutRepository _layoutRepository;

        public DataService(ILayoutRepository layoutRepository)
        {
            _layoutRepository = layoutRepository;
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

        public void CreateLayout(string name)
        {
            throw new System.NotImplementedException();
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
            return _layoutRepository.Get(name);
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