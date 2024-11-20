using Volo.Abp.Modularity;

namespace InventoryAssignment;

public abstract class InventoryAssignmentApplicationTestBase<TStartupModule> : InventoryAssignmentTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
