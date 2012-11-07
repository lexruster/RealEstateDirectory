using System;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.Dictionaries.CommonDictionary
{
	public class CommonDictionaryViewModel<S> : DictionaryViewModel<CommonViewModel<S> , S> where S : BaseDictionary
	{
        public CommonDictionaryViewModel(IServiceLocator serviceLocator, IDictionaryService<S> dictionaryService, IMessageService messageService)
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

        private readonly IDictionaryService<S> _DictionaryService;

		#endregion

		public string Name { get; set; }

		protected override void InitializeEntities()
		{
            _Entities.AddRange(_DictionaryService.GetAll().Select(CreateNewViewModel));
		}

		protected override bool CanAdd()
		{
			return !String.IsNullOrWhiteSpace(Name) && _Entities.All(model => model.Name != Name);
		}

		protected override S CreateNewModel()
		{
            return (S)Activator.CreateInstance(typeof(S), Name);
		}

	    protected override bool IsCanRemove(CommonViewModel<S> entity, out string errorText)
	    {
            errorText = null;
            return _DictionaryService.IsPossibilityToDelete(entity.DbEntity);
	    }

	    protected override void RemoveEntityFromDatabase(S entity)
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