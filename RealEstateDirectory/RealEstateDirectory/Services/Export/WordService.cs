using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ExportToRTF;
using FontStyle = System.Drawing.FontStyle;

namespace RealEstateDirectory.Services.Export
{
	public class WordService : IWordService
	{
		private WordDocument wordDocument = new WordDocument(WordDocumentFormat.A4);
		private Font caption0 = new Font("Arial", 12f, FontStyle.Bold);
		private Font caption = new Font("Arial", 8f, FontStyle.Regular);
		private Font normalBold = new Font("Arial", 8f, FontStyle.Regular);
		private Font normal = new Font("Arial", 8f, FontStyle.Regular);

		private readonly IMessageService _MessageService;
		private readonly IDataExportService _DataExportService;

		public WordService(IMessageService messageService, IDataExportService dataExportService)
		{
			_MessageService = messageService;
			_DataExportService = dataExportService;
		}

		public void ExportToWord(DataGrid grid, string fileName)
		{
			try
			{
				var headers = _DataExportService.GetHeader(grid);
				var data = _DataExportService.GetData(grid);
				ExportToWordInner(headers, data, fileName);
			}
			catch
			{
				_MessageService.ShowMessage(@"Не удалось экспортировать объявления в Ms Word", @"Ошибка",
				                            image: MessageBoxImage.Error);
			}
		}

		public void ExportToWord(string[] headers, string[,] data, string fileName)
		{
			try
			{
				ExportToWordInner(headers, data, fileName);
			}
			catch
			{
				_MessageService.ShowMessage(@"Не удалось экспортировать объявления в Ms Word", @"Ошибка",
				                            image: MessageBoxImage.Error);
			}
		}

		private void ExportToWordInner(string[] headers, string[,] data, string fileName)
		{
			this.wordDocument = new WordDocument(WordDocumentFormat.InCentimeters(29.7, 21.0, 2.0, 2.0, 2.0, 2.0));
			this.wordDocument.SetFont(this.caption0);
			this.wordDocument.SetTextAlign(WordTextAlign.Center);
			this.wordDocument.WriteLine("E-8");
			this.wordDocument.WriteLine("");
			this.wordDocument.SetFont(this.normal);
			this.wordDocument.SetTextAlign(WordTextAlign.Left);
			this.wordDocument.WriteLine(DateTime.Now.ToShortDateString());
			this.wordDocument.WriteLine("");
			this.wordDocument.SetFont(this.caption0);
			this.wordDocument.SetTextAlign(WordTextAlign.Center);
			this.wordDocument.WriteLine("Недвижимость");
			this.wordDocument.SetFont(this.normal);
			this.wordDocument.SetTextAlign(WordTextAlign.Left);

			WordTable wordTable = this.wordDocument.NewTable(this.normal, Color.Black, data.Length + 1, headers.Length, 10);

			//		  wordTable.SetColumnsWidth(new int[13]
			//{
			//  40,
			//  20,
			//  70,
			//  35,
			//  40,
			//  30,
			//  70,
			//  40,
			//  30,
			//  40,
			//  30,
			//  70,
			//  70
			//});
			wordTable.SetContentAlignment(ContentAlignment.MiddleLeft);

			for (int j = 0; j < headers.Length; j++)
			{
				wordTable.Cell(0, j).Write(headers[j]);
			}


			for (int i = 0; i < data.GetLength(0); i++)
			{
				wordTable.Rows[i].SetFont(this.normal);
				for (int j = 0; j < headers.Length; j++)
				{
					wordTable.Cell(i, j).Write(data[i, j] ?? "");
				}
				wordTable.Columns[0].SetBorders(Color.Black, 1, false, true, false, false);
			}

			wordTable.SaveToDocument(15000, 0);

			wordDocument.SaveToFile(fileName);
			Process.Start(fileName);
		}
	}
}