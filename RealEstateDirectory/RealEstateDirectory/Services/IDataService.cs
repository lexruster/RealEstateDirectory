using System.Collections.Generic;
using System.Collections.ObjectModel;
using RealEstateDirectory.Data.Entities;
using RealEstateDirectory.Dictionaries;

namespace RealEstateDirectory.Services
{
	public interface IDataService
	{
		IEnumerable<Street> GetStreets();

		Street AddNewStreet();

		void RemoveStreet(Street entity);

		bool IsCorrect(Street entity, out string validateMessage);
	}
}