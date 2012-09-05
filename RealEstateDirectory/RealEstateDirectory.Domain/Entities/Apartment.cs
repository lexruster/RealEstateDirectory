namespace RealEstateDirectory.Domain.Entities
{
	public class Apartment : Residential
	{
		public int? Floor { get; set; }
		public int? TotalFloors { get; set; }
		public Layout Layout { get; set; }
		public bool? IsSeparateBathroom { get; set; }
	}
}
