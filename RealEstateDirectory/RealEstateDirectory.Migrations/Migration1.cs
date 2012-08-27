using System.Data;
using FluentMigrator;

namespace RealEstateDirectory.Migrations
{
	[Migration(1)]
    public class Migration1 : Migration
    {
	    public override void Up()
	    {
		    Create.Table("Area")
			    .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
			    .WithColumn("Name").AsString();

		    Create.Table("Layout")
			    .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
			    .WithColumn("Name").AsString();

		    Create.Table("Sewage")
			    .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
			    .WithColumn("Name").AsString();

		    Create.Table("Street")
			    .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
			    .WithColumn("Name").AsString();

		    Create.Table("State")
			    .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
			    .WithColumn("Name").AsString();

		    Create.Table("RealEstate")
			    .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
			    .WithColumn("IsSale").AsBoolean().NotNullable()
			    .WithColumn("Price").AsCurrency().Nullable()
			    .WithColumn("AreaId").AsInt32().Nullable().ForeignKey("FK_RealEstate_Area", "Area", "Id").OnDeleteOrUpdate(Rule.SetNull)
				.WithColumn("StreetId").AsInt32().Nullable().ForeignKey("FK_RealEstate_Street", "Street", "Id").OnDeleteOrUpdate(Rule.SetNull)
				.WithColumn("Additional").AsString();

		    Create.Table("Residential")
			    .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable().ForeignKey("K_Residential_RealEstate", "RealEstate", "Id")
			    .WithColumn("LivingSquare").AsDecimal(3, 10)
				.WithColumn("TotalSquare").AsDecimal(3, 10)
			    .WithColumn("RoomsCount").AsInt32()
			    .WithColumn("StateId").AsInt32().Nullable().ForeignKey("FK_Residential_State", "State", "Id").OnDeleteOrUpdate(Rule.SetNull)
			    .WithColumn("Number").AsString();

		    Create.Table("Apartment")
			    .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable().ForeignKey("K_Apartment_Residential", "Residential", "Id")
			    .WithColumn("Floor").AsInt32()
			    .WithColumn("TotalFloors").AsInt32()
			    .WithColumn("LayoutId").AsInt32().Nullable().ForeignKey("FK_Apartment_Layout", "Layout", "Id").OnDeleteOrUpdate(Rule.SetNull)
			    .WithColumn("IsSeparateBathroom").AsBoolean();

		    Create.Table("House")
			    .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable().ForeignKey("K_House_Residential", "Residential", "Id")
			    .WithColumn("Floors").AsInt32()
				.WithColumn("Square").AsDecimal(3, 10)
			    .WithColumn("WithGarage").AsBoolean()
			    .WithColumn("ExtBuilt").AsString()
			    .WithColumn("IsElectricityPresent").AsBoolean()
			    .WithColumn("IsGasPresent").AsBoolean()
			    .WithColumn("SewageId").AsInt32().Nullable().ForeignKey("FK_House_Sewage", "Sewage", "Id").OnDeleteOrUpdate(Rule.SetNull);
	    }

	    public override void Down()
	    {
		    Delete.Table("House");
		    Delete.Table("Apartment");
		    Delete.Table("Residential");
		    Delete.Table("RealEstate");
		    Delete.Table("State");
		    Delete.Table("Street");
		    Delete.Table("Sewage");
		    Delete.Table("Layout");
		    Delete.Table("Area");
	    }
    }
}
