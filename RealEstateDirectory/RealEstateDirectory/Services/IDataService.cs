﻿using System.Collections.ObjectModel;

namespace RealEstateDirectory.Services
{
	public interface IDataService
	{
		void CreateArea(string name);
		void UpdateArea(int id, string name);
		void DeleteArea(int id);

		void CreateLayout(string name);
		void UpdateLayout(int id, string name);
		void DeleteLayout(int id);

		void CreateSewage(string name);
		void UpdateSewage(int id, string name);
		void DeleteSewage(int id);

		void CreateStreet(string name);
		void UpdateStreet(int id, string name);
		void DeleteStreet(int id);

		void CreateState(string name);
		void UpdateState(int id, string name);
		void DeleteState(int id);

		ObservableCollection<TEntity> GetEntityLink<TEntity>();
	}
}