using System.Collections.ObjectModel;

namespace RealEstateDirectory.Services
{
	public interface IDataService
	{
		ObservableCollection<TEntity> GetEntityLink<TEntity>();

		bool IsCorrect<TEntity>(TEntity entity, out string validateMessage);
	}
}