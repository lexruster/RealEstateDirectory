using System.Collections.Generic;
using System.Collections.ObjectModel;
using RealEstateDirectory.Data.Entities;
using RealEstateDirectory.Dictionaries;

namespace RealEstateDirectory.Services
{
	public interface IDataService
	{
		IEnumerable<Street> GetStreets();

		void RemoveStreet(Street entity);

		bool IsCanRemove(Street entity, out string errorMessage);

		void SaveStreet(Street entity);

		bool IsCorrect(Street entity, out string errorMessage);
	}
}