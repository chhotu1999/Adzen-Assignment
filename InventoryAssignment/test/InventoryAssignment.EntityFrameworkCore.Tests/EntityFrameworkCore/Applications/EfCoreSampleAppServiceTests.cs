using InventoryAssignment.Samples;
using Xunit;

namespace InventoryAssignment.EntityFrameworkCore.Applications;

[Collection(InventoryAssignmentTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<InventoryAssignmentEntityFrameworkCoreTestModule>
{

}
