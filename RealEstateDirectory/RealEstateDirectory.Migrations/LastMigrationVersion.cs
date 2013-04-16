using System;
using System.Linq;
using FluentMigrator;

namespace RealEstateDirectory.Migrations
{
	public static class LastMigrationVersion
	{
		public static long Version = typeof(LastMigrationVersion).Assembly
			.GetExportedTypes()
			.Select(type => Attribute.GetCustomAttribute(type, typeof(MigrationAttribute)))
			.Where(attribute => attribute != null)
			.Cast<MigrationAttribute>()
			.Max(attribute => attribute.Version);
	}
}