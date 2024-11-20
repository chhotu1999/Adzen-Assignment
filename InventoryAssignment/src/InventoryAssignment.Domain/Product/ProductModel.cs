using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace InventoryAssignment.Product
{
    public class ProductModel : AuditedAggregateRoot<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        [EnumDataType(typeof(UnitType))]
        [Column(TypeName = "nvarchar(max)")]
        public string Unit { get; set; }
        public decimal UnitPrice { get; set; }

        [EnumDataType(typeof(CategoryType))]
        [Column(TypeName = "nvarchar(max)")]
        public string Category { get; set; }
        public string Description { get; set; }

        public decimal StockLevel { get; set; }
    }
}
