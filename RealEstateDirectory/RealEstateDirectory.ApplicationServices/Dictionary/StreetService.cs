using System;
using System.Linq;
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

		public override ValidationResult IsPossibilityToDelete(Street entity)
        {
			var result = new ValidationResult();
			if (!Repository.IsPossibleToDeleteStreet(entity))
			{
				result.FailValidation("������� ��� ������������ � �������");
			}

			return result;
        }

        public override ValidationResult IsValid(Street entity, int id = 0)
        {
            var result = new ValidationResult();

            if (entity.District==null)
            {
                result.FailValidation("����� ������� �����.");
	            return result;
            }

			if (entity.District.Streets.Any(x => x.Name.ToLower() == entity.Name.ToLower()))
			{
				result.FailValidation(String.Format("� ������ \"{0}\" ��� ���� ����� � ��������� \"{1}\".",
													entity.District.Name, entity.Name));
			}

            return result;
        }
		
		public override void Save(Street entity)
		{
			using (var transaction = PersistenceContext.CurrentSession.BeginTransaction())
			{
				if (entity.Id == 0)
				{
					var distict = entity.District;
					distict.Streets.Add(entity);
					Repository.SaveOrUpdate(distict);
				}
				Repository.SaveOrUpdate(entity);

				transaction.Commit();
			}
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