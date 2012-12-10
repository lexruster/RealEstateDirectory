using System;
using System.Windows;
using System.Windows.Controls;
using NLog;

namespace RealEstateDirectory.Services.Export
{
	public class ExcelService : IExcelService
	{
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();
		private readonly IMessageService _MessageService;
		private readonly IDataExportService _DataExportService;

		public ExcelService(IMessageService messageService, IDataExportService dataExportService)
		{
			_MessageService = messageService;
			_DataExportService = dataExportService;
		}

		public void ExportToExcel(DataGrid grid)
		{
			try
			{
				var headers = _DataExportService.GetHeader(grid);
				var data = _DataExportService.GetData(grid);
				ExportToExcelInner(headers, data);
			}
			catch (Exception e)
			{
				Log.ErrorException("Не удалось экспортировать в EXCEL", e);
				_MessageService.ShowMessage(@"Не удалось экспортировать объявления в Ms Excel", @"Ошибка",
				                            image: MessageBoxImage.Error);
			}
		}

		public void ExportToExcel(string[] headers, string[,] data)
		{
			try
			{
				ExportToExcelInner(headers, data);
			}
			catch (Exception e)
			{
				Log.ErrorException("Не удалось экспортировать в EXCEL", e);
				_MessageService.ShowMessage(@"Не удалось экспортировать объявления в Ms Excel", @"Ошибка",
				                            image: MessageBoxImage.Error);
			}
		}

		private void ExportToExcelInner(string[] headers, string[,] data)
		{
			var excel = new Excel.ApplicationClass();
			excel.Application.Workbooks.Add(true);

			for (int j = 0; j < headers.Length; j++)
			{
				excel.Cells[1, j + 1] = headers[j];
			}

			excel.Cells.NumberFormat = "@";
			for (int rowNum = 0; rowNum < data.GetLength(0); rowNum++)
			{
				for (int colNum = 0; colNum < headers.Length; colNum++)
				{
					excel.Cells[rowNum + 2, colNum + 1] = data[rowNum, colNum];
				}
			}

			excel.Columns.HorizontalAlignment = true;
			var range2 = excel.get_Range(excel.Cells[1, 1],
			                             excel.Cells[data.Length + 1, headers.Length + 1]);
			range2 = range2.get_Resize(data.Length + 1, headers.Length + 1);
			range2.Columns.AutoFit();

			for (int j = 0; j < headers.Length - 1; j++)
			{
				Excel.Range Range12 = excel.get_Range(excel.Cells[1, j + 1], excel.Cells[1, j + 1]);
				var w = (double) Range12.ColumnWidth;
				if (w > 40)
				{
					Range12.ColumnWidth = 40;
				}
			}

			excel.Columns.WrapText = true;
			excel.Visible = true;
			var worksheet = (Excel.Worksheet) excel.ActiveSheet;
#pragma warning disable 467
			worksheet.Activate();
#pragma warning restore 467


		}
	}
}