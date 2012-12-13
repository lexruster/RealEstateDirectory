using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class TerraceService : DictionaryService<Terrace>, ITerraceService
    {
        #region ����

		public override string DictionaryName
		{
			get { return "������"; }
		}

        #endregion

        #region �����������

        public TerraceService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region ������

        public override ValidationResult IsPossibilityToDelete(Terrace entity)
        {
			var result = new ValidationResult();
			if (!Repository.IsPossibleToDeleteTerrace(entity))
			{
				result.FailValidation("������� ��� ������������ � �������");
			}

			return result;
        }

		public Terrace Create(string name)
		{
			return new Terrace(name);
		}

        #endregion
    }
}