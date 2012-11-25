using System.Collections.Generic;

namespace DataTransfer
{
	public class IdMap
	{
		/// <summary>
		/// id �������� � ���� linq 
		/// </summary>
		public List<int> LinqIds;

		/// <summary>
		/// id ������ �������� � ������
		/// </summary>
		public int HbId;

		/// <summary>
		/// ��� ��� ������ ������
		/// </summary>
		public string Name;

		public IdMap(int l, int h, string name)
		{
			LinqIds = new List<int> {l};
			HbId = h;
			Name = name;
		}
	}
}