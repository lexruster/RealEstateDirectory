using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.AbstractApplicationServices
{
	public interface IDataService
	{
		void CreateArea(string name);
		void UpdateArea(int id, string name);
		void DeleteArea(int id);

		Layout CreateLayout(string name);
		void UpdateLayout(int id, string name);
		void DeleteLayout(int id);
	    Layout FindLayout(string name);

		void CreateSewage(string name);
		void UpdateSewage(int id, string name);
		void DeleteSewage(int id);

		Street CreateStreet(string name);
		void UpdateStreet(int id, string name);
		void DeleteStreet(int id);

		void CreateState(string name);
		void UpdateState(int id, string name);
		void DeleteState(int id);
        Plot CreatePlot(string descr, Street street);
	}
}