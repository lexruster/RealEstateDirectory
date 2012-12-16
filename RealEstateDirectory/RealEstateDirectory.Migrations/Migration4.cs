using FluentMigrator;

namespace RealEstateDirectory.Migrations
{
	[Migration(4)]
	public class Migration4 : Migration
	{
		public override void Up()
		{
			Delete.Column("SubmitToDomino").FromTable("RealEstate");
			Delete.Column("SubmitToVDV").FromTable("RealEstate");
		}

		public override void Down()
		{
			Alter.Table("RealEstate").AddColumn("SubmitToDomino").AsBoolean().Nullable();
			Alter.Table("RealEstate").AddColumn("SubmitToVDV").AsBoolean().Nullable();
		}
	}
}