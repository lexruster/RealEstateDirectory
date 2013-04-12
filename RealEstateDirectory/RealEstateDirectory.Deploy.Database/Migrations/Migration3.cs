using FluentMigrator;

namespace RealEstateDirectory.Deploy.Database.Migrations
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
}