using Volo.Abp.Modularity;

namespace InventoryAssignment;

[DependsOn(
    typeof(InventoryAssignmentApplicationModule),
    typeof(InventoryAssignmentDomainTestModule)
)]
public class InventoryAssignmentApplicationTestModule : AbpModule
{

}
