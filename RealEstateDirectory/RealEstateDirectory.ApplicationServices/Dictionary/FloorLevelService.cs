using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class FloorLevelService : DictionaryService<FloorLevel>, IFloorLevelService
    {
        #region ����
		
		public override string DictionaryName
		{
			get { return "������ �����"; }
		}

        #endregion

        #region �����������

        public FloorLevelService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region ������

		public override ValidationResult IsPossibilityToDelete(FloorLevel entity)
        {
			var result = new ValidationResult();
			if (!Repository.IsPossibleToDeleteFloorLevel(entity))
			{
				result.FailValidation("������� ��� ������������ � �������");
			}

			return result;
        }

		public FloorLevel Create(string name)
		{
			return new FloorLevel(name);
		}

        #endregion
    }
}