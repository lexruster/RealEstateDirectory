using System;
using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries
{
	public abstract class DictionaryEntityViewModel<T> : NotificationObject, IDataErrorInfo where T: class
	{
		protected DictionaryEntityViewModel()
		{
			PropertyChanged += (sender, args) =>
				{
					if (DbEntity != null)
						This_PropertyChanged(args.PropertyName);
				};
		}

		protected abstract void This_PropertyChanged(string propertyName);

		public abstract void AssociateWithModel(T entity);

		public T DbEntity = null;

		public abstract bool IsValid();

		public abstract bool CanSaveChange(out string errorText);

		public abstract string this[string columnName] { get; }

		public string Error { get; private set; }
	}
}