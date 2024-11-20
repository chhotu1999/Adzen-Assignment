using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAssignment.StockSummary
{
    public class StockSummaryDto
    {
        public int StockSumamryId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public string StockMode { get; set; }

        public decimal StockQuantity { get; set; }

        public string Unit { get; set; }

        public decimal BalanceQuantity { get; set; }

    }
}
