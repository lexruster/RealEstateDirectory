using System;
using System.Collections.Generic;
using System.Globalization;
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
									  IMaterialService materialService, IConditionService conditionalService, IDestinationService destinationService)
			: base(residenceService, messageService, districtService, realtorService, ownershipService, dealVariantService, conditionalService)
		{
			_ViewsService = viewsService;
			_MaterialService = materialService;
			_DestinationService = destinationService;
		}

		#region Инфраструктура

		private readonly IViewsService _ViewsService;
		private readonly IMaterialService _MaterialService;
		private readonly IDestinationService _DestinationService;

		#endregion

		#region Сущность

		#region Свойства

		public ListCollectionView Material { get; set; }
		public ListCollectionView Destination { get; set; }

		public string TotalSquare { get; set; }

		[NotifyProperty(AlsoNotifyFor = new[] { "TotalFloor" })]
		public string Floor { get; set; }

		[NotifyProperty(AlsoNotifyFor = new[] { "Floor" })]
		public string TotalFloor { get; set; }

		#endregion

		#region Валидация

		public override string this[string propertyName]
		{
			get
			{
				var baseResult = base[propertyName];
				if (baseResult != null)
					return baseResult;

				if (propertyName == PropertySupport.ExtractPropertyName(() => TotalSquare) && !String.IsNullOrWhiteSpace(TotalSquare))
				{
					decimal totalSquare;
					if (!Decimal.TryParse(TotalSquare, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, NumberFormatInfo.CurrentInfo, out totalSquare))
						return "Общая площадь введена некорректно";
				}

				if (propertyName == PropertySupport.ExtractPropertyName(() => TotalFloor) && !String.IsNullOrWhiteSpace(TotalFloor))
				{
					int totalFloor;
					if (!Int32.TryParse(TotalFloor, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.CurrentInfo, out totalFloor))
						return "Количество этажей введено некорректно";
				}

				if (propertyName == PropertySupport.ExtractPropertyName(() => Floor) && !String.IsNullOrWhiteSpace(Floor))
				{
					int floor;
					if (!Int32.TryParse(Floor, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.CurrentInfo, out floor))
						return "Этаж введен некорректно";
				}

				if ((propertyName == PropertySupport.ExtractPropertyName(() => TotalFloor)
					|| propertyName == PropertySupport.ExtractPropertyName(() => Floor))
					&& !String.IsNullOrWhiteSpace(TotalFloor) && !String.IsNullOrWhiteSpace(Floor))
				{
					var totalFloor = Int32.Parse(TotalFloor);
					var floor = Int32.Parse(Floor);
					if (floor > totalFloor)
						return "Этаж не может быть больше общего количества этажй";
				}

				return null;
			}
		}

		protected override IEnumerable<string> ValidatableProperties
		{
			get
			{
				foreach (var validatableProperty in base.ValidatableProperties)
					yield return validatableProperty;

				yield return PropertySupport.ExtractPropertyName(() => TotalSquare);
				yield return PropertySupport.ExtractPropertyName(() => Floor);
				yield return PropertySupport.ExtractPropertyName(() => TotalFloor);
			}
		}

		#endregion

		#endregion

		#region Взаимодействие с моделью БД

		protected override Domain.Entities.Residence CreateNewModel()
		{
			var residence = new Domain.Entities.Residence();
			UpdateConcreteModelFromValues(residence);
			SetRealEstateValues(residence);

			return residence;
		}

		protected override void UpdateValuesFromConcreteModel()
		{
			TotalSquare = DbEntity.TotalSquare.HasValue ? DbEntity.TotalSquare.Value.ToString("0.#") : String.Empty;
			TotalFloor = DbEntity.TotalFloor.HasValue ? DbEntity.TotalFloor.Value.ToString() : String.Empty;
			Floor = DbEntity.Floor.HasValue ? DbEntity.Floor.Value.ToString() : String.Empty;
			Material.MoveCurrentTo(DbEntity.Material);
			Destination.MoveCurrentTo(DbEntity.Destination);
		}

		protected override void UpdateConcreteModelFromValues(Domain.Entities.Residence residence)
		{
			residence.TotalSquare = String.IsNullOrWhiteSpace(TotalSquare) ? null : new decimal?(Decimal.Parse(TotalSquare));
			residence.TotalFloor = String.IsNullOrWhiteSpace(TotalSquare) ? null : new int?(Int32.Parse(TotalFloor));
			residence.Floor = String.IsNullOrWhiteSpace(TotalSquare) ? null : new int?(Int32.Parse(Floor));
			residence.Material = ResolveDictionary<Material>(Material);
			residence.Destination = ResolveDictionary<Destination>(Destination);
		}

		#endregion

		protected override void InitCollection()
		{
			Material = new ListCollectionView((new[] { NullMaterial }).Concat(_MaterialService.GetAll()).ToList());
			Destination = new ListCollectionView((new[] { NullDestination }).Concat(_DestinationService.GetAll()).ToList());
		}

		protected override void CloseDialog()
		{
			_ViewsService.CloseResidenceDialog();
		}

		protected override void OpenDialog()
		{
			_ViewsService.OpenResidenceDialog(this);
		}
	}
}