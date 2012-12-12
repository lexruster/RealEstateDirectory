namespace Misc.Miscellaneous
{
	/// <summary>
	/// �����, �������������� ���� ������ ��� ��������
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
		/// ��������� �������
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// ���������
		/// </summary>
		public string[] Headers { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public string[,] Data { get; set; }
	}
}