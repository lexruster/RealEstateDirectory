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
//using ExportToRTF;
using ESCommon;
using ESCommon.Rtf;
using Misc.WordProvider;
using NLog;
using FontStyle = System.Drawing.FontStyle;

namespace RealEstateDirectory.Services.Export
{
	public class WordService : IWordService
	{
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();
		private RtfDocument rtf;

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
			catch (Exception e)
			{
				Log.ErrorException("Не удалось экспортировать в RTF", e);
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
			catch (Exception e)
			{
				Log.ErrorException("Не удалось экспортировать в RTF", e);
				_MessageService.ShowMessage(@"Не удалось экспортировать объявления в Ms Word", @"Ошибка",
				                            image: MessageBoxImage.Error);
			}
		}

		private void ExportToWordInner(string[] headers, string[,] data, string fileName)
		{
			WordProvider.Generate(headers, data, fileName);
			//RtfWriter rtfWriter = new RtfWriter();
			//rtf = new RtfDocument();
			//rtf.IsLandscape = true;
			//rtf.PageWidth = 16848;
			//rtf.PageHeight = 11952;
			//rtf.MarginLeft = 700;
			//rtf.MarginBottom = 700;
			//rtf.MarginRight = 700;
			//rtf.MarginTop = 1400;

			//rtf.FontTable.Add(new RtfFont("Calibri"));
			//rtf.FontTable.Add(new RtfFont("Constantia"));
			//rtf.ColorTable.AddRange(new RtfColor[]
			//	{
			//		new RtfColor(Color.Black),
			//		new RtfColor(0, 0, 255)
			//	});

			//RtfParagraphFormatting LeftAligned12 = new RtfParagraphFormatting(8, RtfTextAlign.Left);
			//RtfParagraphFormatting Centered10 = new RtfParagraphFormatting(8, RtfTextAlign.Center);
			//RtfFormattedParagraph header = new RtfFormattedParagraph(new RtfParagraphFormatting(16, RtfTextAlign.Center));

			//var t = new RtfTable(RtfTableAlign.Center, headers.Length, data.Length + 1);
			//header.Formatting.SpaceAfter = TwipConverter.ToTwip(8F, MetricUnit.Point);
			//header.AppendText(new RtfFormattedText("Недвижимость", RtfCharacterFormatting.Bold));
			//t.Width = TwipConverter.ToTwip(26, MetricUnit.Centimeter);

			//foreach (RtfTableRow row in t.Rows)
			//{
			//	row.Height = TwipConverter.ToTwip(2, MetricUnit.Centimeter);
			//}

			//t.DefaultCellStyle = new RtfTableCellStyle(RtfBorderSetting.All, Centered10);

			//for (int j = 0; j < headers.Length; j++)
			//{
			//	t[j, 0].Formatting = new RtfParagraphFormatting(8, RtfTextAlign.Center);
			//	t[j, 0].Formatting.TextColorIndex = 1;
			//	t[j, 0].AppendText(headers[j]);
			//}

			//for (int i = 0; i < data.GetLength(0); i++)
			//{
			//	for (int j = 0; j < headers.Length; j++)
			//	{
			//		t[j, i + 1].Definition.Style = new RtfTableCellStyle(RtfBorderSetting.All, LeftAligned12,
			//															 RtfTableCellVerticalAlign.Bottom);
			//		t[j, i + 1].AppendText(data[i, j] ?? "");
			//	}
			//}

			//rtf.Contents.AddRange(new RtfDocumentContentBase[]
			//	{
			//		header,
			//		t,
			//	});

			//using (TextWriter writer = new StreamWriter(fileName))
			//{
			//	rtfWriter.Write(writer, rtf);
			//	writer.Flush();
			//	writer.Close();
			//}

			Process.Start(fileName);
		}
	}
}