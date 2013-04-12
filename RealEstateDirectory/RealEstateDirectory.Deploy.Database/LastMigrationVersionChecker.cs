using System;
using System.Linq;
using FluentMigrator;

namespace RealEstateDirectory.Deploy.Database
{
	public static class LastMigrationVersionChecker
	{
		public static long LastMigrationVersion = typeof (LastMigrationVersionChecker).Assembly
			.GetExportedTypes()
			.Select(type => Attribute.GetCustomAttribute(type, typeof (MigrationAttribute)))
			.Where(attribute => attribute != null)
			.Cast<MigrationAttribute>()
			.Max(attribute => attribute.Version);
	}
}