using System.Windows;

namespace RealEstateDirectory.Shell
{
	/// <summary>
	/// Interaction logic for ShellView.xaml
	/// </summary>
	public partial class ShellView : Window
	{
		public ShellView()
		{
			InitializeComponent();

			Closing += (sender, args) =>
				{
					Application.Current.Shutdown();
				};
		}
	}
}
