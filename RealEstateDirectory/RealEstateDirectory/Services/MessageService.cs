﻿using System.Windows;

namespace RealEstateDirectory.Services
{
	public class MessageService : IMessageService
	{
		public MessageBoxResult ShowMessage(string text, string title, MessageBoxButton buttons = MessageBoxButton.OK, MessageBoxImage image = MessageBoxImage.None)
		{
			return MessageBox.Show(text, title, buttons, image);
		}

		public bool ShowConfirm(string text, string title)
		{
			return MessageBox.Show(text, title, MessageBoxButton.OKCancel, MessageBoxImage.Question)==MessageBoxResult.OK;
		}
	}
}