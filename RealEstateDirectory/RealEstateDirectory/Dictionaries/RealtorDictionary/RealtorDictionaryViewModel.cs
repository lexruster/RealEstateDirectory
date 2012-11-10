using System;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.RealtorDictionary
{
	[NotifyForAll]
	public class RealtorDictionaryViewModel : DictionaryViewModel<RealtorViewModel, Realtor>
	{
		public RealtorDictionaryViewModel(IServiceLocator serviceLocator, IRealtorService dictionaryService, IMessageService messageService)
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

		private readonly IRealtorService _DictionaryService;

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

		protected override Realtor CreateNewModel()
		{
			return new Realtor(Name);
		}

		protected override bool IsCanRemove(RealtorViewModel entityViewModel, out string errorText)
		{
			errorText = null;
			return _DictionaryService.IsPossibilityToDelete(entityViewModel.DbEntity);
		}

		protected override void RemoveEntityFromDatabase(Realtor entity)
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