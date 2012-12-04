using System.Collections.Generic;

namespace DataTransfer
{
	public class IdMap
	{
		/// <summary>
		/// id элемента в базе linq 
		/// </summary>
		public List<int> LinqIds;

		/// <summary>
		/// id нового элемента в хибере
		/// </summary>
		public int HbId;

		/// <summary>
		/// имя для поиска дублей
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