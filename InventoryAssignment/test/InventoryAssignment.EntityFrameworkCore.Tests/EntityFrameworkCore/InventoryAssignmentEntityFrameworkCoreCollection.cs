﻿using Xunit;

namespace InventoryAssignment.EntityFrameworkCore;

[CollectionDefinition(InventoryAssignmentTestConsts.CollectionDefinitionName)]
public class InventoryAssignmentEntityFrameworkCoreCollection : ICollectionFixture<InventoryAssignmentEntityFrameworkCoreFixture>
{

}
