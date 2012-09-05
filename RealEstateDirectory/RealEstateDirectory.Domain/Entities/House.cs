namespace RealEstateDirectory.Domain.Entities
{
	public class House : Residential
	{
		public int? Floors { get; set; }
		public decimal? Square { get; set; }
		public bool? WithGarage { get; set; }
		public string ExtBuilt { get; set; }
		public bool? IsElectricityPresent { get; set; }
		public bool? IsGasPresent { get; set; }
		public Sewage Sewage { get; set; }
	}
}
