using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ExportToRTF;

namespace RealEstateDirectory.Services
{
    public class WordService : IWordService
    {
        private readonly IMessageService _MessageService;

		public WordService(IMessageService messageService)
        {
            _MessageService = messageService;
        }

		public void ExportToWord(DataGrid grid, string fileName)
		{
			new E8RealtyesRtfExporter().GetRtfDocument(grid).SaveToFile(fileName);
			Process.Start(fileName);
		}


	    private void ExportToWordInner(DataGrid grid, string fileName)
        {
            try
            {
                List<object> list = grid.ItemsSource.Cast<object>().ToList();
                var excel = new Excel.ApplicationClass();
                excel.Application.Workbooks.Add(true);
                excel.Cells[1, 1] = "№";
                for (int j = 0; j < grid.Columns.Count - 1; j++)
                {
                    excel.Cells[1, j + 2] = grid.Columns[j].Header;
                }
                excel.Cells.NumberFormat = "@";
                for (int rowNum = 0; rowNum < list.Count; rowNum++)
                {
                    excel.Cells[rowNum + 2, 1] = rowNum + 1;
                    var item = list[rowNum];
                    for (int colNum = 0; colNum < grid.Columns.Count; colNum++)
                    {
                        DataGridColumn gridColumn = grid.Columns[colNum];
                        string[] path = GetPath(gridColumn);
                        if (path != null)
                        {
                            var text = GetValue(path, item);
                            if (text != null)
                            {
                                if (text.ToString() == "True")
                                {
                                    text = "Да";
                                }
                                if (text.ToString() == "False")
                                {
                                    text = "Нет";
                                }
                                excel.Cells[rowNum + 2, colNum + 2] = text.ToString();
                            }
                        }
                    }
                }

                excel.Columns.HorizontalAlignment = true;
                var range2 = excel.get_Range(excel.Cells[1, 1],
                                             excel.Cells[grid.Items.Count + 1, grid.Columns.Count + 1]);
                range2 = range2.get_Resize(grid.Items.Count + 1, grid.Columns.Count + 1);
                range2.Columns.AutoFit();

                for (int j = 0; j < grid.Columns.Count - 1; j++)
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
            catch
            {
                _MessageService.ShowMessage(@"Не удалось экспортировать объявления в Ms Excel", @"Ошибка",
                                            image: MessageBoxImage.Error);
            }
        }

        public string GetValue(object obj, string propName)
        {
            Type type = obj.GetType();
            PropertyInfo property = type.GetProperty(propName);

            if (property == null)
            {
                Debug.WriteLine(string.Format("Couldn't find property '{0}' in type '{1}'", propName, type.Name));
                return null;
            }

            return property.GetValue(obj, null).ToString();
        }

        private static object GetValue(string[] path, object obj)
        {
            foreach (string pathStep in path)
            {
                if (obj == null)
                    return null;

                Type type = obj.GetType();
                PropertyInfo property = type.GetProperty(pathStep);

                if (property == null)
                {
                    Debug.WriteLine(string.Format("Couldn't find property '{0}' in type '{1}'", pathStep, type.Name));
                    return null;
                }

                obj = property.GetValue(obj, null);
            }

            return obj;
        }

        private static string[] GetPath(DataGridColumn gridColumn)
        {
            string path = "";

            if (string.IsNullOrEmpty(path))
            {
                if (gridColumn is DataGridBoundColumn)
                {
                    Binding binding = ((DataGridBoundColumn) gridColumn).Binding as Binding;
                    if (binding != null)
                    {
                        path = binding.Path.Path;
                    }
                }
                else
                {
                    path = gridColumn.SortMemberPath;
                }
            }

            return string.IsNullOrEmpty(path) ? null : path.Split('.');
        }
    }

	internal sealed class E8RealtyesRtfExporter
	{
		private WordDocument wordDocument = new WordDocument(WordDocumentFormat.A4);
		//private Font caption0 = new Font("Arial", 12f, FontStyle.Bold);
		//private Font caption = new Font("Arial", 8f, FontStyle.Regular);
		//private Font normalBold = new Font("Arial", 8f, FontStyle.Regular);
		//private Font normal = new Font("Arial", 8f, FontStyle.Regular);

		public E8RealtyesRtfExporter()
		{
			this.wordDocument = new WordDocument(WordDocumentFormat.InCentimeters(29.7, 21.0, 2.0, 2.0, 2.0, 2.0));
		}

		public WordDocument GetRtfDocument(DataGrid grid)
		{
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
			this.wordDocument.WriteLine("Квартиры");
			this.wordDocument.SetFont(this.normal);
			this.wordDocument.SetTextAlign(WordTextAlign.Left);
			

			WordTable wordTable = this.wordDocument.NewTable(this.normal, Color.Black, dtAppartment.Rows.Count + 1, 13, 15);
			wordTable.SetColumnsWidth(new int[13]
      {
        40,
        20,
        70,
        35,
        40,
        30,
        70,
        40,
        30,
        40,
        30,
        70,
        70
      });
			wordTable.SetContentAlignment(ContentAlignment.MiddleLeft);
			wordTable.Cell(0, 0).Write("Район");
			wordTable.Cell(0, 1).Write("К.");
			wordTable.Cell(0, 2).Write("Адрес");
			wordTable.Cell(0, 3).Write("Этажн.");
			wordTable.Cell(0, 4).Write("План.");
			wordTable.Cell(0, 5).Write("М. ст.");
			wordTable.Cell(0, 6).Write("Площ.");
			wordTable.Cell(0, 7).Write("Балкон");
			wordTable.Cell(0, 8).Write("Сан. узел");
			wordTable.Cell(0, 9).Write("Вар.");
			wordTable.Cell(0, 10).Write("Цена");
			wordTable.Cell(0, 11).Write("Комментарий");
			wordTable.Cell(0, 12).Write("Риэлтор");
			int row = 1;
			foreach (DataRow dataRow in (InternalDataCollectionBase)dtAppartment.Rows)
			{
				wordTable.Rows[row].SetFont(this.normal);
				wordTable.Cell(row, 0).Write(dataRow["Район"].ToString());
				wordTable.Cell(row, 1).Write(dataRow["iRoomsAmount"].ToString());
				wordTable.Cell(row, 2).Write(dataRow["Адрес"].ToString());
				wordTable.Cell(row, 3).Write(dataRow["Этажность"].ToString());
				wordTable.Cell(row, 4).Write(dataRow["Планировка"].ToString());
				wordTable.Cell(row, 5).Write(dataRow["Материал стен"].ToString());
				wordTable.Cell(row, 6).Write(dataRow["Площадь"].ToString());
				wordTable.Cell(row, 7).Write(dataRow["Балкон"].ToString());
				wordTable.Cell(row, 8).Write(dataRow["Сан. узел"].ToString());
				wordTable.Cell(row, 9).Write(dataRow["Вар."].ToString());
				wordTable.Cell(row, 10).Write(dataRow["iPrice"].ToString());
				wordTable.Cell(row, 11).Write(dataRow["vcComment"].ToString());
				wordTable.Cell(row, 12).Write(dataRow["Риэлтор"].ToString());
				wordTable.Columns[0].SetBorders(Color.Black, 1, false, true, false, false);
				++row;
			}
			 ;
			 this.GetAppartmentTable(wordTable).SaveToDocument(15000, 0);

			
			return this.wordDocument;
		}
	}
}