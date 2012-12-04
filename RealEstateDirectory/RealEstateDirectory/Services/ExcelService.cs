using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RealEstateDirectory.Services
{
    public class ExcelService : IExcelService
    {
        private readonly IMessageService _MessageService;

        public ExcelService(IMessageService messageService)
        {
            _MessageService = messageService;
        }

        public void ExportToExcel(DataGrid grid)
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
}