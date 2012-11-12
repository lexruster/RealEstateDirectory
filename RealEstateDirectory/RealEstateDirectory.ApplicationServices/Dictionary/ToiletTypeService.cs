using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class ToiletTypeService : DictionaryService<ToiletType>, IToiletTypeService
    {
        #region Поля

		public override string DictionaryName
		{
			get { return "Сан. узел"; }
		}

        #endregion

        #region Конструктор

        public ToiletTypeService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override bool IsPossibilityToDelete(ToiletType entity)
        {
            return Repository.IsPossibleToDeleteToiletType(entity);
        }

		public ToiletType Create(string name)
		{
			return new ToiletType(name);
		}

        #endregion
    }
}