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
        #region ����

		public override string DictionaryName
		{
			get { return "�����"; }
		}

        #endregion

        #region �����������

        public StreetService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region ������

        public override bool IsPossibilityToDelete(Street entity)
        {
            return Repository.IsPossibleToDeleteStreet(entity);
        }

        public override ValidationResult IsValid(Street entity, int id = 0)
        {
            var result = new ValidationResult();

            if (entity.District==null)
            {
                result.FailValidation("����� ������� �����.");
            }
            
            if (!IsNameUniquenessInner(entity, id))
            {
                result.FailValidation(String.Format("������� ����������� \"{0}\" �� ��������� \"{1}\" ��� ����������.",
                                                    DictionaryName, entity.Name));
            }

            return result;
        }

        /// <summary>
        /// ������� ��������
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(Street entity)
        {
            using (var transaction = PersistenceContext.CurrentSession.BeginTransaction())
            {
                var distict = entity.District;
                //�� ����������� ������� �� ���������, �� ����� �������...
                distict.Streets.Remove(entity);
                //���������� ������� �� ������ ����� ���������
                entity.District = null;
                Repository.Delete(entity);

                transaction.Commit();
            }
        }

        #endregion
    }
}