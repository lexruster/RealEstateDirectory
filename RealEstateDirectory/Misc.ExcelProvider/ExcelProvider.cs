using Misc.Miscellaneous;
using Excel = Microsoft.Office.Interop.Excel;

namespace Misc.ExcelProvider
{
	public static class ExcelProvider
	{
		public static void Generate(ExportTable export)
		{
			var headers = export.Headers;
			var data = export.Data;
			var excel = new Excel.ApplicationClass();
			excel.Application.Workbooks.Add(true);

			for (int j = 0; j < headers.Count; j++)
			{
				excel.Cells[1, j + 1] = headers[j];
			}

			excel.Cells.NumberFormat = "@";
			for (int rowNum = 0; rowNum < data.Count; rowNum++)
			{
				for (int colNum = 0; colNum < headers.Count; colNum++)
				{
					excel.Cells[rowNum + 2, colNum + 1] = data[rowNum][colNum];
				}
			}

			excel.Columns.HorizontalAlignment = true;
			var range2 = excel.get_Range(excel.Cells[1, 1],
										 excel.Cells[data.Count + 1, headers.Count + 1]);
			range2 = range2.get_Resize(data.Count + 1, headers.Count + 1);
			range2.Columns.AutoFit();

			for (int j = 0; j < headers.Count - 1; j++)
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
