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
			System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
			try
			{
				System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
				var headers = _dataGridExportService.GetHeader(grid);
				var data = _dataGridExportService.GetData(grid);
				ExportToExcelInner(headers, data);
			}
			catch (Exception e)
			{
				Log.ErrorException("�� ������� �������������� � EXCEL", e);
				_MessageService.ShowMessage(@"�� ������� �������������� ���������� � Ms Excel", @"������",
				                            image: MessageBoxImage.Error);
			}
			finally
			{
				System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
			}
		}

		public void ExportToExcel(ExportObject data)
		{
			System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
			try
			{
				System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
				ExportToExcelInner(data.Tables[0].Headers, data.Tables[0].Data);
			}
			catch (Exception e)
			{
				Log.ErrorException("�� ������� �������������� � EXCEL", e);
				_MessageService.ShowMessage(@"�� ������� �������������� ���������� � Ms Excel", @"������",
				                            image: MessageBoxImage.Error);
			}
			finally 
			{
				System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
			}
		}

		private void ExportToExcelInner(List<Header> headers, List<List<string>> data)
		{
			var table = new ExportTable("", headers, data);
			ExcelProvider.Generate(table);
		}
	}
}