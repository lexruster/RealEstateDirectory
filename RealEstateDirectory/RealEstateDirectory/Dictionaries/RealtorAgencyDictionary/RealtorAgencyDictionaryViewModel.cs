using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.RealtorAgencyDictionary
{
	[NotifyForAll]
	public class RealtorAgencyDictionaryViewModel : DictionaryViewModel<RealtorAgencyViewModel, RealtorAgency>
	{
		public enum State
		{
			View = 1,
			Add = 2,
			Edit = 3
		}

		#region Конструктор

		public RealtorAgencyDictionaryViewModel(IServiceLocator serviceLocator, IRealtorAgencyService dictionaryService,
		                                        IMessageService messageService)
			: base(serviceLocator, messageService)
		{
			_DictionaryService = dictionaryService;
			UpdateState(State.View);
			PropertyChanged += (sender, args) =>
				{
					if (args.PropertyName == PropertySupport.ExtractPropertyName(() => Name) ||
					    args.PropertyName == PropertySupport.ExtractPropertyName(() => Director) ||
					    args.PropertyName == PropertySupport.ExtractPropertyName(() => Address) ||
					    args.PropertyName == PropertySupport.ExtractPropertyName(() => Contacts) ||
					    args.PropertyName == PropertySupport.ExtractPropertyName(() => Description))
					{
						OkCommand.RaiseCanExecuteChanged();
						CancelCommand.RaiseCanExecuteChanged();
					}

					if (args.PropertyName == PropertySupport.ExtractPropertyName(() => SelectedRealtorAgency))
						UpdateSelectedRealtorAgency();
				};
			_DictionaryService.StartSession();

			AddCommand = new DelegateCommand(() =>
				{
					ClearProperties();
					UpdateState(State.Add);
					ButtonUpdate();
				}, CanAdd);

			ChangeCommand = new DelegateCommand(() =>
				{
					UpdateState(State.Edit);
					ButtonUpdate();
				}, CanEdit);

			OkCommand = new DelegateCommand(() =>
				{
					if (StateEnum == State.Add)
					{
						var viewModel = CreateNewViewModel(CreateNewModel());
						var error = viewModel.Error;
						if (error == null)
						{
							viewModel.SaveToDatabase();
							_Entities.Add(viewModel);
							ClearProperties();
							UpdateState(State.View);
						}
						else
						{
							_MessageService.ShowMessage(error, "Ошибка", image: MessageBoxImage.Error);
						}
					}

					if (StateEnum == State.Edit)
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
							UpdateState(State.View);
						}
						else
						{
							SelectedRealtorAgency.UpdateValuesFromModel();
							_MessageService.ShowMessage(error, "Ошибка", image: MessageBoxImage.Error);
						}
					}

					ButtonUpdate();
				}, CanOk);

			CancelCommand = new DelegateCommand(() =>
				{
					if (StateEnum == State.Add)
					{
						ClearProperties();
					}

					if (StateEnum == State.Edit)
					{
						UpdateSelectedRealtorAgency();
					}

					UpdateState(State.View);
					ButtonUpdate();
				}, CanCancel);

			SelectAndChangeCommand = new DelegateCommand(() =>
				{
					UpdateSelectedRealtorAgency();
					UpdateState(State.Edit);
					ButtonUpdate();
				}, CanSelectAndChange);
		}

		#endregion

		#region Infrastructure

		private readonly IRealtorAgencyService _DictionaryService;

		#endregion

		#region Свойства  INotifi

		public string Name { get; set; }
		public string Director { get; set; }
		public string Description { get; set; }
		public string Contacts { get; set; }
		public string Address { get; set; }
		public RealtorAgencyViewModel SelectedRealtorAgency { get; set; }

		/// <summary>
		/// Текстовое представление состояний формы
		/// </summary>
		public string StateStr { get; set; }

		/// <summary>
		/// Форма в режиме чтения, грид активен, поля ввода не активны
		/// </summary>
		public bool ReadOnly { get; set; }

		/// <summary>
		/// Поля ввода автивны
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		/// Видимост секции управления коллекцией
		/// </summary>
		public Visibility CollectionChangeSectionVisibility { get; set; }

		/// <summary>
		/// Видимость секции редактирования
		/// </summary>
		public Visibility EditSectionVisibility { get; set; }

		#endregion

		#region Свойства

		/// <summary>
		/// Текущее состояние формы
		/// </summary>
		protected State StateEnum { get; set; }

		#endregion

		#region Методы

		private void ButtonUpdate()
		{
			AddCommand.RaiseCanExecuteChanged();
			ChangeCommand.RaiseCanExecuteChanged();
			OkCommand.RaiseCanExecuteChanged();
			CancelCommand.RaiseCanExecuteChanged();
			SelectAndChangeCommand.RaiseCanExecuteChanged();
		}

		private void UpdateState(State st)
		{
			StateEnum = st;
			switch (StateEnum)
			{
				case State.View:
					StateStr = "Просмотр";
					ReadOnly = true;
					Enabled = false;
					EditSectionVisibility = Visibility.Hidden;
					CollectionChangeSectionVisibility = Visibility.Visible;
					break;

				case State.Add:
					StateStr = "Добавление";
					ReadOnly = false;
					Enabled = true;
					EditSectionVisibility = Visibility.Visible;
					CollectionChangeSectionVisibility = Visibility.Hidden;
					break;

				case State.Edit:
					StateStr = "Изменение";
					ReadOnly = false;
					Enabled = true;
					EditSectionVisibility = Visibility.Visible;
					CollectionChangeSectionVisibility = Visibility.Hidden;
					break;
			}
		}

		private bool ViewModewlIsChanged()
		{
			return Name != SelectedRealtorAgency.Name ||
			       Address != SelectedRealtorAgency.Address ||
			       Contacts != SelectedRealtorAgency.Contacts ||
			       Description != SelectedRealtorAgency.Description ||
			       Director != SelectedRealtorAgency.Director;
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

		#endregion

		#region Команды

		public DelegateCommand ChangeCommand { get; protected set; }
		public DelegateCommand OkCommand { get; protected set; }
		public DelegateCommand CancelCommand { get; protected set; }
		public DelegateCommand SelectAndChangeCommand { get; protected set; }

		#endregion

		#region Методы проверки команд

		protected override bool CanAdd()
		{
			return ReadOnly;
		}

		protected bool CanSelectAndChange()
		{
			return ReadOnly;
		}

		protected bool CanEdit()
		{
			return SelectedRealtorAgency != null && ReadOnly;
		}

		protected bool CanOk()
		{
			if (!ReadOnly)
			{
				if (StateEnum == State.Add)
				{
					return !String.IsNullOrEmpty(Name);
				}
				if (StateEnum == State.Edit)
				{
					return SelectedRealtorAgency != null && !String.IsNullOrEmpty(Name) && ViewModewlIsChanged();
				}
			}

			return false;
		}

		protected bool CanCancel()
		{
			return !ReadOnly;
		}

		#endregion

		#region Перегрузки

		public override string DictionaryName
		{
			get { return _DictionaryService.DictionaryName; }
		}

		protected override void InitializeEntities()
		{
			_Entities.AddRange(_DictionaryService.GetAll().Select(CreateNewViewModel));
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
			var newRealtorAgency = new RealtorAgency(Name)
				{
					Director = Director,
					Description = Description,
					Contacts = Contacts,
					Address = Address,
				};

			return newRealtorAgency;
		}

		protected override bool IsCanRemove(RealtorAgencyViewModel entityViewModel, out string errorText)
		{
			errorText = null;

			ValidationResult validation = _DictionaryService.IsPossibilityToDelete(entityViewModel.DbEntity);
			if (!validation.IsValid)
			{
				errorText = validation.GetReasons();
			}

			return validation.IsValid;
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

		#endregion
	}
}