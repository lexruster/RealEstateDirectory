using System;
using System.Windows;
using RealEstateDirectory.Interfaces;

namespace RealEstateDirectory.Dictionaries.Common
{
	public class DictionaryWindow : Window
	{
		public DictionaryWindow()
		{
			Closing += (sender, args) =>
				{
					if (DataContext == null || !(DataContext is ISessionedViewModel))
						return;
					try
					{
						((ISessionedViewModel) DataContext).CloseSession();
					}
					catch (Exception exc)
					{
						MessageBox.Show(exc.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
					}
				};
		}
	}
}