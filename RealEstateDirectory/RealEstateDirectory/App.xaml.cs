using System;
using System.Windows;
using ActiveLock3_6NET;
using NLog;
using RealEstateDirectory.Misc;
using Environment = System.Environment;

namespace RealEstateDirectory
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private _IActiveLock _activeLock;
        private ActiveLockEventNotifier _activeLockEventNotifier;
		private bool _appShutdown = false;

		protected override void OnStartup(StartupEventArgs e)
		{
			Log.Info("      ");
			Log.Info("==================   START  ==========================");
			Log.Info("Приложение запущено");
			base.OnStartup(e);

		    InitConfig();
			if (_appShutdown) return;

			var bootstrapper = new Bootstrapper();
			Log.Info("Бутстрапер создан");
			bootstrapper.Run();
			Log.Info("Бутстрапер запущен");
		}

        private void InitConfig()
        {
            var tempResult = true;
            try
            {
				Log.Info("Начало поиска лицензии");
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
				Log.ErrorException("Лицензия не определена", ex);
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
					Log.ErrorException("Лицензия отсутсвует", ex);
                    LoadRubrics();
                    tempResult = false;
                }
            }

            if (!tempResult)
            {
				_appShutdown = true;
				Log.Info("Приложение закрывается - не пройдена проверка лицензии");
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
