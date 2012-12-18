using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using RealEstateDirectory.Utils;

namespace RealEstateDirectory.Services
{
	public class UpdateService : IUpdateService
	{
		private DispatcherTimer _timer;
		private readonly IMessageService _messageService;

		public UpdateService(IMessageService messageService)
		{
			_messageService = messageService;
		}

		public void StartPeriodicCheck()
		{
			_timer = new DispatcherTimer();
			_timer.Tick += timerTick;
			_timer.Interval = new TimeSpan(0, 0, 5);
			_timer.Start();
		}

		private void timerTick(object sender, EventArgs e)
		{
			_timer.Stop();
			var thread = new Thread(CheckUpdatesOnTimer);
			thread.Start();
		}

		private void CheckUpdatesOnTimer()
		{
			CheckUpdates(false);
		}

		public void CheckUpdates(bool showSucces)
		{
			try
			{
				var webRequest = new HTTP();
				var remoteVersion =
					Version.Parse(webRequest.ReadFromServer(ConfigurationManager.AppSettings["GetVersionUrl"], 10000));
				var localVersion = Assembly.GetExecutingAssembly().GetName().Version;

				if (remoteVersion.CompareTo(localVersion) > 0)
				{
					var message =
						String.Format(
							"Вышла новая версия ПО. Ваша версия {0}, новая версия {1}. Рекомендуется обновиться. Скачать новую версию прямо сейчас?",
							localVersion, remoteVersion);
					if (_messageService.ShowMessage(message, "Новая версия", image: MessageBoxImage.Information,
					                                buttons: MessageBoxButton.OKCancel) == MessageBoxResult.OK)
					{
						Process.Start(ConfigurationManager.AppSettings["UpdateUrl"]);
					}
				}
				else
				{
					if (showSucces)
					{
						_messageService.ShowMessage("Вы используете самую последнюю версию.", "Программа обновлена",
						                            image: MessageBoxImage.Information);
					}
				}
			}
			catch (Exception)
			{
				if (showSucces)
				{
					_messageService.ShowMessage("Ошибка определения наличия обновлений.", "Ошибка", image: MessageBoxImage.Error);
				}
			}

			_timer.Interval = new TimeSpan(0, 30, 0);
			_timer.Start();
		}
	}
}