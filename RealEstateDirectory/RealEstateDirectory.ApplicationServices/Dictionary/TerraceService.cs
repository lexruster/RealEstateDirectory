using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class TerraceService : DictionaryService<Terrace>, ITerraceService
    {
        #region Поля

		public override string DictionaryName
		{
			get { return "Балкон"; }
		}

        #endregion

        #region Конструктор

        public TerraceService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override ValidationResult IsPossibilityToDelete(Terrace entity)
        {
			var result = new ValidationResult();
			if (!Repository.IsPossibleToDeleteTerrace(entity))
			{
				result.FailValidation("Элемент уже используется в системе");
			}

			return result;
        }

		public Terrace Create(string name)
		{
			return new Terrace(name);
		}

        #endregion
    }
}