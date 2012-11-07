using System.ComponentModel;
using Microsoft.Practices.Prism.ViewModel;
using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Dictionaries
{
	public abstract class DictionaryEntityViewModel<T> : NotificationObject, IDataErrorInfo, IEditableObject where T: BaseDictionary
	{
		public abstract void UpdateValuesFromModel();

		public abstract void UpdateModelFromValues();

		public T DbEntity = null;

		public abstract string this[string columnName] { get; }

		public abstract string Error { get; }

		public virtual void BeginEdit()
		{
			
		}

		public virtual void EndEdit()
		{
			UpdateModelFromValues();
			SaveToDatabase();
		}

		public virtual void CancelEdit()
		{
			UpdateValuesFromModel();
		}

		public abstract void SaveToDatabase();
	}
}