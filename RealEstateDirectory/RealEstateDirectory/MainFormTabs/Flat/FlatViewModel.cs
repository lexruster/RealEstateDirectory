using System;
using NotifyPropertyWeaver;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.MainFormTabs.Common;

namespace RealEstateDirectory.MainFormTabs.Flat
{
    [NotifyForAll]
    public class FlatViewModel : RealEstateViewModel<Domain.Entities.Flat>
    {
        #region Свойства  INotify

        public decimal? TotalSquare { get; set; }
        public decimal? ResidentialSquare { get; set; }
        public decimal? KitchenSquare { get; set; }
        public int? TotalRoomCount { get; set; }
        public int? TotalFloor { get; set; }
        public Terrace Terrace { get; set; }
        public Material Material { get; set; }
        public Layout Layout { get; set; }
        public FloorLevel FloorLevel { get; set; }
        public int? Floor { get; set; }
        public ToiletType ToiletType { get; set; }

        #endregion

        #region Свойства

        public string TotalSquareString
        {
            get { return String.Format("{0:0.0}", TotalSquare); }
        }

        public string ResidentialSquareString
        {
            get { return String.Format("{0:0.0}", ResidentialSquare); }
        }

        public string KitchenSquareString
        {
            get { return String.Format("{0:0.0}", KitchenSquare); }
        }

        public string SquareString
        {
            get { return String.Format("{0:0.0}/{1:0.0}/{2:0.0}", TotalSquare, ResidentialSquare, KitchenSquare); }
        }

        public string FloorString
        {
            get
            {
                if(Floor.HasValue|| TotalFloor.HasValue)
                    return String.Format("{0}/{1}", Floor, TotalFloor);
                return "";
            }
        }

        #endregion

        #region Методы

        protected override void UpdateValuesFromConcreteModel()
        {
            Floor = DbEntity.Floor;
            FloorLevel = DbEntity.FloorLevel;
            Layout = DbEntity.Layout;
            Material = DbEntity.Material;
            Terrace = DbEntity.Terrace;
            TotalFloor = DbEntity.TotalFloor;
            TotalRoomCount = DbEntity.TotalRoomCount;
            TotalSquare = DbEntity.TotalSquare;
            KitchenSquare = DbEntity.KitchenSquare;
            ResidentialSquare= DbEntity.ResidentialSquare;
            ToiletType = DbEntity.ToiletType;
        }

        #endregion
    }
}