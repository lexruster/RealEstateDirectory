using System.ComponentModel;
using System.Windows;
using Microsoft.Practices.Prism.ViewModel;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.Common
{
	public abstract class DictionaryEntityViewModel<T> : NotificationObject, IDataErrorInfo, IEditableObject
		where T : BaseDictionary
	{
		public abstract void UpdateValuesFromModel();

		public abstract void UpdateModelFromValues();
		public IMessageService _MessageService;

		public T DbEntity = null;

		public abstract string this[string columnName] { get; }

		public abstract string Error { get; }

		public virtual void InitMessageService(IMessageService messageService)
		{
			_MessageService = messageService;
		}

		public virtual void BeginEdit()
		{

		}

		public virtual void EndEdit()
		{
			var error = Error;

			if (error == null)
			{
				UpdateModelFromValues();
				SaveToDatabase();
			}
			else
			{
				CancelEdit();
				if (_MessageService != null)
				{
					_MessageService.ShowMessage(error, "Ошибка", image: MessageBoxImage.Error);
				}
			}
		}

		public virtual void CancelEdit()
		{
			UpdateValuesFromModel();
		}

		public abstract void SaveToDatabase();
	}
}