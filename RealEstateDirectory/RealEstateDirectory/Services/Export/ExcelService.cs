using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Misc.ExcelProvider;
using Misc.Miscellaneous;
using NLog;

namespace RealEstateDirectory.Services.Export
{
	public class ExcelService : IExcelService
	{
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();
		private readonly IMessageService _MessageService;
		private readonly IDataGridExportService _dataGridExportService;

		public ExcelService(IMessageService messageService, IDataGridExportService dataGridExportService)
		{
			_MessageService = messageService;
			_dataGridExportService = dataGridExportService;
		}

		public void ExportToExcel(DataGrid grid)
		{
			try
			{
				var headers = _dataGridExportService.GetHeader(grid);
				var data = _dataGridExportService.GetData(grid);
				ExportToExcelInner(headers, data);
			}
			catch (Exception e)
			{
				Log.ErrorException("Не удалось экспортировать в EXCEL", e);
				_MessageService.ShowMessage(@"Не удалось экспортировать объявления в Ms Excel", @"Ошибка",
				                            image: MessageBoxImage.Error);
			}
		}

		public void ExportToExcel(ExportObject data)
		{
			try
			{
				ExportToExcelInner(data.Tables[0].Headers, data.Tables[0].Data);
			}
			catch (Exception e)
			{
				Log.ErrorException("Не удалось экспортировать в EXCEL", e);
				_MessageService.ShowMessage(@"Не удалось экспортировать объявления в Ms Excel", @"Ошибка",
				                            image: MessageBoxImage.Error);
			}
		}

		private void ExportToExcelInner(List<string> headers, List<List<string>> data)
		{
			var table = new ExportTable("", headers, data);
			ExcelProvider.Generate(table);
		}
	}
}