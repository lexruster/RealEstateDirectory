using System;
using NotifyPropertyWeaver;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.MainFormTabs.Common;

namespace RealEstateDirectory.MainFormTabs.Residence
{
	[NotifyForAll]
	public class ResidenceViewModel : RealEstateViewModel<Domain.Entities.Residence>
	{
		#region Свойства  INotify

		public decimal? TotalSquare { get; set; }
		public int? TotalFloor { get; set; }
		public Material Material { get; set; }
		public int? Floor { get; set; }

		#endregion

		#region Свойства

		public string TotalSquareString
		{
			get { return String.Format("{0:0.0}", TotalSquare); }
		}

		public string FloorString
		{
			get
			{
				if (Floor.HasValue || TotalFloor.HasValue)
					return String.Format("{0}/{1}", Floor, TotalFloor);
				return "";
			}
		}

		#endregion

		#region Методы

		protected override void UpdateValuesFromConcreteModel()
		{
			Floor = DbEntity.Floor;
			Material = DbEntity.Material;
			TotalFloor = DbEntity.TotalFloor;
			TotalSquare = DbEntity.TotalSquare;
		}

		#endregion
	}
}