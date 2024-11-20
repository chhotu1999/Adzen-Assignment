using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InventoryAssignment.Product
{
    public interface IProductService : ICrudAppService<ProductDto, int, PagedAndSortedResultRequestDto, CreateUpdateProductDto>
    {
        Task<List<ProductDto>> GetAllProductsAsync(); // Method to get all products without pagination

    }
}
