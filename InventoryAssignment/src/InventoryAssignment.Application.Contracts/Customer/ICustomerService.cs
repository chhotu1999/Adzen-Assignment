using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InventoryAssignment.Customer
{
    public interface ICustomerService : ICrudAppService<CustomerDto, int, PagedAndSortedResultRequestDto, CreateUpdateCustomerDto>
    {
        Task<List<CustomerDto>> GetAllCustomersAsync(); // Method to get all customer without pagination

    }
}
