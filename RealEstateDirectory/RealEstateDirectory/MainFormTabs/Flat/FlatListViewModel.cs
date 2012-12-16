using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using Misc.Miscellaneous;
using NotifyPropertyWeaver;
using RealEstateDirectory.AbstractApplicationServices;
using RealEstateDirectory.AbstractApplicationServices.Dictionary;
using RealEstateDirectory.MainFormTabs.Common;
using RealEstateDirectory.Services;
using RealEstateDirectory.Services.Export;

namespace RealEstateDirectory.MainFormTabs.Flat
{
	[NotifyForAll]
	public class FlatListViewModel : RealEstateListViewModel<Domain.Entities.Flat>
	{
		public FlatListViewModel(IFlatService service, IMessageService messageService,
		                         IDistrictService districtService, IRealtorService realtorService,
		                         IOwnershipService ownershipService, IDealVariantService dealVariantService,
		                         IConditionService conditionService,
		                         IExcelService excelService, IWordService wordService,
		                         IServiceLocator serviceLocator)
			: base(
				service, messageService, districtService, realtorService, ownershipService, dealVariantService, conditionService,
				excelService,
				wordService, serviceLocator)
		{
		}

		public int? TotalRoomCount { get; set; }
		protected override RealEstateViewModel<Domain.Entities.Flat> GetViewEntityViewModel()
		{
			return _ServiceLocator.GetInstance<FlatViewModel>();
		}

		protected override RealEstateEditViewModel<Domain.Entities.Flat> GetEditEntityViewModel()
		{
			return _ServiceLocator.GetInstance<FlatEditViewModel>();
		}

		protected override IEnumerable<Domain.Entities.Flat> UpdateChildFilterData(IEnumerable<Domain.Entities.Flat> entities)
		{
			entities = TotalRoomCount.HasValue
				           ? entities.Where(room => room.TotalRoomCount == TotalRoomCount.Value)
				           : entities;

			if (FilterTerrace != null && !Equals(FilterTerrace, _AllTerrace))
			{
				entities = entities.Where(room => room.Terrace != null && room.Terrace.Name.ToLower() != "���".ToLower());
			}

			return entities;
		}

		protected override void LoadChildFiltersData()
		{
			TotalRoomCount = null;
		}

		public override ExportTable GetExportedTable(ExportMode mode)
		{
			var table = new ExportTable("��������")
				{
					Headers = new List<Header>
						{
							new Header("�", 30),
							new Header("�����", 60),
							new Header("�����", 100),
							new Header("����", 150),
							new Header("�/�", 200),
							new Header("����.", 300),
							new Header("�������", 400),
							new Header("���.", 500),
							new Header("����.", 700),
							new Header("���.����", 100),
							new Header("����.", 100),
							new Header("�����������", 500),
							new Header("�������", 100),
							new Header("���� �.�.", 100),
							new Header("�.", 100),
							new Header("�������", 100),
						}
				};

			var collection = GetCollectionForExport(mode);
			
			foreach (var item in collection)
			{
				var flat = item as FlatViewModel;
				var row = new List<string>
					{
						flat.TotalRoomCount.ToString(),
						GetBaseDictionaryName(flat.District),
						flat.Address,
						flat.FloorString,
						GetBaseDictionaryName(flat.FloorLevel),
						GetBaseDictionaryName(flat.Layout),
						flat.SquareString,
						GetBaseDictionaryName(flat.Material),
						GetBaseDictionaryName(flat.Terrace),
						GetBaseDictionaryName(flat.ToiletType),
						GetBaseDictionaryName(flat.Condition),
						flat.Description,
						GetBaseDictionaryName(flat.DealVariant),
						flat.PriceString,
						flat.HasVideo ? "�" : "",
						flat.Realtor == null ? "" : flat.Realtor.Phone

					};
				table.Data.Add(row);
			}

			return table;
		}
	}
}