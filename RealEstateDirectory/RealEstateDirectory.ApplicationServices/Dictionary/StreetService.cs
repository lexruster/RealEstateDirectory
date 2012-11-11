using System;
using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class StreetService : DictionaryService<Street>, IStreetService
    {
        #region Поля

		public override string DictionaryName
		{
			get { return "Улицы"; }
		}

        #endregion

        #region Конструктор

        public StreetService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region Методы

        public override bool IsPossibilityToDelete(Street entity)
        {
            return Repository.IsPossibleToDeleteStreet(entity);
        }

        public override ValidationResult IsValid(Street entity, int id = 0)
        {
            var result = new ValidationResult();

            if (entity.District==null)
            {
                result.FailValidation("Нужно указать район.");
            }
            
            if (!IsNameUniquenessInner(entity, id))
            {
                result.FailValidation(String.Format("Элемент справочника \"{0}\" со значением \"{1}\" уже существует.",
                                                    DictionaryName, entity.Name));
            }

            return result;
        }

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(Street entity)
        {
            using (var transaction = PersistenceContext.CurrentSession.BeginTransaction())
            {
                var distict = entity.District;
                //Не обязательно удалять из коллекции, но лучше удалить...
                distict.Streets.Remove(entity);
                //Необходимо отвязть от района перед удалением
                entity.District = null;
                Repository.Delete(entity);

                transaction.Commit();
            }
        }

        #endregion
    }
}