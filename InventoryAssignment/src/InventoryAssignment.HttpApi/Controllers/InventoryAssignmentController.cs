using InventoryAssignment.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace InventoryAssignment.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class InventoryAssignmentController : AbpControllerBase
{
    protected InventoryAssignmentController()
    {
        LocalizationResource = typeof(InventoryAssignmentResource);
    }
}
