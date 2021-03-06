using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class MaterialService : DictionaryService<Material>, IMaterialService
    {
        #region ����

		public override string DictionaryName
		{
			get { return "���������"; }
		}

        #endregion

        #region �����������

        public MaterialService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region ������

		public override ValidationResult IsPossibilityToDelete(Material entity)
        {
			var result = new ValidationResult();
			if (!Repository.IsPossibleToDeleteMaterial(entity))
			{
				result.FailValidation("������� ��� ������������ � �������");
			}

			return result;
        }

		public Material Create(string name)
		{
			return new Material(name);
		}

        #endregion
    }
}