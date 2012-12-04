using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class WaterSupplyService : DictionaryService<WaterSupply>, IWaterSupplyService
    {
        #region Поля

		public override string DictionaryName
		{
			get { return "Водоснабжение"; }
		}

        #endregion

        #region Конструктор

        public WaterSupplyService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region Методы

		public override ValidationResult IsPossibilityToDelete(WaterSupply entity)
        {
			var result = new ValidationResult();
			if (!Repository.IsPossibleToDeleteWaterSupply(entity))
			{
				result.FailValidation("Элемент уже используется в системе");
			}

			return result;
        }

		public WaterSupply Create(string name)
		{
			return new WaterSupply(name);
		}

        #endregion
    }
}