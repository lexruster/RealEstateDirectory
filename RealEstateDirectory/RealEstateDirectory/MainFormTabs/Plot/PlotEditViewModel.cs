using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.ViewModel;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.MainFormTabs.Common;
using RealEstateDirectory.Services;

namespace RealEstateDirectory.MainFormTabs.Plot
{
	[NotifyForAll]
	public class PlotEditViewModel : RealEstateEditViewModel<Domain.Entities.Plot>
	{
		#region Конструктор

		public PlotEditViewModel(IPlotService service, IMessageService messageService,
			IDistrictService districtService, IViewsService viewsService,
			IRealtorService realtorService, IOwnershipService ownershipService,
			IDealVariantService dealVariantService, IConditionService conditionalService)
			: base(service, messageService, districtService, realtorService, ownershipService, dealVariantService, conditionalService)
		{
			_ViewsService = viewsService;
		}

		#endregion

		#region Инфраструктура

		private readonly IViewsService _ViewsService;

		#endregion

		#region Сущность

		#region Свойства

		public string PlotSquare { get; set; }

		#endregion

		#region Валидация

		public override string this[string propertyName]
		{
			get
			{
				var baseResult = base[propertyName];
				if (baseResult != null)
					return baseResult;

				if (propertyName == PropertySupport.ExtractPropertyName(() => PlotSquare) && !IsValidAndPositiveDecimal(PlotSquare))
					return "Площадь участка введена некорректно";

				return null;
			}
		}

		protected override IEnumerable<string> ValidableProperties
		{
			get
			{
				foreach (var validatableProperty in base.ValidableProperties)
					yield return validatableProperty;

				yield return PropertySupport.ExtractPropertyName(() => PlotSquare);
			}
		}

		#endregion

		#region Взаимодействие с моделью БД

		protected override Domain.Entities.Plot CreateNewModel()
		{
			var plot = new Domain.Entities.Plot();
			UpdateConcreteModelFromValues(plot);
			SetRealEstateValues(plot);

			return plot;
		}

		protected override void UpdateValuesFromConcreteModel()
		{
			PlotSquare = DbEntity.PlotSquare.HasValue ? DbEntity.PlotSquare.Value.ToString("0.#") : String.Empty;
		}

		protected override void UpdateConcreteModelFromValues(Domain.Entities.Plot plot)
		{
			plot.PlotSquare = String.IsNullOrWhiteSpace(PlotSquare) ? null : new decimal?(Decimal.Parse(PlotSquare));
		}

		#endregion

		#endregion

		#region Перегрузки

		protected override void InitCollection()
		{
		}

		protected override void CloseDialog()
		{
			_ViewsService.ClosePlotDialog();
		}

		protected override void OpenDialog()
		{
			_ViewsService.OpenPlotDialog(this);
		}

		#endregion
	}
}
