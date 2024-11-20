using InventoryAssignment.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace InventoryAssignment.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(InventoryAssignmentEntityFrameworkCoreModule),
    typeof(InventoryAssignmentApplicationContractsModule)
)]
public class InventoryAssignmentDbMigratorModule : AbpModule
{
}
