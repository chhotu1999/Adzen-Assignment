using Microsoft.Extensions.Localization;
using InventoryAssignment.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace InventoryAssignment;

[Dependency(ReplaceServices = true)]
public class InventoryAssignmentBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<InventoryAssignmentResource> _localizer;

    public InventoryAssignmentBrandingProvider(IStringLocalizer<InventoryAssignmentResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
