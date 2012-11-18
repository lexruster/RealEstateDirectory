using System;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Dictionaries.Common;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Dictionaries.RealtorDictionary
{
	[NotifyForAll]
	public class RoomViewModel : NotificationObject
	{
		public RoomViewModel(IRoomService service)
		{
			_Service = service;
		}

		#region Infrastructure

		private readonly IRoomService _Service;

		#endregion

		public int Id { get; set; }
		protected District District { get; set; }
		protected string Description { get; set; }
		protected DealVariant DealVariant { get; set; }

		public override void UpdateValuesFromModel()
		{
			Id = DbEntity.Id;
			DealVariant = DbEntity.DealVariant;
			Description = DbEntity.Description;
			District = DbEntity.District;
		}

		public override void UpdateModelFromValues()
		{
		}

		public override string this[string columnName]
		{
			get
			{
				
				return null;
			}
		}

		public override string Error
		{
			get
			{
				var dictElement = new Room();
				var validation = _Service.IsValid(dictElement, Id);
				return validation.IsValid ? null : validation.GetReasons();
			}
		}

		public override void SaveToDatabase()
		{
			_Service.Save(DbEntity);
			UpdateValuesFromModel();
		}
	}
}