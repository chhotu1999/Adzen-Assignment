
using InventoryAssignment.Customer;
using InventoryAssignment.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace InventoryAssignment.Sales
{
    public class MvSales : AuditedAggregateRoot<int>
    {

        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; } 
        public CustomerModel Customer { get; set; } // Navigation property

        public decimal NetAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }

        public string? Remarks { get; set; }
        public ICollection<MvSalesDetail> SalesDetails { get; set; }
    }

    public class MvSalesDetail : AuditedAggregateRoot<int>
    {
        public int SalesId { get; set; }
        public MvSales Sales { get; set; } // Navigation property

        public int ProductId { get; set; }
        public ProductModel Product { get; set; } // Navigation property

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }

   
}
