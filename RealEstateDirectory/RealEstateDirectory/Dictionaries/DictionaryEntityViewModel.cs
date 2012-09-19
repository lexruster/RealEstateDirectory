using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries
{
	public class DictionaryEntityViewModel<T>
	{
		public DictionaryEntityViewModel(IDataService dataService)
		{
			_DataService = dataService;
		}

		#region Infrastructure

		private readonly IDataService _DataService;

		#endregion
	}
}