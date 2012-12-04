using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using ActiveLock3_6NET;
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
        private _IActiveLock _activeLock;
        private ActiveLockEventNotifier _activeLockEventNotifier;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

		    InitConfig();

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
					Database = "postgres",
					Connection = Utils.Config.GetProperty("DefaultConnectionString"),
					Target = Assembly.GetAssembly(typeof (MigrationsBeacon)).Location,
					PreviewOnly = false,
					NestedNamespaces = false,
					Task = "migrate"
				};
			new TaskExecutor(context).Execute();
		}

        private void InitConfig()
        {
            var tempResult = true;
            try
            {
                var myAl = new Globals();
                _activeLock = myAl.NewInstance();
                _activeLock.SoftwareName = "RealEstateDirectory";
                _activeLock.SoftwareVersion = "1.0";
                _activeLock.TrialType = IActiveLock.ALTrialTypes.trialNone;

                _activeLock.SoftwareCode =
                    @"RSA1024<RSAKeyValue><Modulus>0srdjuOxJjnOC2Y9gQexlzvdHAbilsjf3er5asn889Pj/N6GjaLLE6p1R+QO3AHZQ6QP5U1/WURRoPSzE+5lvv/H03g2k/LuCEoUwednDm6eaFiS/v3/QpsI1DyQ8rFIr0HciNfDqCpFOq4gTXry8Pc4dGE6aAKFH1M0xRh3zrk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

                _activeLock.LockType = IActiveLock.ALLockTypes.lockFingerprint | IActiveLock.ALLockTypes.lockWindows |
                                      IActiveLock.ALLockTypes.lockBaseboardID | IActiveLock.ALLockTypes.lockMotherboard;
                _activeLock.AutoRegisterKeyPath = Environment.CurrentDirectory + @"\RealEstateDirectory1.0.all";
                _activeLock.KeyStoreType = IActiveLock.LicStoreType.alsFile;
                var keyStorePath = Environment.CurrentDirectory + @"\lic.lic";
                _activeLock.KeyStorePath = keyStorePath;
                _activeLock.SoftwarePassword = Convert.ToChar(99).ToString() + Convert.ToChar(111).ToString() +
                                              Convert.ToChar(111).ToString() + Convert.ToChar(108).ToString();
                _activeLockEventNotifier = _activeLock.EventNotifier;

                var path =
                    System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                _activeLock.Init(path, ref keyStorePath);
            }
            catch (Exception ex)
            {
                LoadRubrics();
                tempResult = false;
            }

            if (tempResult)
            {
                var answer = "";
                string str1 = "", str2 = "", str3 = "", str4 = "", str5 = "", str6 = "", str7 = "", str8 = "", str9 = "", str10 = "", str11 = "";

                try
                {
                    _activeLock.Acquire(ref answer, ref str1, ref str2, ref str3, ref str4, ref str5, ref str6, ref str7, ref str8, ref str9, ref str10, ref str11);
                }
                catch (Exception ex)
                {
                    LoadRubrics();
                    tempResult = false;
                }
            }

            if (!tempResult)
            {
                Current.Shutdown();
            }
        }

        private void LoadRubrics()
        {
            var registerWindow = new RegisterWindow(_activeLock);
            var result = registerWindow.ShowDialog();
        }
	}
}
