using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDirectory.Data.Entities
{
	public class Apartment : Residential
	{
		public int? Floor { get; set; }
		public int? TotalFloors { get; set; }
		public Layout Layout { get; set; }
		public bool? IsSeparateBathroom { get; set; }
	}
}
