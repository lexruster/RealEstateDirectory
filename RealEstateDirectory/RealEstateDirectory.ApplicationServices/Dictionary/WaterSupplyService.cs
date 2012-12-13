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
        #region ����

		public override string DictionaryName
		{
			get { return "�������������"; }
		}

        #endregion

        #region �����������

        public WaterSupplyService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region ������

		public override ValidationResult IsPossibilityToDelete(WaterSupply entity)
        {
			var result = new ValidationResult();
			if (!Repository.IsPossibleToDeleteWaterSupply(entity))
			{
				result.FailValidation("������� ��� ������������ � �������");
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