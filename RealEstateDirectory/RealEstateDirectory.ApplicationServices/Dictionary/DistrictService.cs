using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class DistrictService : DictionaryService<District>, IDistrictService
    {
        #region ����

        public override string DictionaryName
        {
            get { return "������"; }
        }

        #endregion

        #region �����������

        public DistrictService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region ������

		public override ValidationResult IsPossibilityToDelete(District entity)
        {
			var result = new ValidationResult();
			if (!Repository.IsPossibleToDeleteDistrict(entity))
			{
				result.FailValidation("������� ��� ������������ � �������");
			}

			return result;
        }

	    public District Create(string name)
	    {
		    return new District(name);
	    }

	    #endregion
    }
}