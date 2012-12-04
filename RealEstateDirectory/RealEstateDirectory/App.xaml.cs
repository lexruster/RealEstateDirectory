using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using RealEstateDirectory.Misc;

namespace RealEstateDirectory
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			Utils.Config.Load();
			if (String.IsNullOrEmpty(Utils.Config.GetProperty("DefaultConnectionString")))
			{
				var configWindow = new ConfigWindow();
				var result = configWindow.ShowDialog();
				if (!result.HasValue || !result.Value)
				{
					Application.Current.Shutdown();
					return;
				}
			}

            var bootstrapper = new Bootstrapper();
			bootstrapper.Run();
		}
	}
}
