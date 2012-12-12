using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RealEstateDirectory.Domain.Entities;

namespace RealEstateDirectory.Domain.Data.Mapping
{
	public class PlotMap : JoinedSubclassMapping<Plot>
	{
		public PlotMap()
		{
			Key(k =>
				{
					k.Column("RealEstateId");
					k.OnDelete(OnDeleteAction.Cascade);
					k.ForeignKey("\"FK_Plot_RealEstate\"");
				});
			Property(x => x.PlotSquare, m =>
				{
					m.Precision(19);
					m.Scale(5);
				});
		}
	}
}