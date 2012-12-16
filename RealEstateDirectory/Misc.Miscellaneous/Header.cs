namespace Misc.Miscellaneous
{
	public class Header
	{
		public string Name;
		public float Width;

		public Header(string name)
		{
			Name = name;
		}

		public Header(string name, float width)
			: this(name)
		{
			Width = width;
		}
	}
}