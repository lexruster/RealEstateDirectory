using System;
using System.Collections.Generic;
using System.Text;

namespace Misc.Miscellaneous
{
	/// <summary>
	/// Класс, представляющий данные для экспорта
	/// </summary>
	public class ExportObject
	{
		public ExportObject()
		{
			Tables = new List<ExportTable>();
		}

		public List<ExportTable> Tables { get; set; }
	}

	/// <summary>
	/// Как экспортировать
	/// </summary>
	public enum ExportMode
	{
		All = 1,
		Filtered = 2,
		Selected = 3
	}
}