namespace RealEstateDirectory.Domain.Entities
{
	public class House : Residential
	{
		public virtual int? Floors { get; set; }
		public virtual decimal? Square { get; set; }
		public virtual bool? WithGarage { get; set; }
		public virtual string ExtBuilt { get; set; }
		public virtual bool? IsElectricityPresent { get; set; }
		public virtual bool? IsGasPresent { get; set; }
		public virtual Sewage Sewage { get; set; }
	}
}
