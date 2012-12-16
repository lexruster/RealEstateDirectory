using FluentMigrator;

namespace RealEstateDirectory.Migrations
{
	[Migration(3)]
	public class Migration3 : Migration
	{
		public override void Up()
		{
			Create.DictionaryTable("Destination");
			Alter.Table("Residence")
				.AddColumn("DestinationId").AsInt32().Nullable().ForeignKey("FK_Residence_Destination", "Destination", "Id");
		}

		public override void Down()
		{
			Delete.Column("DestinationId").FromTable("Residence");
			Delete.Table("Destination");
		}
	}

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