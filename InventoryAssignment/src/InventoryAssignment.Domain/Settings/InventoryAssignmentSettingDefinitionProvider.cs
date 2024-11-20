using Volo.Abp.Settings;

namespace InventoryAssignment.Settings;

public class InventoryAssignmentSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(InventoryAssignmentSettings.MySetting1));
    }
}
