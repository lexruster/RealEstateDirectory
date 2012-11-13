using System;
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

namespace RealEstateDirectory.Dictionaries.RealtorAgencyDictionary
{
	[NotifyForAll]
	public class RealtorAgencyDictionaryViewModel : DictionaryViewModel<RealtorAgencyViewModel, RealtorAgency>
	{
		public RealtorAgencyDictionaryViewModel(IServiceLocator serviceLocator, IRealtorAgencyService dictionaryService,
		                                        IMessageService messageService)
			: base(serviceLocator, messageService)
		{
			_DictionaryService = dictionaryService;

			PropertyChanged += (sender, args) =>
				{
					if (args.PropertyName == PropertySupport.ExtractPropertyName(() => Name) ||
					    args.PropertyName == PropertySupport.ExtractPropertyName(() => Director) ||
					    args.PropertyName == PropertySupport.ExtractPropertyName(() => Address) ||
					    args.PropertyName == PropertySupport.ExtractPropertyName(() => Contacts) ||
					    args.PropertyName == PropertySupport.ExtractPropertyName(() => Description))
						ChangeCommand.RaiseCanExecuteChanged();

					if (args.PropertyName == PropertySupport.ExtractPropertyName(() => SelectedRealtorAgency))
						UpdateSelectedRealtorAgency();
				};
			_DictionaryService.StartSession();

			AddCommand = new DelegateCommand(() =>
				{
					var viewModel = CreateNewViewModel(CreateNewModel());
					_Entities.Add(viewModel);
					SelectedRealtorAgency = viewModel;
				}, CanAdd);

			ChangeCommand = new DelegateCommand(() =>
				{
					SelectedRealtorAgency.Name = Name;
					SelectedRealtorAgency.Address = Address;
					SelectedRealtorAgency.Contacts = Contacts;
					SelectedRealtorAgency.Description = Description;
					SelectedRealtorAgency.Director = Director;
					var error = SelectedRealtorAgency.Error;

					if (error == null)
					{
						SelectedRealtorAgency.UpdateModelFromValues();
						SelectedRealtorAgency.SaveToDatabase();
						ClearProperties();
					}
					else
					{
						SelectedRealtorAgency.UpdateValuesFromModel();
						_MessageService.ShowMessage(error, "Ошибка", image: MessageBoxImage.Error);
					}
					ChangeCommand.RaiseCanExecuteChanged();
				}, CanEdit);
		}

		#region Infrastructure

		private readonly IRealtorAgencyService _DictionaryService;

		#endregion

		public string Name { get; set; }
		public string Director { get; set; }
		public string Description { get; set; }
		public string Contacts { get; set; }
		public string Address { get; set; }
		public RealtorAgencyViewModel SelectedRealtorAgency { get; set; }

		public DelegateCommand ChangeCommand { get; protected set; }

		public override string DictionaryName
		{
			get { return _DictionaryService.DictionaryName; }
		}

		protected override void InitializeEntities()
		{
			_Entities.AddRange(_DictionaryService.GetAll().Select(CreateNewViewModel));
		}

		protected void UpdateSelectedRealtorAgency()
		{
			if (SelectedRealtorAgency != null)
			{
				Name = SelectedRealtorAgency.Name;
				Address = SelectedRealtorAgency.Address;
				Contacts = SelectedRealtorAgency.Contacts;
				Description = SelectedRealtorAgency.Description;
				Director = SelectedRealtorAgency.Director;
				ChangeCommand.RaiseCanExecuteChanged();
			}
		}

		protected override bool CanAdd()
		{
			return true;
		}

		protected bool CanEdit()
		{
			return SelectedRealtorAgency != null && !String.IsNullOrEmpty(Name) && ViewModewlIsChanged();
		}

		private bool ViewModewlIsChanged()
		{
			return Name != SelectedRealtorAgency.Name ||
			       Address != SelectedRealtorAgency.Address ||
			       Contacts != SelectedRealtorAgency.Contacts ||
			       Description != SelectedRealtorAgency.Description ||
			       Director != SelectedRealtorAgency.Director;
		}

		protected override void ClearProperties()
		{
			Name = String.Empty;
			Director = String.Empty;
			Description = String.Empty;
			Contacts = String.Empty;
			Address = String.Empty;
		}

		protected override RealtorAgency CreateNewModel()
		{
			var newRealtorAgency = new RealtorAgency("Новое агентство");

			return newRealtorAgency;
		}

		protected override bool IsCanRemove(RealtorAgencyViewModel entityViewModel, out string errorText)
		{
			errorText = null;
			return _DictionaryService.IsPossibilityToDelete(entityViewModel.DbEntity);
		}

		protected override void RemoveEntityFromDatabase(RealtorAgency entity)
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