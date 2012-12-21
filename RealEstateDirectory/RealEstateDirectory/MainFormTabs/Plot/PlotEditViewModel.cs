using System;
using System.Collections.Generic;
using System.Globalization;
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

				if (propertyName == PropertySupport.ExtractPropertyName(() => PlotSquare) && !String.IsNullOrWhiteSpace(PlotSquare))
				{
					decimal plotSquare;
					if (!Decimal.TryParse(PlotSquare, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, NumberFormatInfo.CurrentInfo, out plotSquare))
						return "Площадь участка введена некорректно";
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

		#region Свойства

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
