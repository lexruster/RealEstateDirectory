using FluentMigrator;

namespace RealEstateDirectory.Migrations
{
	[Migration(2)]
	public class Migration2 : Migration
	{
		public override void Up()
		{
			Create.DictionaryTable("Condition");
			Alter.Table("RealEstate")
				.AddColumn("ConditionId").AsInt32().Nullable().ForeignKey("FK_RealEstate_Condition", "Condition", "Id");
		}

		public override void Down()
		{
			Delete.Column("ConditionId").FromTable("RealEstate");
			Delete.Table("Condition");
		}
	}
}