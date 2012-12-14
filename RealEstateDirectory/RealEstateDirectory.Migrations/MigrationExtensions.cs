using FluentMigrator.Builders.Create;
using FluentMigrator.Builders.Create.Table;

namespace RealEstateDirectory.Migrations
{
	public static class MigrationExtensions
	{
		public static ICreateTableWithColumnSyntax DictionaryTable(this ICreateExpressionRoot createExpressionRoot, string tableName)
		{
			return createExpressionRoot.Table(tableName)
				.WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
				.WithColumn("Name").AsString(2000).Unique("IX_" + tableName + "_Name").NotNullable();
		}
	}
}