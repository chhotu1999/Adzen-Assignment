using Volo.Abp.Modularity;

namespace InventoryAssignment;

/* Inherit from this class for your domain layer tests. */
public abstract class InventoryAssignmentDomainTestBase<TStartupModule> : InventoryAssignmentTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
