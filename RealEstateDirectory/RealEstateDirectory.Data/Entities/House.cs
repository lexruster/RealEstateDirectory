using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDirectory.Data.Entities
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
