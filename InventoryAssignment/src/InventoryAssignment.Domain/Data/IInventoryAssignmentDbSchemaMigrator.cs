using System.Threading.Tasks;

namespace InventoryAssignment.Data;

public interface IInventoryAssignmentDbSchemaMigrator
{
    Task MigrateAsync();
}
