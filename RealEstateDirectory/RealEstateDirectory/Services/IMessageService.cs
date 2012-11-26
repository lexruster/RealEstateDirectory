using System.Windows;

namespace RealEstateDirectory.Services
{
	public interface IMessageService
	{
		MessageBoxResult ShowMessage(string text, string title, MessageBoxButton buttons = MessageBoxButton.OK, MessageBoxImage image = MessageBoxImage.None);
		bool ShowConfirm(string text, string title);
	}
}