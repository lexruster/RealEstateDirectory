using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDirectory.Data.Entities
{
	public class RealEstate
	{
		public virtual int Id { get; protected set; }
		public virtual bool? IsSale { get; set; }
		public virtual decimal? Price { get; set; }
		public virtual Area Area { get; set; }
		public virtual Street Street { get; set; }
		public virtual string Additional { get; set; }
	}
}
