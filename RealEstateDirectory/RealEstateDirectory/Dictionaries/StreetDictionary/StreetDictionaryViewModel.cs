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

namespace RealEstateDirectory.Dictionaries.StreetDictionary
{
	[NotifyForAll]
	public class StreetDictionaryViewModel : DictionaryViewModel<StreetViewModel, Street>
	{
		public enum State
		{
			View = 1,
			Add = 2,
			Edit = 3
		}

		#region Конструктор

		public StreetDictionaryViewModel(IServiceLocator serviceLocator, IStreetService dictionaryService,
												IMessageService messageService, IDistrictService districtService)
			: base(serviceLocator, messageService)
		{
			_DictionaryService = dictionaryService;
			_DistrictService = districtService;
			UpdateState(State.View);
			PropertyChanged += (sender, args) =>
				{
					if (args.PropertyName == PropertySupport.ExtractPropertyName(() => Name) ||
						args.PropertyName == PropertySupport.ExtractPropertyName(() => District))
					{
						OkCommand.RaiseCanExecuteChanged();
						CancelCommand.RaiseCanExecuteChanged();
					}

					if (args.PropertyName == PropertySupport.ExtractPropertyName(() => SelectedDistrict))
						UpdateCollection();

					if (args.PropertyName == PropertySupport.ExtractPropertyName(() => SelectedStreet))
						UpdateSelectedStreet();
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
							UpdateCollection();
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
						SelectedStreet.Name = Name;
						SelectedStreet.District = District;
						var error = SelectedStreet.Error;

						if (error == null)
						{
							SelectedStreet.UpdateModelFromValues();
							SelectedStreet.SaveToDatabase();
							UpdateCollection();
							UpdateState(State.View);
						}
						else
						{
							SelectedStreet.UpdateValuesFromModel();
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
						UpdateSelectedStreet();
					}

					UpdateState(State.View);
					ButtonUpdate();
				}, CanCancel);

			SelectAndChangeCommand = new DelegateCommand(() =>
				{
					UpdateSelectedStreet();
					UpdateState(State.Edit);
					ButtonUpdate();
				}, CanSelectAndChange);
		}

		#endregion

		#region Infrastructure

		private readonly IStreetService _DictionaryService;
		private readonly IDistrictService _DistrictService;
		

		#endregion

		#region Свойства  INotify

		public string Name { get; set; }
		public District District { get; set; }
		public StreetViewModel SelectedStreet
		{
			get { return _selectedStreet; }
			set
			{
				if (_selectedStreet == value || value == null)
					return;
				_selectedStreet = value;
				RaisePropertyChanged(PropertySupport.ExtractPropertyName(() => SelectedStreet));
			}
		}

		public District SelectedDistrict { get; set; }
		public List<District> DistrictList { get; set; }

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
		private StreetViewModel _selectedStreet;

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
			return Name != SelectedStreet.Name ||
				   District != SelectedStreet.District;
		}

		protected void UpdateSelectedStreet()
		{
			if (SelectedStreet != null)
			{
				Name = SelectedStreet.Name;
				District = SelectedStreet.District;
				ChangeCommand.RaiseCanExecuteChanged();
			}
		}

		protected void UpdateCollection()
		{
			ClearProperties();
			_Entities.Clear();
			_Entities.AddRange(_DictionaryService.GetAll().Where(x => x.District == SelectedDistrict).Select(CreateNewViewModel));
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
			return SelectedStreet != null && ReadOnly;
		}

		protected bool CanOk()
		{
			if (!ReadOnly)
			{
				if (StateEnum == State.Add)
				{
					return !String.IsNullOrEmpty(Name) && District != null;
				}
				if (StateEnum == State.Edit)
				{
					return SelectedStreet != null && !String.IsNullOrEmpty(Name) && District != null && ViewModewlIsChanged();
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
			DistrictList = _DistrictService.GetAll().ToList();
			SelectedDistrict = DistrictList.Any() ? DistrictList.First() : null;
			UpdateCollection();
		}

		protected override void ClearProperties()
		{
			Name = String.Empty;
			District = null;
		}

		protected override Street CreateNewModel()
		{
			var newStreet = new Street(Name)
				{
					District = District
				};

			return newStreet;
		}

		protected override bool IsCanRemove(StreetViewModel entityViewModel, out string errorText)
		{
			errorText = null;

			ValidationResult validation = _DictionaryService.IsPossibilityToDelete(entityViewModel.DbEntity);
			if (!validation.IsValid)
			{
				errorText = validation.GetReasons();
			}

			return validation.IsValid;
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

		#endregion
	}
}