using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDirectory.Data.Entities
{
	public class Residential : RealEstate
	{
		public decimal? LivingSquare { get; set; }
		public decimal? TotalSquare { get; set; }
		public int? RoomsCount { get; set; }
		public State State { get; set; }
		public string Number { get; set; }
	}
}
