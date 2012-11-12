using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
    public class ToiletTypeService : DictionaryService<ToiletType>, IToiletTypeService
    {
        #region ����

		public override string DictionaryName
		{
			get { return "���. ����"; }
		}

        #endregion

        #region �����������

        public ToiletTypeService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
            : base(persistenceContext, serviceLocator)
        {
        }

        #endregion

        #region ������

        public override bool IsPossibilityToDelete(ToiletType entity)
        {
            return Repository.IsPossibleToDeleteToiletType(entity);
        }

		public ToiletType Create(string name)
		{
			return new ToiletType(name);
		}

        #endregion
    }
}