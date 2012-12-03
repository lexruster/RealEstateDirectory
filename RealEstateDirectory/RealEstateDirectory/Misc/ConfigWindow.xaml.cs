using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RealEstateDirectory.Config
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
	}
}
