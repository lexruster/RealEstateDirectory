using System;
using System.Collections.Generic;
using System.Linq;
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
}