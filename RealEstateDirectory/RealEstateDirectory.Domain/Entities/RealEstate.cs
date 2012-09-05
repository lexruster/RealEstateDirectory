using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.Domain.Entities
{
	public class RealEstate:Entity<int>
	{
		public virtual bool? IsSale { get; set; }
		public virtual decimal? Price { get; set; }
		public virtual Area Area { get; set; }
		public virtual Street Street { get; set; }
		public virtual string Additional { get; set; }
	}
}
