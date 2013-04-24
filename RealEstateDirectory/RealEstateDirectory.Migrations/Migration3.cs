using FluentMigrator;

namespace RealEstateDirectory.Migrations
{
	[Migration(3)]
	public class Migration3 : Migration
	{
		public override void Up()
		{
			Delete.Index("IX_Street_Name").OnTable("Street");
		}

		public override void Down()
		{
			Create.UniqueConstraint("IX_Street_Name").OnTable("Street").Column("Name");
		}
	}
}