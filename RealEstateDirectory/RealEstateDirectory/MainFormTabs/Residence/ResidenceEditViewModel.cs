using System;
using System.Linq;
using System.Windows.Data;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.MainFormTabs.Common;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.MainFormTabs.Residence
{
	[NotifyForAll]
	public class ResidenceEditViewModel : RealEstateEditViewModel<Domain.Entities.Residence>
	{
		public ResidenceEditViewModel(IResidenceService residenceService, IMessageService messageService,
		                              IDistrictService districtService, IViewsService viewsService, IRealtorService realtorService,
		                              IOwnershipService ownershipService, IDealVariantService dealVariantService,
		                              IMaterialService materialService)
			: base(residenceService, messageService, districtService, realtorService, ownershipService, dealVariantService)
		{
			_ViewsService = viewsService;
			_MaterialService = materialService;
		}

		#region Инфраструктура

		private readonly IViewsService _ViewsService;
		private readonly IMaterialService _MaterialService;

		#endregion

		#region Свойства  INotifi

		public decimal? TotalSquare { get; set; }
		public ListCollectionView Material { get; set; }

		private int? _TotalFloor;

		[DoNotNotify]
		public int? TotalFloor
		{
			get { return _TotalFloor; }
			set
			{
				if (_TotalFloor == value)
					return;
				_TotalFloor = value;
				RaisePropertyChanged(() => TotalFloor);
				RaisePropertyChanged(() => Floor);
			}
		}

		private int? _Floor;

		[DoNotNotify]
		public int? Floor
		{
			get { return _Floor; }
			set
			{
				if (_Floor == value)
					return;
				_Floor = value;
				RaisePropertyChanged(() => Floor);
				RaisePropertyChanged(() => TotalFloor);
			}
		}

		#endregion

		#region Перегрузки

		protected override void UpdateValuesFromConcreteModel()
		{
			TotalSquare = DbEntity.TotalSquare;
			TotalFloor = DbEntity.TotalFloor;
			Floor = DbEntity.Floor;
			Material.MoveCurrentTo(DbEntity.Material);
		}

		protected override void UpdateConcreteModelFromValues()
		{
			SetResidenceValues(DbEntity);
		}

		protected override void CloseDialog()
		{
			_ViewsService.CloseResidenceDialog();
		}

		protected override void OpenDialog()
		{
			_ViewsService.OpenResidenceDialog(this);
		}

		protected override Domain.Entities.Residence CreateNewModel()
		{
			var residence = new Domain.Entities.Residence();
			SetResidenceValues(residence);
			SetRealEstateValues(residence);

			return residence;
		}

		private void SetResidenceValues(Domain.Entities.Residence residence)
		{
			residence.TotalSquare = TotalSquare;
			residence.TotalFloor = TotalFloor;
			residence.Floor = Floor;
			residence.Material = ResolveDictionary<Material>(Material);
		}

		protected override string ChildDataError(string propertyName)
		{
			if (propertyName == PropertySupport.ExtractPropertyName(() => TotalSquare))
			{
				if (TotalSquare < 0)
					return "Площадь не может быть отрицательной";
			}

			if (propertyName == PropertySupport.ExtractPropertyName(() => Floor))
			{
				if (Floor < 0)
					return "Этаж не может быть отрицательным";
				if (Floor.HasValue && TotalFloor.HasValue && Floor > TotalFloor)
					return "Этаж не может быть больше общего числа этажей";
			}

			if (propertyName == PropertySupport.ExtractPropertyName(() => TotalFloor))
			{
				if (TotalFloor < 0)
					return "Всего этажей не может быть отрицательным";
				if (Floor.HasValue && TotalFloor.HasValue && Floor > TotalFloor)
					return "Этаж не может быть больше общего числа этажей";
			}

			return null;
		}

		protected override void InitCollection()
		{
			Material = new ListCollectionView((new[] { NullMaterial }).Concat(_MaterialService.GetAll()).ToList());
		}

		#endregion
	}
}