using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace InventoryAssignment.Data;

/* This is used if database provider does't define
 * IInventoryAssignmentDbSchemaMigrator implementation.
 */
public class NullInventoryAssignmentDbSchemaMigrator : IInventoryAssignmentDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
