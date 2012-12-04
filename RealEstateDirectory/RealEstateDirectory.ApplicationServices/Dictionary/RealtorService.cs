using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class RealtorService : DictionaryService<Realtor>, IRealtorService
    {
        #region ����

		public override string DictionaryName
		{
			get { return "��������"; }
		}

        #endregion

        #region �����������

        public RealtorService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region ������

		public override ValidationResult IsPossibilityToDelete(Realtor entity)
        {
			var result = new ValidationResult();
			if (!Repository.IsPossibleToDeleteRealtor(entity))
			{
				result.FailValidation("������� ��� ������������ � �������");
			}

			return result;
        }

        #endregion
    }
}