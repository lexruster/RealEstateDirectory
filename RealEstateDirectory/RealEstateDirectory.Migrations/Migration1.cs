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
			    .WithColumn("Name").AsString().NotNullable();

		    Create.Table("Layout")
			    .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
				.WithColumn("Name").AsString().NotNullable();

		    Create.Table("Sewage")
			    .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
				.WithColumn("Name").AsString().NotNullable();

		    Create.Table("Street")
			    .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
				.WithColumn("Name").AsString().NotNullable();

		    Create.Table("State")
			    .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
				.WithColumn("Name").AsString().NotNullable();

		    Create.Table("RealEstate")
			    .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
			    .WithColumn("IsSale").AsBoolean().Nullable()
			    .WithColumn("Price").AsCurrency().Nullable()
			    .WithColumn("AreaId").AsInt32().Nullable().ForeignKey("FK_RealEstate_Area", "Area", "Id").OnDeleteOrUpdate(Rule.SetNull)
				.WithColumn("StreetId").AsInt32().Nullable().ForeignKey("FK_RealEstate_Street", "Street", "Id").OnDeleteOrUpdate(Rule.SetNull)
				.WithColumn("Additional").AsString().Nullable();

		    Create.Table("Residential")
			    .WithColumn("RealEstateId").AsInt32().PrimaryKey().Identity().NotNullable().ForeignKey("K_Residential_RealEstate", "RealEstate", "Id")
			    .WithColumn("LivingSquare").AsDecimal(3, 10).Nullable()
			    .WithColumn("TotalSquare").AsDecimal(3, 10).Nullable()
			    .WithColumn("RoomsCount").AsInt32().Nullable()
			    .WithColumn("StateId").AsInt32().Nullable().ForeignKey("FK_Residential_State", "State", "Id").OnDeleteOrUpdate(Rule.SetNull)
			    .WithColumn("Number").AsString().Nullable();

		    Create.Table("Apartment")
			    .WithColumn("ResidentialId").AsInt32().PrimaryKey().Identity().NotNullable().ForeignKey("K_Apartment_Residential", "Residential", "RealEstateId")
				.WithColumn("Floor").AsInt32().Nullable()
				.WithColumn("TotalFloors").AsInt32().Nullable()
			    .WithColumn("LayoutId").AsInt32().Nullable().ForeignKey("FK_Apartment_Layout", "Layout", "Id").OnDeleteOrUpdate(Rule.SetNull)
				.WithColumn("IsSeparateBathroom").AsBoolean().Nullable();

		    Create.Table("House")
				.WithColumn("ResidentialId").AsInt32().PrimaryKey().Identity().NotNullable().ForeignKey("K_House_Residential", "Residential", "RealEstateId")
				.WithColumn("Floors").AsInt32().Nullable()
				.WithColumn("Square").AsDecimal(3, 10).Nullable()
				.WithColumn("WithGarage").AsBoolean().Nullable()
				.WithColumn("ExtBuilt").AsString().Nullable()
				.WithColumn("IsElectricityPresent").AsBoolean().Nullable()
				.WithColumn("IsGasPresent").AsBoolean().Nullable()
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
