using System;
using System.Collections.ObjectModel;
using RealEstateDirectory.Data.Entities;

namespace RealEstateDirectory.Services
{
	public class DataService : IDataService
	{
		public void CreateArea(string name)
		{
			throw new NotImplementedException();
		}

		public void UpdateArea(int id, string name)
		{
			throw new NotImplementedException();
		}

		public void DeleteArea(int id)
		{
			throw new NotImplementedException();
		}

		public void CreateLayout(string name)
		{
			throw new NotImplementedException();
		}

		public void UpdateLayout(int id, string name)
		{
			throw new NotImplementedException();
		}

		public void DeleteLayout(int id)
		{
			throw new NotImplementedException();
		}

		public void CreateSewage(string name)
		{
			throw new NotImplementedException();
		}

		public void UpdateSewage(int id, string name)
		{
			throw new NotImplementedException();
		}

		public void DeleteSewage(int id)
		{
			throw new NotImplementedException();
		}

		public void CreateStreet(string name)
		{
			throw new NotImplementedException();
		}

		public void UpdateStreet(int id, string name)
		{
			throw new NotImplementedException();
		}

		public void DeleteStreet(int id)
		{
			throw new NotImplementedException();
		}

		public void CreateState(string name)
		{
			throw new NotImplementedException();
		}

		public void UpdateState(int id, string name)
		{
			throw new NotImplementedException();
		}

		public void DeleteState(int id)
		{
			throw new NotImplementedException();
		}

		public ObservableCollection<TEntity> GetEntityLink<TEntity>()
		{
			if (typeof(TEntity) == typeof(Street))
				return new ObservableCollection<TEntity>();
			else
				throw new NotImplementedException();
		}
	}
}