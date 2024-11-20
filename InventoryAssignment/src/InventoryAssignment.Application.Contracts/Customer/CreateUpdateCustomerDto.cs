using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAssignment.Customer
{
    public class CreateUpdateCustomerDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public String Gender { get; set; }
        public string Email { get; set; }
        [Required]
        public string Contact { get; set; }
        public string Address { get; set; }
    }
}
