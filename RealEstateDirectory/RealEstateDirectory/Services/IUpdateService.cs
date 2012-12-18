using System.Windows;

namespace RealEstateDirectory.Services
{
	public interface IUpdateService
	{
		void CheckUpdates(bool showSucces);
		void StartPeriodicCheck();
	}
}