using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace InventoryAssignment.Customer
{
    public class CustomerDto : AuditedEntityDto<int>
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
    }
}
