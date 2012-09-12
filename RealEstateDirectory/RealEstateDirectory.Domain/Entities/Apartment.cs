namespace RealEstateDirectory.Domain.Entities
{
	public class Apartment : Residential
	{
		public virtual int? Floor { get; set; }
		public virtual int? TotalFloors { get; set; }
		public virtual Layout Layout { get; set; }
		public virtual bool? IsSeparateBathroom { get; set; }
	}
}
