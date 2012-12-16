using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace RealEstateDirectory.Behaviors
{
	public class DataGridViewBoundSelectedItemsBehavior : Behavior<DataGrid>
	{
		protected override void OnAttached()
		{
			base.OnAttached();

			((ObservableCollection<object>)AssociatedObject.SelectedItems).CollectionChanged += GridSelectedItems_CollectionChanged;

			ResetSelectedItems();
		}

		protected override void OnDetaching()
		{
			((ObservableCollection<object>)AssociatedObject.SelectedItems).CollectionChanged -= GridSelectedItems_CollectionChanged;

			base.OnDetaching();
		}

		private void ResetSelectedItems()
		{
			if (SelectedItems == null)
				return;

			SelectedItems.Clear();
			foreach (var item in AssociatedObject.SelectedItems)
				SelectedItems.Add(item);
		}

		public static readonly DependencyProperty SelectedItemsProperty =
			DependencyProperty.Register("SelectedItems", typeof(IList), typeof(DataGridViewBoundSelectedItemsBehavior), new PropertyMetadata(default(IList), SelectedItems_Changed));

		private static void SelectedItems_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((DataGridViewBoundSelectedItemsBehavior)dependencyObject).ResetSelectedItems();
		}

		public IList SelectedItems
		{
			get { return (IList)GetValue(SelectedItemsProperty); }
			set { SetValue(SelectedItemsProperty, value); }
		}

		private void GridSelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (SelectedItems == null)
				return;

			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Remove:
					foreach (var item in e.OldItems)
						SelectedItems.Remove(item);
					break;
				case NotifyCollectionChangedAction.Add:
					foreach (var item in e.NewItems)
						SelectedItems.Add(item);
					break;
				case NotifyCollectionChangedAction.Replace:
					foreach (var item in e.OldItems)
						SelectedItems.Remove(item);
					foreach (var item in e.NewItems)
						SelectedItems.Add(item);
					break;
				case NotifyCollectionChangedAction.Reset:
					SelectedItems.Clear();
					foreach (var item in e.NewItems)
						SelectedItems.Add(item);
					break;
			}
		}
	}
}