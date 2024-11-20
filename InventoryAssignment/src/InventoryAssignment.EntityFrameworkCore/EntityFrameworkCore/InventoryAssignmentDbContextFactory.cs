using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InventoryAssignment.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class InventoryAssignmentDbContextFactory : IDesignTimeDbContextFactory<InventoryAssignmentDbContext>
{
    public InventoryAssignmentDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        InventoryAssignmentEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<InventoryAssignmentDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new InventoryAssignmentDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../InventoryAssignment.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
