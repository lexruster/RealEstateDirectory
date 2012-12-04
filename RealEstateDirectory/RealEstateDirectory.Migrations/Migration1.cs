using System;
using System.Data;
using FluentMigrator;

namespace RealEstateDirectory.Migrations
{
	[Migration(1)]
	public class Migration1 : Migration
	{
		public override void Up()
		{
			CreateDictionaryTable("DealVariant");
			CreateDictionaryTable("District");
			CreateDictionaryTable("FloorLevel");
			CreateDictionaryTable("Layout");
			CreateDictionaryTable("Material");
			CreateDictionaryTable("Ownership");
			CreateDictionaryTable("Realtor");
			CreateDictionaryTable("RealtorAgency");
			CreateDictionaryTable("Sewage");
			CreateDictionaryTable("Street");
			CreateDictionaryTable("Terrace");
			CreateDictionaryTable("ToiletType");
			CreateDictionaryTable("WaterSupply");

			Create.Table("RealEstate")
				.WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
				.WithColumn("TerritorialNumber").AsString().Nullable()
				.WithColumn("Description").AsString(Int32.MaxValue).Nullable()
				.WithColumn("HasVideo").AsBoolean().NotNullable()
				.WithColumn("SubmitToVDV").AsBoolean().NotNullable()
				.WithColumn("SubmitToDomino").AsBoolean().NotNullable()
				.WithColumn("Price").AsDecimal(5, 19).Nullable()
				.WithColumn("CreateDate").AsDateTime().NotNullable()
				.WithColumn("DistrictId").AsInt32().Nullable().ForeignKey("FK_RealEstate_District", "District", "Id")
				.WithColumn("StreetId").AsInt32().Nullable().ForeignKey("FK_RealEstate_Street", "Street", "Id")
				.WithColumn("RealtorId").AsInt32().Nullable().ForeignKey("FK_RealEstate_Realtor", "Realtor", "Id")
				.WithColumn("DealVariantId").AsInt32().Nullable().ForeignKey("FK_RealEstate_DealVariant", "DealVariant", "Id")
				.WithColumn("OwnershipId").AsInt32().Nullable().ForeignKey("FK_RealEstate_Ownership", "DealVariant", "Id");

			Create.Table("Building")
				.WithColumn("RealEstateId").AsInt32().PrimaryKey().NotNullable().ForeignKey("FK_Building_RealEstate", "RealEstate", "Id").OnDelete(Rule.Cascade)
				.WithColumn("Floor").AsInt32().Nullable()
				.WithColumn("TotalFloor").AsInt32().Nullable()
				.WithColumn("TotalSquare").AsDecimal(5, 19).Nullable()
				.WithColumn("MaterialId").AsInt32().Nullable().ForeignKey("FK_Building_Material", "Material", "Id");

			Create.Table("Apartment")
				.WithColumn("BuildingId").AsInt32().PrimaryKey().NotNullable().ForeignKey("FK_Apartment_Building", "Building", "RealEstateId").OnDelete(Rule.Cascade)
				.WithColumn("TotalRoomCount").AsInt32().Nullable()
				.WithColumn("LayoutId").AsInt32().Nullable().ForeignKey("FK_Apartment_Layout", "Layout", "Id")
				.WithColumn("TerraceId").AsInt32().Nullable().ForeignKey("FK_Apartment_Terrace", "Terrace", "Id")
				.WithColumn("FloorLevelId").AsInt32().Nullable().ForeignKey("FK_Apartment_FloorLevel", "FloorLevel", "Id");

			Create.Table("Flat")
				.WithColumn("ApartmentId").AsInt32().PrimaryKey().NotNullable().ForeignKey("FK_Flat_Apartment", "Apartment", "BuildingId").OnDelete(Rule.Cascade)
				.WithColumn("ResidentialSquare").AsDecimal(5, 19).Nullable()
				.WithColumn("KitchenSquare").AsDecimal(5, 19).Nullable()
				.WithColumn("ToiletTypeId").AsInt32().Nullable().ForeignKey("FK_Flat_ToiletType", "ToiletType", "Id");

			Create.Table("Plot")
				.WithColumn("RealEstateId").AsInt32().PrimaryKey().NotNullable().ForeignKey("FK_Plot_RealEstate", "RealEstate", "Id").OnDelete(Rule.Cascade)
				.WithColumn("PlotSquare").AsDecimal(5, 19).Nullable();

			Create.Table("House")
				.WithColumn("PlotId").AsInt32().PrimaryKey().NotNullable().ForeignKey("FK_House_Plot", "Plot", "RealEstateId").OnDelete(Rule.Cascade)
				.WithColumn("TotalFloor").AsInt32().Nullable()
				.WithColumn("HouseSquare").AsDecimal(5, 19).Nullable()
				.WithColumn("HasBathhouse").AsBoolean().Nullable()
				.WithColumn("HasGarage").AsBoolean().Nullable()
				.WithColumn("HasGas").AsBoolean().Nullable()
				.WithColumn("WaterSupplyId").AsInt32().Nullable().ForeignKey("FK_House_WaterSupply", "WaterSupply", "Id")
				.WithColumn("SewageId").AsInt32().Nullable().ForeignKey("FK_House_Sewage", "Sewage", "Id")
				.WithColumn("MaterialId").AsInt32().Nullable().ForeignKey("FK_House_Material", "Material", "Id");

			Create.Table("Residence")
				.WithColumn("BuildingId").AsInt32().PrimaryKey().NotNullable().ForeignKey("FK_Residence_Building", "Building", "RealEstateId").OnDelete(Rule.Cascade);

			Create.Table("Room")
				.WithColumn("ApartmentId").AsInt32().PrimaryKey().NotNullable().ForeignKey("FK_Room_Apartment", "Apartment", "BuildingId").OnDelete(Rule.Cascade)
				.WithColumn("RoomCount").AsInt32().Nullable();
		}

		private void CreateDictionaryTable(string tableName)
		{
			Create.Table(tableName)
				.WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
				.WithColumn("Name").AsString(2000).Unique("IX_" + tableName + "_Name").NotNullable();
		}

		public override void Down()
		{
			Delete.Table("Room");
			Delete.Table("Residence");
			Delete.Table("House");
			Delete.Table("Plot");
			Delete.Table("Flat");
			Delete.Table("Apartment");
			Delete.Table("Building");
			Delete.Table("RealEstate");

			Delete.Table("DealVariant");
			Delete.Table("District");
			Delete.Table("FloorLevel");
			Delete.Table("Layout");
			Delete.Table("Material");
			Delete.Table("Ownership");
			Delete.Table("Realtor");
			Delete.Table("RealtorAgency");
			Delete.Table("Sewage");
			Delete.Table("Street");
			Delete.Table("Terrace");
			Delete.Table("ToiletType");
			Delete.Table("WaterSupply");
		}
	}
}
