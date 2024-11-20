using InventoryAssignment.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace InventoryAssignment.StockSummary
{
    public class StockSummaryModel : AuditedAggregateRoot<int>
    {
        public int ProductId { get; set; }

        [EnumDataType(typeof(StockType))] // Optional, for validation
        [Column(TypeName = "nvarchar(max)")]
        public string StockMode { get; set; }

        public decimal StockQuantity { get; set; }

        public decimal BalanceQuantity { get; set; }

        [ForeignKey("ProductId")]
        public ProductModel Product { get; set; }
    }
}
