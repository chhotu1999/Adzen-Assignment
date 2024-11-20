using InventoryAssignment.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace InventoryAssignment.Permissions;

public class InventoryAssignmentPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(InventoryAssignmentPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(InventoryAssignmentPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<InventoryAssignmentResource>(name);
    }
}
