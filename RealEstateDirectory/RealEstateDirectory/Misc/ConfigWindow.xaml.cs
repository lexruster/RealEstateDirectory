using System;
using System.Windows;

namespace RealEstateDirectory.Misc
{
	/// <summary>
	/// Interaction logic for ConfigWindow.xaml
	/// </summary>
	public partial class ConfigWindow : Window
	{
		public ConfigWindow()
		{
			InitializeComponent();
		}

		private void Save(object sender, RoutedEventArgs e)
		{
			Utils.Config.Load();
			Utils.Config.AddProperty("DefaultConnectionString",
			                         String.Format("Server={0};Port={1};Database={2};UserId={3};Password={4}", txtIp.Text,
			                                       txtPort.Text, txtBD.Text, txtLogin.Text, txtPassword.Text));
			Utils.Config.Save();

			DialogResult = true;
			//Server=127.0.0.1;Port=5432;Database=RealEstateDirectory;UserId=nhibernate;Password=nhibernate
		}

		private void ExitClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Utils.Config.Load();
			var prop = Utils.Config.GetProperty("DefaultConnectionString");
			if (!String.IsNullOrEmpty(prop))
			{
				try
				{
					var data = prop.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries);
					txtIp.Text = data[0].Replace("Server=", "");
					txtPort.Text = data[1].Replace("Port=", "");
					txtBD.Text = data[2].Replace("Database=", "");
					txtLogin.Text = data[3].Replace("UserId=", "");
					txtPassword.Text = data[4].Replace("Password=", "");
				}
				catch (Exception)
				{

				}
			}
		}
	}
}