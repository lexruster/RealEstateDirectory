using Microsoft.Practices.Prism.UnityExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using RealEstateDirectory.Dictionaries;
using RealEstateDirectory.Services;
using RealEstateDirectory.Shell;

namespace RealEstateDirectory
{
	public class Bootstrapper : UnityBootstrapper
	{
		protected override void ConfigureContainer()
		{
			base.ConfigureContainer();

			Container.RegisterType<IDataService, DataService>();
			Container.RegisterType<IViewsService, ViewsService>();
			Container.RegisterType<IMessageService, MessageService>();
		}

		protected override DependencyObject CreateShell()
		{
			return Container.Resolve<ShellView>();
		}

		protected override void InitializeShell()
		{
			base.InitializeShell();
			((ShellView) Shell).DataContext = Container.Resolve<ShellViewModel>();
			Application.Current.MainWindow = (Window) Shell;
		}

		public override void Run(bool runWithDefaultConfiguration)
		{
			base.Run(runWithDefaultConfiguration);
			Application.Current.MainWindow.Show();
		}
	}
}
