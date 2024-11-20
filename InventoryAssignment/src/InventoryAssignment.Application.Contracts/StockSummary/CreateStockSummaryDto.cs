using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAssignment.StockSummary
{
    public class CreateStockSummaryDto
    {
        public int ProductId { get; set; }
        public decimal StockQuantity { get; set; }
    }
}
