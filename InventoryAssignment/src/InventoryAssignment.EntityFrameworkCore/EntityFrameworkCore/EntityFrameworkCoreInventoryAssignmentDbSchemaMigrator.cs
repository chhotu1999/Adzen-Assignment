using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using InventoryAssignment.Data;
using Volo.Abp.DependencyInjection;

namespace InventoryAssignment.EntityFrameworkCore;

public class EntityFrameworkCoreInventoryAssignmentDbSchemaMigrator
    : IInventoryAssignmentDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreInventoryAssignmentDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the InventoryAssignmentDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<InventoryAssignmentDbContext>()
            .Database
            .MigrateAsync();
    }
}
