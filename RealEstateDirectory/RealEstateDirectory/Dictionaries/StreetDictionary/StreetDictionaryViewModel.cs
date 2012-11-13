﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.StreetDictionary
{
	[NotifyForAll]
	public class StreetDictionaryViewModel : DictionaryViewModel<StreetViewModel, Street>
	{
		public StreetDictionaryViewModel(IServiceLocator serviceLocator, IStreetService dictionaryService,
		                                 IDistrictService districtService, IMessageService messageService)
			: base(serviceLocator, messageService)
		{
			_DictionaryService = dictionaryService;
			_DistrictService = districtService;

			PropertyChanged += (sender, args) =>
				{
					if (args.PropertyName == PropertySupport.ExtractPropertyName(() => Name))
						AddCommand.RaiseCanExecuteChanged();

					if (args.PropertyName == PropertySupport.ExtractPropertyName(() => DistrictChanged) ||
					    args.PropertyName == PropertySupport.ExtractPropertyName(() => NameChanged))
						ChangeCommand.RaiseCanExecuteChanged();

					if (args.PropertyName == PropertySupport.ExtractPropertyName(() => SelectedDistrict))
						UpdateCollection();

					if (args.PropertyName == PropertySupport.ExtractPropertyName(() => SelectedStreet))
						UpdateSelectedStreet();
				};
			_DictionaryService.StartSession();

			ChangeCommand = new DelegateCommand(() =>
				{
					SelectedStreet.Name = NameChanged;
					SelectedStreet.District = DistrictChanged;
					var error = SelectedStreet.Error;

					if (error == null)
					{
						SelectedStreet.UpdateModelFromValues();
						SelectedStreet.SaveToDatabase();
						ClearProperties();
						UpdateCollection();
					}
					else
					{
						SelectedStreet.UpdateValuesFromModel();
						_MessageService.ShowMessage(error, "Ошибка", image: MessageBoxImage.Error);
					}
					ChangeCommand.RaiseCanExecuteChanged();
				}, CanEdit);
		}

		#region Infrastructure

		private readonly IStreetService _DictionaryService;
		private readonly IDistrictService _DistrictService;

		#endregion

		public string Name { get; set; }
		public District SelectedDistrict { get; set; }
		public List<District> DistrictList { get; set; }
		public StreetViewModel SelectedStreet { get; set; }

		public string NameChanged { get; set; }
		public District DistrictChanged { get; set; }

		public DelegateCommand ChangeCommand { get; protected set; }

		public override string DictionaryName
		{
			get { return _DictionaryService.DictionaryName; }
		}

		protected override void InitializeEntities()
		{
			DistrictList = _DistrictService.GetAll().ToList();
			SelectedDistrict = DistrictList.Any() ? DistrictList.First() : null;
			UpdateCollection();
		}

		protected void UpdateCollection()
		{
			ClearProperties();
			_Entities.Clear();
			_Entities.AddRange(_DictionaryService.GetAll().Where(x => x.District == SelectedDistrict).Select(CreateNewViewModel));
		}

		protected void UpdateSelectedStreet()
		{
			if (SelectedStreet != null)
			{
				NameChanged = SelectedStreet.Name;
				DistrictChanged = SelectedStreet.District;
			}
		}

		protected override bool CanAdd()
		{
			return !String.IsNullOrWhiteSpace(Name) && _Entities.All(model => model.Name != Name) && SelectedDistrict != null;
		}

		protected bool CanEdit()
		{
			return SelectedStreet != null && !String.IsNullOrEmpty(NameChanged) && DistrictChanged != null &&
			       (NameChanged != SelectedStreet.Name || DistrictChanged != SelectedStreet.District);
		}

		protected override void ClearProperties()
		{
			Name = String.Empty;
			NameChanged = String.Empty;
			DistrictChanged = null;
		}

		protected override Street CreateNewModel()
		{
			var newStreet = new Street(Name) {District = SelectedDistrict};

			return newStreet;
		}

		protected Street CreateChangedModel()
		{
			var newStreet = new Street(Name) {District = SelectedDistrict};

			return newStreet;
		}

		protected override bool IsCanRemove(StreetViewModel entityViewModel, out string errorText)
		{
			errorText = null;
			return _DictionaryService.IsPossibilityToDelete(entityViewModel.DbEntity);
		}

		protected override void RemoveEntityFromDatabase(Street entity)
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