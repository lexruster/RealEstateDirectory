using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;

namespace RealEstateDirectory.Dictionaries
{
	[NotifyForAll]
	public class StreetViewModel : NotificationObject
	{
		public string Name { get; set; }
	}
}