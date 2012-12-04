using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using RealEstateDirectory.Migrations;
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

			LoadConfig();
			RunMigrations();

			var bootstrapper = new Bootstrapper();
			bootstrapper.Run();
		}

		private static void LoadConfig()
		{
			Utils.Config.Load();
			if (String.IsNullOrEmpty(Utils.Config.GetProperty("DefaultConnectionString")))
			{
				var configWindow = new ConfigWindow();
				var result = configWindow.ShowDialog();
				if (!result.HasValue || !result.Value)
					Current.Shutdown();
			}
		}

		private void RunMigrations()
		{
			var context = new RunnerContext(new NullAnnouncer())
				{
					Database = "postgresql",
					Connection = Utils.Config.GetProperty("DefaultConnectionString"),
					Target = Assembly.GetAssembly(typeof (MigrationsBeacon)).Location,
					PreviewOnly = false,
					NestedNamespaces = false,
					Task = "migrate"
				};
			new TaskExecutor(context).Execute();
		}
	}
}
