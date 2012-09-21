using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace RealEstateDirectory.Services
{
	public class DataObservableCollection<T> : ObservableCollection<T>
	{
		public event EventHandler<CollectionChangingEventArgs<T>> CollectionChanging = delegate { };

		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
		{
			if (!IsNotificationsSuspended)
				base.OnCollectionChanged(args);
			else
				_IsDirty = true;
		}

		public void AddRange(IEnumerable<T> items)
		{
			foreach (var item in items)
				Add(item);
		}

		protected override void InsertItem(int index, T item)
		{
			if (IsNotificationsSuspended)
			{
				_IsDirty = true;
				return;
			}

			var collectionChangingEventArgs = new CollectionChangingEventArgs<T>(item, index, CollectionChangeAction.Add);
			CollectionChanging(this, collectionChangingEventArgs);
			if (collectionChangingEventArgs.Cancel)
				return;

			base.InsertItem(index, collectionChangingEventArgs.Item);
		}

		protected override void RemoveItem(int index)
		{
			if (IsNotificationsSuspended)
			{
				_IsDirty = true;
				return;
			}

			var collectionChangingEventArgs = new CollectionChangingEventArgs<T>(Items[index], index, CollectionChangeAction.Remove);
			CollectionChanging(this, collectionChangingEventArgs);
			if (collectionChangingEventArgs.Cancel)
				return;

			base.RemoveItem(index);
		}

		protected override void ClearItems()
		{
			if (IsNotificationsSuspended)
			{
				_IsDirty = true;
				return;
			}

			var collectionChangingEventArgs = new CollectionChangingEventArgs<T>(default(T), 0, CollectionChangeAction.Refresh);
			CollectionChanging(this, collectionChangingEventArgs);
			if (collectionChangingEventArgs.Cancel)
				return;

			base.ClearItems();
		}

		public void SuspendNotification()
		{
			IsNotificationsSuspended = true;
		}

		public void ResumeNotification()
		{
			IsNotificationsSuspended = false;
			if (!_IsDirty)
				return;
			_IsDirty = false;
			OnPropertyChanged(new PropertyChangedEventArgs("Count"));
			OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		private bool _IsDirty;

		public bool IsNotificationsSuspended { get; private set; }
	}
}