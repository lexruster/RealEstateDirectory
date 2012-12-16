using Microsoft.Practices.ServiceLocation;
using RealEstateDirectory.AbstractApplicationServices.Common;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.ApplicationServices.Dictionary
{
	public class DestinationService : DictionaryService<Destination>, IDestinationService
	{
		#region Поля

		public override string DictionaryName
		{
			get { return "Назначение"; }
		}

		#endregion

		#region Конструктор

		public DestinationService(IPersistenceContext persistenceContext, IServiceLocator serviceLocator)
			: base(persistenceContext, serviceLocator)
		{
		}

		#endregion

		#region Методы

		public override ValidationResult IsPossibilityToDelete(Destination entity)
		{
			var result = new ValidationResult();
			if (!Repository.IsPossibleToDeleteDestination(entity))
			{
				result.FailValidation("Элемент уже используется в системе");
			}

			return result;
		}

		public Destination Create(string name)
		{
			return new Destination(name);
		}

		#endregion
	}
}