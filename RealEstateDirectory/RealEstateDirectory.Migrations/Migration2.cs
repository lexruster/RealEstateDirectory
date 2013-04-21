using FluentMigrator;

namespace RealEstateDirectory.Migrations
{
	[Migration(2)]
	public class Migration2 : Migration
	{
		public override void Up()
		{
			Delete
				.Column("HasBathhouse")
				.Column("HasGarage")
				.Column("HasGas")
				.Column("WaterSupplyId")
				.Column("SewageId")
				.Column("MaterialId")
				.FromTable("House");
		}

		public override void Down()
		{

		}
	}
}