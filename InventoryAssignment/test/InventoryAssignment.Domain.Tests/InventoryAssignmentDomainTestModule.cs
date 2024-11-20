using Volo.Abp.Modularity;

namespace InventoryAssignment;

[DependsOn(
    typeof(InventoryAssignmentDomainModule),
    typeof(InventoryAssignmentTestBaseModule)
)]
public class InventoryAssignmentDomainTestModule : AbpModule
{

}
