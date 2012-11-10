using System.Windows;
using RealEstateDirectory.Dictionaries.Common;

namespace RealEstateDirectory.Services
{
	public interface IViewsService
	{
		void OpenView<TViewModel>();
	}
}