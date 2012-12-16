using System;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.MainFormTabs.Common
{
	[NotifyForAll]
	public abstract class RealEstateViewModel<T> : NotificationObject
		where T : RealEstate
	{
		#region Конструктор

		#endregion

		#region Свойства  INotify

		public string TerritorialNumber { get; set; }
		public Street Street { get; set; }
		public Realtor Realtor { get; set; }
		public decimal? Price { get; set; }
		public Ownership Ownership { get; set; }
		public int Id { get; set; }
		public bool HasVideo { get; set; }
		public District District { get; set; }
		public string Description { get; set; }
		public DealVariant DealVariant { get; set; }
		public Condition Condition { get; set; }
		public DateTime CreateDate { get; set; }

		#endregion

		#region Свойства

		public string PriceString
		{
			get { return String.Format("{0:#,0.###}", Price); }
		}

        public string Address
        {
            get { return String.Format("{0}, {1}", Street!=null? Street.Name:"", TerritorialNumber); }
        }

        public string RealtorString
        {
            get
            {
                if (Realtor!=null)
                return String.Format("{0} {1}", Realtor.Name, Realtor.Phone);
                return "";
            }
        }

		public T DbEntity;

		#endregion

		#region Методы

		public void LoadViewModel(T entity)
		{
			DbEntity = entity;
			UpdateValuesFromModel();
		}

		public void UpdateValuesFromModel()
		{
			CreateDate = DbEntity.CreateDate;
			DealVariant = DbEntity.DealVariant;
			Condition = DbEntity.Condition;
			Description = DbEntity.Description;
			District = DbEntity.District;
			HasVideo = DbEntity.HasVideo;
			Id = DbEntity.Id;
			Ownership = DbEntity.Ownership;
			Price = DbEntity.Price;
			Realtor = DbEntity.Realtor;
			Street = DbEntity.Street;
			TerritorialNumber = DbEntity.TerritorialNumber;

			UpdateValuesFromConcreteModel();
		}

		protected abstract void UpdateValuesFromConcreteModel();

		#endregion
	}
}