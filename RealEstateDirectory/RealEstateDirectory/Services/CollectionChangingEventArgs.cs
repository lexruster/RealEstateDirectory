using System;
using System.ComponentModel;

namespace RealEstateDirectory.Services
{
	public class CollectionChangingEventArgs<T> : EventArgs
	{
		public CollectionChangingEventArgs(T item, int index, CollectionChangeAction action)
		{
			Cancel = false;
			Item = item;
			Index = index;
			Action = action;
		}

		public bool Cancel { get; set; }

		public T Item { get; set; }

		public int Index { get; private set; }

		public CollectionChangeAction Action { get; private set; }
	}
}