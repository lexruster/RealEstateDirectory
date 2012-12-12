namespace Misc.Miscellaneous
{
	/// <summary>
	/// Класс, представляющий блок данных для экспорта
	/// </summary>
	public class ExportTable
	{
		public ExportTable(string[] headers, string[,] data)
		{
			Headers = headers;
			Data = data;
		}

		public ExportTable(string title, string[] headers, string[,] data)
			: this(headers, data)
		{
			Title = title;
		}

		/// <summary>
		/// Заголовок таблицы
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Заголовки
		/// </summary>
		public string[] Headers { get; set; }

		/// <summary>
		/// Данные
		/// </summary>
		public string[,] Data { get; set; }
	}
}