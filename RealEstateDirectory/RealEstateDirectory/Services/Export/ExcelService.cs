using System;
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
				Log.ErrorException("�� ������� �������������� � EXCEL", e);
				_MessageService.ShowMessage(@"�� ������� �������������� ���������� � Ms Excel", @"������",
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
				Log.ErrorException("�� ������� �������������� � EXCEL", e);
				_MessageService.ShowMessage(@"�� ������� �������������� ���������� � Ms Excel", @"������",
				                            image: MessageBoxImage.Error);
			}
		}

		private void ExportToExcelInner(string[] headers, string[,] data)
		{
			var table = new ExportTable(headers, data);
			ExcelProvider.Generate(table);
		}
	}
}