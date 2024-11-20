using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAssignment.Product
{
    public class CreateUpdateProductDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public string Unit { get; set; }
        [Required]
        public string Category { get; set; }
        public string Description { get; set; }

    }
}
