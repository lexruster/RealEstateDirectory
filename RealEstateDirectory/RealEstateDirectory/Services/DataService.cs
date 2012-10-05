using System;
using System.Collections.Generic;
using System.Diagnostics;
using RealEstateDirectory.Data.Entities;

namespace RealEstateDirectory.Services
{
	public class DataService : IDataService
	{
		public IEnumerable<Street> GetStreets()
		{
			//TODO: Выборка всех Street-ов из базы.
			Debug.WriteLine("Select all Street from database.");
			return new Street[0];
		}

		public Street CreateNewStreet()
		{
			return new Street();
		}

		public void RemoveStreet(Street entity)
		{
			//TODO: Удаление объекта из базы данных.
			Debug.WriteLine("Remove Street object from database.");
			throw new NotImplementedException();
		}

		public void SaveStreet(Street entity)
		{
			//TODO: Добавление объекта в базу данных.
			Debug.WriteLine("Save Street object to database.");
		}

		public bool IsCorrect(Street entity, out string errorMessage)
		{
			//TODO: Проверка объекта на правильность.
			Debug.WriteLine("Check, is Street object correct.");
			errorMessage = null;
			return true;
		}

		public bool IsCanRemove(Street entity, out string validateMessage)
		{
			//TODO: Проверка объекта на возможность удаления.
			Debug.WriteLine("Check, is can we remove Street object from database.");
			validateMessage = null;
			return true;
		}
	}
}