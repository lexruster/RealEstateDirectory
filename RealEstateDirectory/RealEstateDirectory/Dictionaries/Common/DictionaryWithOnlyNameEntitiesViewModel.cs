﻿using System;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.Common
{
	[NotifyForAll]
	public class DictionaryWithOnlyNameEntitiesViewModel<TEntityViewModel, TEntity> : DictionaryViewModel<TEntityViewModel, TEntity>
		where TEntity : BaseDictionary
		where TEntityViewModel : DictionaryWithOnlyNameEntityViewModel<TEntity>
	{
		public DictionaryWithOnlyNameEntitiesViewModel(IServiceLocator serviceLocator, IDictionaryWithOnlyNameEntitiesService<TEntity> dictionaryService, IMessageService messageService)
			: base(serviceLocator, messageService)
		{
			_DictionaryService = dictionaryService;
			

			PropertyChanged += (sender, args) =>
				{
					if (args.PropertyName == PropertySupport.ExtractPropertyName(() => Name))
						AddCommand.RaiseCanExecuteChanged();
				};
			_DictionaryService.StartSession();
		}

		#region Infrastructure

		private readonly IDictionaryWithOnlyNameEntitiesService<TEntity> _DictionaryService;

		#endregion

		public string Name { get; set; }

		public override string DictionaryName
		{
			get { return _DictionaryService.DictionaryName; }
		}

		protected override void InitializeEntities()
		{
			_Entities.AddRange(_DictionaryService.GetAll().Select(CreateNewViewModel));
		}

		protected override bool CanAdd()
		{
			return !String.IsNullOrWhiteSpace(Name) && _Entities.All(model => model.Name != Name);
		}

		protected override void ClearProperties()
		{
			Name = String.Empty;
		}

		protected override TEntity CreateNewModel()
		{
			return _DictionaryService.Create(Name);
		}

		protected override bool IsCanRemove(TEntityViewModel entityViewModel, out string errorText)
		{
			errorText = null;
			ValidationResult validation = _DictionaryService.IsPossibilityToDelete(entityViewModel.DbEntity);
			if (!validation.IsValid)
			{
				errorText = validation.GetReasons();
			}
			return validation.IsValid;
		}

		protected override void RemoveEntityFromDatabase(TEntity entity)
		{
			_DictionaryService.Delete(entity);
		}

		public override void OpenSession()
		{
			_DictionaryService.StartSession();
		}

		public override void CloseSession()
		{
			_DictionaryService.StopSession();
		}
	}
}