using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries
{
	public abstract class TemplatedDictionaryViewModel<TEntity> : DictionaryViewModel
	{
		protected TemplatedDictionaryViewModel(IDataService dataService) : base(dataService)
		{
		}

		protected override void InitializeEntities()
		{
			_DataEntities = _DataService.GetEntityLink<TEntity>();
			Entities = new ListCollectionView(new ReadOnlyObservableCollection<TEntity>(_DataEntities));
			Entities.CurrentChanged += Entities_CurrentChanged;
			Entities.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
		}

		protected ObservableCollection<TEntity> _DataEntities;
	}
}