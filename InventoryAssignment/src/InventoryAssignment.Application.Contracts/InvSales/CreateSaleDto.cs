using InventoryAssignment.Product;
using InventoryAssignment.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAssignment.InvSales
{
    public class CreateSaleDto
    {
        public int CustomerId { get; set; }
        public List<SalesDetailDto> SalesDetails { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Remarks { get; set; }
    }

    public class CreateSalesDetailDto
    {
        public int SalesId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
