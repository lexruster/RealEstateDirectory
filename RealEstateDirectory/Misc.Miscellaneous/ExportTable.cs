using System.Collections.Generic;

namespace Misc.Miscellaneous
{
	/// <summary>
	/// �����, �������������� ���� ������ ��� ��������
	/// </summary>
	public class ExportTable
	{
		public ExportTable(string title, List<Header> headers, List<List<string>> data)
		{
			Title = title;
			Headers = headers;
			Data = data;
		}

		public ExportTable(string title)
		{
			Title = title;
			Headers=new List<Header>();
			Data =new List<List<string>>();
		}

		/// <summary>
		/// ��������� �������
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// ���������
		/// </summary>
		public List<Header> Headers { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public List<List<string>> Data { get; set; }
	}
}