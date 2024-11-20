using InventoryAssignment.Samples;
using Xunit;

namespace InventoryAssignment.EntityFrameworkCore.Domains;

[Collection(InventoryAssignmentTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<InventoryAssignmentEntityFrameworkCoreTestModule>
{

}
