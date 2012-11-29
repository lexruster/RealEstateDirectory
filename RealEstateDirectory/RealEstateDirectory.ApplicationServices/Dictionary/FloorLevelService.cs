using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class FloorLevelService : DictionaryService<FloorLevel>, IFloorLevelService
    {
        #region Поля
		
		public override string DictionaryName
		{
			get { return "Тип потолка"; }
		}

        #endregion

        #region Конструктор

        public FloorLevelService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region Методы

		public override ValidationResult IsPossibilityToDelete(FloorLevel entity)
        {
			var result = new ValidationResult();
			if (!Repository.IsPossibleToDeleteFloorLevel(entity))
			{
				result.FailValidation("Элемент уже используется в системе");
			}

			return result;
        }

		public FloorLevel Create(string name)
		{
			return new FloorLevel(name);
		}

        #endregion
    }
}