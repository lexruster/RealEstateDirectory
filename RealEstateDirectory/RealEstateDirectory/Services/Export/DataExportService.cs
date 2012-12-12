using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RealEstateDirectory.Services.Export
{
	public class DataExportService : IDataExportService
	{
		private readonly IMessageService _MessageService;
		private const int SkipEndRows = 4;

		public DataExportService(IMessageService messageService)
		{
			_MessageService = messageService;
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

		public string[] GetHeader(DataGrid grid)
		{
			var headers = new List<string>();

			for (int j = 0; j < grid.Columns.Count - 1 - SkipEndRows; j++)
			{
				headers.Add(grid.Columns[j].Header.ToString());
			}

			return headers.ToArray();
		}

		public string[,] GetData(DataGrid grid)
		{
			List<object> list = grid.ItemsSource.Cast<object>().ToList();

			var data = new string[list.Count(),grid.Columns.Count];

			for (int rowNum = 0; rowNum < list.Count; rowNum++)
			{
				var item = list[rowNum];
				for (int colNum = 0; colNum < grid.Columns.Count - SkipEndRows; colNum++)
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
							data[rowNum, colNum] = text.ToString();
						}
					}
				}
			}
			return data;
		}
	}
}