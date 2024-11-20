using InventoryAssignment.Localization;
using Volo.Abp.Application.Services;

namespace InventoryAssignment;

/* Inherit your application services from this class.
 */
public abstract class InventoryAssignmentAppService : ApplicationService
{
    protected InventoryAssignmentAppService()
    {
        LocalizationResource = typeof(InventoryAssignmentResource);
    }
}
