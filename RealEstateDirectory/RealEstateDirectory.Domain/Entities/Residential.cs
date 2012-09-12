namespace RealEstateDirectory.Domain.Entities
{
	public class Residential : RealEstate
	{
		public virtual decimal? LivingSquare { get; set; }
		public virtual decimal? TotalSquare { get; set; }
		public virtual int? RoomsCount { get; set; }
		public virtual State State { get; set; }
		public virtual string Number { get; set; }
	}
}
