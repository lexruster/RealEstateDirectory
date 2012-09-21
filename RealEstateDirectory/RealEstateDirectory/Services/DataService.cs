using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using RealEstateDirectory.Data.Entities;
using RealEstateDirectory.Dictionaries;

namespace RealEstateDirectory.Services
{
	public class DataService : IDataService
	{
		public DataService()
		{
		}

		private DataObservableCollection<StreetViewModel> _StreetsLink = null;

		void StreetsLink_CollectionChanging(object sender, CollectionChangingEventArgs<StreetViewModel> e)
		{
			switch (e.Action)
			{
				case CollectionChangeAction.Add:
					//TODO: Добавление объекта в сессию и замена на его прокси-версию e.Item.
					Debug.WriteLine("Remove Street object from session.");
					break;
				case CollectionChangeAction.Remove:
					//TODO: Удаление объекта из сессии.
					Debug.WriteLine("Remove Street object from session.");
					break;
			}
		}

		private void InitStreetLink()
		{
			_StreetsLink = new DataObservableCollection<StreetViewModel>();
			//TODO: Выборка всех Street-ов из базы.
			Debug.WriteLine("Select all Street from database.");
			_StreetsLink.CollectionChanging += StreetsLink_CollectionChanging;
		}

		public ObservableCollection<StreetViewModel> GetStreetsLink()
		{
			if (_StreetsLink == null)
				InitStreetLink();
			return _StreetsLink;
		}

		private T ToProxyReplacer<T>(T entity)
		{
			//TODO: Добавление в сессию.
			Debug.WriteLine("Add pure {0} object to session and change it to proxy version.", entity.GetType().Name);
			return entity;
		}

		public ObservableCollection<T> GetEntityLink<T>() where T : class
		{
			if (typeof(T) == typeof(StreetViewModel))
				return (ObservableCollection<T>) _StreetsLink;
		}

		public bool IsCorrect<T>(T entity, out string validateMessage)
		{
			//TODO: Серьёзная валидация.
			Debug.WriteLine("Validation {0} object.", entity);
			validateMessage = String.Empty;
			return true;
		}
	}
}