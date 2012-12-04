using System;
using System.Windows;
using ActiveLock3_6NET;

namespace RealEstateDirectory.Misc
{
	/// <summary>
	/// Interaction logic for RegisterWindow.xaml
	/// </summary>
	public partial class RegisterWindow : Window
	{
        private readonly _IActiveLock _activeLock;
        public RegisterWindow(_IActiveLock activeLock)
		{
            _activeLock = activeLock;
			InitializeComponent();
		}

        private void Generate(object sender, RoutedEventArgs e)
		{
            var text = txtUser.Text;
            txtKey.Text = "";
            if (!String.IsNullOrEmpty(text))
            {
                try
                {
                    var key = _activeLock.get_InstallationCode(text);
                    txtKey.Text = key;
                }
                catch (Exception ex)
                {
                    txtKey.Text = String.Format("Ошибка получения ключа {0}.", ex.Message);
                }
            }
            else
            {
                MessageBox.Show(@"Введите имя пользователя.", @"Внимание");
            }
		}
	}
}