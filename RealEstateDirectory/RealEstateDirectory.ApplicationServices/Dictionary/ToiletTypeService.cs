using System.Linq;
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
        #endregion

        #region Конструктор

        protected ToiletTypeService(IPersistenceContext persistenceContext, IDictionaryRepository<ToiletType> repository, IServiceLocator serviceLocator)
            : base(persistenceContext, repository, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override bool IsPossibilityToDelete(ToiletType entity)
        {
            return FlatService.GetQueryable().Count(x => x.ToiletType == entity) == 0;
        }

        #endregion
    }
}