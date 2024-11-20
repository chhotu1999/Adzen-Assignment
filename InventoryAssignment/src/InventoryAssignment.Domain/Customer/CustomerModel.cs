
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace InventoryAssignment.Customer
{
    public class CustomerModel : AuditedAggregateRoot<int>
    {
        public string Name { get; set; }

        [EnumDataType(typeof(GenderType))]
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
    }
}
