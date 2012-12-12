using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace RealEstateDirectory.Misc
{
	/// <summary>
	/// Interaction logic for About.xaml
	/// </summary>
	public partial class About : Window
	{
		public About()
		{
			InitializeComponent();
			GetAsemblyInfo();
		}

		public void GetAsemblyInfo()
		{
			Assembly app = Assembly.GetExecutingAssembly();
			Version version = app.GetName().Version;
			txtVersion.Text = String.Format("Версия {0}", version);
			hlUpdate.NavigateUri = new Uri(ConfigurationManager.AppSettings["UpdateUrl"]);
		}

		private void Hyperlink_RequestNavigate
			(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
		{
			Process.Start(e.Uri.ToString());
		}
	}
}
	