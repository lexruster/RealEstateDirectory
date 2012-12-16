using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Misc.Miscellaneous;
using Misc.WordProvider;
using NLog;
using FontStyle = System.Drawing.FontStyle;

namespace RealEstateDirectory.Services.Export
{
	public class WordService : IWordService
	{
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();

		private readonly IMessageService _MessageService;
		private readonly IDataGridExportService _dataGridExportService;

		public WordService(IMessageService messageService, IDataGridExportService dataGridExportService)
		{
			_MessageService = messageService;
			_dataGridExportService = dataGridExportService;
		}

		public void ExportToWord(DataGrid grid, string fileName)
		{
			try
			{
				var headers = _dataGridExportService.GetHeader(grid);
				var data = _dataGridExportService.GetData(grid);
				ExportToWordInner(headers, data, fileName);
			}
			catch (Exception e)
			{
				Log.ErrorException("Не удалось экспортировать в RTF", e);
				_MessageService.ShowMessage(@"Не удалось экспортировать объявления в Ms Word", @"Ошибка",
				                            image: MessageBoxImage.Error);
			}
		}

		public void ExportToWord(ExportObject data)
		{
			var dlg = new Microsoft.Win32.SaveFileDialog
				{
					DefaultExt = "rich text format file |*.rtf",
					Filter = "rtf file |*.rtf"
				};

			if (dlg.ShowDialog() == true)
			{
				string fileName = dlg.FileName;
				try
				{
					WordProvider.Generate(data, fileName);
					Process.Start(fileName);
				}
				catch (Exception e)
				{
					Log.ErrorException("Не удалось экспортировать в RTF", e);
					_MessageService.ShowMessage(@"Не удалось экспортировать объявления в Ms Word", @"Ошибка",
					                            image: MessageBoxImage.Error);
				}
			}
		}

		private void ExportToWordInner(List<Header> headers, List<List<string>> data, string fileName)
		{
			var table = new ExportTable("", headers, data);
			var exportObject = new ExportObject();
			exportObject.Tables.Add(table);
			WordProvider.Generate(exportObject, fileName);
			Process.Start(fileName);
		}
	}
}