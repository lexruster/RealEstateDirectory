using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.MainFormTabs.Common;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.MainFormTabs.Room
{
    [NotifyForAll]
    public class RoomViewModel : RealEstateViewModel<Domain.Entities.Room>
    {
        #region Свойства  INotifi

        public decimal? TotalSquare { get; set; }
        public int? TotalRoomCount { get; set; }
        public int? TotalFloor { get; set; }
        public Terrace Terrace { get; set; }
        public int? RoomCount { get; set; }
        public Material Material { get; set; }
        public Layout Layout { get; set; }
        public FloorLevel FloorLevel { get; set; }
        public int? Floor { get; set; }

        #endregion

        #region Свойства

        public string TotalSquareString
        {
            get { return String.Format("{0:0.0}", TotalSquare); }
        }

        #endregion

        #region Методы

        protected override void UpdateValuesFromConcreteModel()
        {
            Floor = DbEntity.Floor;
            FloorLevel = DbEntity.FloorLevel;
            Layout = DbEntity.Layout;
            Material = DbEntity.Material;
            RoomCount = DbEntity.RoomCount;
            Terrace = DbEntity.Terrace;
            TotalFloor = DbEntity.TotalFloor;
            TotalRoomCount = DbEntity.TotalRoomCount;
            TotalSquare = DbEntity.TotalSquare;
        }

        #endregion
    }
}