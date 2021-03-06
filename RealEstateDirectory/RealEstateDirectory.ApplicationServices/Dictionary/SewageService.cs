using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class SewageService : DictionaryService<Sewage>, ISewageService
    {
        #region ����

		public override string DictionaryName
		{
			get { return "�����������"; }
		}

        #endregion

        #region �����������

        public SewageService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region ������

		public override ValidationResult IsPossibilityToDelete(Sewage entity)
        {
			var result = new ValidationResult();
			if (!Repository.IsPossibleToDeleteSewage(entity))
			{
				result.FailValidation("������� ��� ������������ � �������");
			}

			return result;
        }

		public Sewage Create(string name)
		{
			return new Sewage(name);
		}

        #endregion
    }
}