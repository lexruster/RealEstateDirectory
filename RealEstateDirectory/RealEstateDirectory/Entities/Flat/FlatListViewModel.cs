using System;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.RealtorDictionary
{
	[NotifyForAll]
	public class FlatListViewModel : RealEstateListViewModel<FlatViewModel, Flat>
	{
		public FlatListViewModel(IServiceLocator serviceLocator, IFlatService service, IMessageService messageService)
			: base(serviceLocator, messageService)
		{
			_Service = service;

			PropertyChanged += (sender, args) =>
				{
					//if (args.PropertyName == PropertySupport.ExtractPropertyName(() => SelectedFlat))
						//AddCommand.RaiseCanExecuteChanged();
				};

			_Service.StartSession();
		}

		#region Infrastructure

		private readonly IFlatService _Service;

		#endregion

		public Flat SelectedFlat { get; set; }

		public override string RealEstateListName
	    {
			get { return _Service.RealEstateName; }
	    }

		protected override void InitializeEntities()
		{
			_Entities.AddRange(_Service.GetAll().Select(CreateNewViewModel));
		}

		protected override bool CanAdd()
		{
			//return !String.IsNullOrWhiteSpace(Name) && _Entities.All(model => model.Name != Name);
			return true;
		}

		protected override void ClearProperties()
		{
			
		}

		protected override Flat CreateNewModel()
		{
			return new Flat();
		}

		protected override bool IsCanRemove(FlatViewModel entityViewModel, out string errorText)
		{
			errorText = null;
			return _Service.IsPossibilityToDelete(entityViewModel.DbEntity);
		}

		protected override void RemoveEntityFromDatabase(Flat entity)
		{
			_Service.Delete(entity);
		}

		public override void OpenSession()
		{
			_Service.StartSession();
		}

		public override void CloseSession()
		{
			_Service.StopSession();
		}
	}
}